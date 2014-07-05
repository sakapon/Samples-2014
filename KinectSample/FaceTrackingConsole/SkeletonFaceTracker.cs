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
            KinectContext.Current.FaceTrackingFrameUpdated += Context_FaceTrackingFrameUpdated;
        }

        public event Action<float> JawLowerUpdated = v => { };

        byte[] colorImage;
        short[] depthImage;
        Skeleton[] skeletonData;

        int skeletonId = -1;
        FaceTracker faceTracker;

        void Context_FaceTrackingFrameUpdated(ColorImageFrame cf, DepthImageFrame df, SkeletonFrame sf)
        {
            if (colorImage == null)
            {
                colorImage = new byte[cf.PixelDataLength];
                depthImage = new short[df.PixelDataLength];
                skeletonData = new Skeleton[sf.SkeletonArrayLength];
            }

            cf.CopyPixelDataTo(colorImage);
            df.CopyPixelDataTo(depthImage);
            sf.CopySkeletonDataTo(skeletonData);

            var skeleton = skeletonData
                .Where(s => s.TrackingState != SkeletonTrackingState.NotTracked)
                .OrderBy(s => s.Position.Z)
                .FirstOrDefault();

            if (skeleton == null)
            {
                skeletonId = -1;
                if (faceTracker != null)
                {
                    faceTracker.Dispose();
                    faceTracker = null;
                }
                return;
            }

            var skeletonId_old = skeletonId;
            skeletonId = skeleton.TrackingId;

            if (skeletonId_old != skeletonId)
            {
                try
                {
                    faceTracker = new FaceTracker(KinectContext.Current.SensorChooser.Kinect);
                }
                catch (InvalidOperationException)
                {
                    return;
                }
            }

            if (skeleton.TrackingState != SkeletonTrackingState.Tracked) return;

            // MEMO: FaceTrackFrame オブジェクトの Dispose メソッドを呼び出すと、以降の処理が正常に続かなくなります。
            var faceFrame = faceTracker.Track(cf.Format, colorImage, df.Format, depthImage, skeleton);
            if (!faceFrame.TrackSuccessful) return;

            var animationUnits = faceFrame.GetAnimationUnitCoefficients();
            JawLowerUpdated(animationUnits[AnimationUnit.JawLower]);
        }
    }
}
