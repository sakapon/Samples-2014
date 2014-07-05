using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.FaceTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FaceTrackingConsole
{
    public class SkeletonFaceTracker
    {
        public SkeletonFaceTracker()
        {
            KinectContext.Current.AllFramesUpdated += Context_AllFramesUpdated;
        }

        public event Action<bool> IsSkeletonTrackedUpdated = b => { };
        public event Action<float?> JawLowerUpdated = v => { };

        KinectSensor sensor;
        byte[] colorImage;
        short[] depthImage;
        Skeleton[] skeletonData;

        void Context_AllFramesUpdated(KinectSensor sensor, ColorImageFrame cf, DepthImageFrame df, SkeletonFrame sf)
        {
            this.sensor = sensor;
            if (colorImage == null)
            {
                colorImage = new byte[cf.PixelDataLength];
                depthImage = new short[df.PixelDataLength];
                skeletonData = new Skeleton[sf.SkeletonArrayLength];
            }

            cf.CopyPixelDataTo(colorImage);
            df.CopyPixelDataTo(depthImage);
            sf.CopySkeletonDataTo(skeletonData);

            TrackFace();
        }

        int skeletonId = -1;
        FaceTracker faceTracker;

        void TrackFace()
        {
            var skeleton = skeletonData
                .Where(s => s.TrackingState != SkeletonTrackingState.NotTracked)
                .OrderBy(s => s.Position.Z)
                .FirstOrDefault();

            IsSkeletonTrackedUpdated(skeleton != null);

            if (skeleton == null)
            {
                skeletonId = -1;
                if (faceTracker != null)
                {
                    faceTracker.Dispose();
                    faceTracker = null;
                }

                JawLowerUpdated(null);
                return;
            }

            if (skeletonId != skeleton.TrackingId)
            {
                try
                {
                    if (faceTracker != null)
                    {
                        faceTracker.Dispose();
                    }
                    faceTracker = new FaceTracker(sensor);
                }
                catch (InvalidOperationException)
                {
                    return;
                }
            }
            skeletonId = skeleton.TrackingId;

            if (skeleton.TrackingState != SkeletonTrackingState.Tracked)
            {
                JawLowerUpdated(null);
                return;
            }

            // MEMO: FaceTrackFrame オブジェクトの Dispose メソッドを呼び出すと、以降の処理が正常に続かなくなります。
            var faceFrame = faceTracker.Track(sensor.ColorStream.Format, colorImage, sensor.DepthStream.Format, depthImage, skeleton);
            if (!faceFrame.TrackSuccessful)
            {
                JawLowerUpdated(null);
                return;
            }

            var animationUnits = faceFrame.GetAnimationUnitCoefficients();
            JawLowerUpdated(animationUnits[AnimationUnit.JawLower]);
        }
    }
}
