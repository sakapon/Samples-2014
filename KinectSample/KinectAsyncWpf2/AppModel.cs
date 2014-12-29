using KLibrary.ComponentModel;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace KinectAsyncWpf2
{
    public class AppModel : NotifyBase
    {
        AsyncKinectManager kinect;

        public string PositionText
        {
            get { return GetValue<string>(); }
            private set { SetValue(value); }
        }

        public AppModel()
        {
            kinect = new AsyncKinectManager(sensor =>
            {
                sensor.SkeletonStream.Enable();
                sensor.Start();

                sensor.SkeletonFrameReady += sensor_SkeletonFrameReady;
            });
        }

        void sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Thread.Sleep(200); // 意図的な負荷。

            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame == null)
                {
                    PositionText = "";
                    return;
                }

                var skeletons = new Skeleton[frame.SkeletonArrayLength];
                frame.CopySkeletonDataTo(skeletons);

                var skeleton = skeletons.FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);
                if (skeleton == null)
                {
                    PositionText = "";
                    return;
                }

                var p = skeleton.Position;
                PositionText = string.Format("({0:N3}, {1:N3}, {2:N3})", p.X, p.Y, p.Z);
            }
        }
    }
}
