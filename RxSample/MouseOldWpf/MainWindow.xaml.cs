using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

namespace MouseOldWpf
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(string), typeof(MainWindow), new PropertyMetadata(null));

        public string Orientation
        {
            get { return (string)GetValue(OrientationProperty); }
            private set { SetValue(OrientationProperty, value); }
        }

        public MainWindow()
        {
            InitializeComponent();

            var π = Math.PI;
            var orientationSymbols = new[] { "→", "↘", "↓", "↙", "←", "↖", "↑", "↗" };
            var zoneAngleRange = 2 * π / orientationSymbols.Length;

            var start = default(Point?);

            MouseDown += (o, e) =>
            {
                start = e.GetPosition(this);
            };
            MouseUp += (o, e) =>
            {
                if (!start.HasValue) return;

                var end = e.GetPosition(this);
                var _ = new { Start = start.Value, End = end };
                Debug.WriteLine(_);

                var d = _.End - _.Start;
                if (d.Length < 100) return;

                var angle = 2 * π + Math.Atan2(d.Y, d.X);
                var zone = (int)Math.Round(angle / zoneAngleRange) % orientationSymbols.Length;
                Orientation = orientationSymbols[zone];
            };
            MouseLeave += (o, e) =>
            {
                start = null;
            };
        }
    }
}
