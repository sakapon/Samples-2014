using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KinectAsyncWpf
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor sensor;
        Skeleton[] skeletons;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(2000); // 意図的な負荷。

            if (KinectSensor.KinectSensors.Count == 0) return;

            sensor = KinectSensor.KinectSensors[0];
            sensor.SkeletonStream.Enable();
            sensor.Start();

            skeletons = new Skeleton[sensor.SkeletonStream.FrameSkeletonArrayLength];
            sensor.SkeletonFrameReady += sensor_SkeletonFrameReady;
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            if (sensor != null)
            {
                sensor.Stop();
            }
        }

        void sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Thread.Sleep(15); // 意図的な負荷。

            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame == null)
                {
                    ShowPosition("");
                    return;
                }

                frame.CopySkeletonDataTo(skeletons);

                var skeleton = skeletons.FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);
                if (skeleton == null)
                {
                    ShowPosition("");
                    return;
                }

                var p = skeleton.Position;
                ShowPosition(string.Format("({0:N3}, {1:N3}, {2:N3})", p.X, p.Y, p.Z));
            }
        }

        void ShowPosition(string text)
        {
            PositionText.Text = text;
        }
    }
}
