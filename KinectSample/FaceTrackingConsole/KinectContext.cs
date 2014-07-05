using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackingConsole
{
    public class KinectContext : IDisposable
    {
        static KinectContext current;
        public static KinectContext Current
        {
            get
            {
                if (current == null)
                {
                    current = new KinectContext();
                }
                return current;
            }
        }

        static KinectContext()
        {
            AppDomain.CurrentDomain.ProcessExit += (o, e) =>
            {
                if (current != null)
                {
                    ((IDisposable)current).Dispose();
                }
            };
        }

        public event Action<KinectSensor, ColorImageFrame, DepthImageFrame, SkeletonFrame> AllFramesUpdated = (ks, cf, df, sf) => { };

        public KinectSensorChooser SensorChooser { get; private set; }

        KinectContext()
        {
            SensorChooser = new KinectSensorChooser();
            SensorChooser.KinectChanged += SensorChooser_KinectChanged;
            SensorChooser.Start();
        }

        ~KinectContext()
        {
            Dispose(false);
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (SensorChooser.Status == ChooserStatus.SensorStarted)
                {
                    SensorChooser.Stop();
                }
            }
            catch (Exception)
            {
            }
        }

        void SensorChooser_KinectChanged(object sender, KinectChangedEventArgs e)
        {
            if (e.OldSensor != null)
            {
                try
                {
                    e.OldSensor.AllFramesReady -= Kinect_AllFramesReady;
                    e.OldSensor.ColorStream.Disable();
                    e.OldSensor.DepthStream.Disable();
                    e.OldSensor.SkeletonStream.Disable();
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }

            if (e.NewSensor != null)
            {
                try
                {
                    e.NewSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                    e.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution320x240Fps30);
                    e.NewSensor.SkeletonStream.Enable();

                    try
                    {
                        e.NewSensor.DepthStream.Range = DepthRange.Near;
                        e.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                    }
                    catch (InvalidOperationException)
                    {
                        // Non Kinect for Windows devices do not support Near mode, so reset back to default mode.
                        e.NewSensor.DepthStream.Range = DepthRange.Default;
                        e.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    }
                    e.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;

                    e.NewSensor.AllFramesReady += Kinect_AllFramesReady;
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }
        }

        void Kinect_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            using (var cf = e.OpenColorImageFrame())
            using (var df = e.OpenDepthImageFrame())
            using (var sf = e.OpenSkeletonFrame())
            {
                if (cf == null || df == null || sf == null) return;

                AllFramesUpdated((KinectSensor)sender, cf, df, sf);
            }
        }
    }
}
