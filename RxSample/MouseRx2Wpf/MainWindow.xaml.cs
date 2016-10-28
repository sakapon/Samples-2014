using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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

namespace MouseRx2Wpf
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty DeltaProperty =
            DependencyProperty.Register(nameof(Delta), typeof(Vector?), typeof(MainWindow), new PropertyMetadata(null, (d, e) => DeltaChanged((MainWindow)d, (Vector?)e.NewValue)));

        public Vector? Delta
        {
            get { return (Vector?)GetValue(DeltaProperty); }
            set { SetValue(DeltaProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(string), typeof(MainWindow), new PropertyMetadata(null));

        public string Orientation
        {
            get { return (string)GetValue(OrientationProperty); }
            private set { SetValue(OrientationProperty, value); }
        }

        public MainWindow()
        {
            InitializeComponent();

            var events = new EventsExtension(this);
            events.MouseDrag.Subscribe(d => d.Subscribe(v => Delta = v, () => Delta = null));
        }

        const double π = Math.PI;
        static readonly string[] orientationSymbols = new[] { "→", "↘", "↓", "↙", "←", "↖", "↑", "↗" };
        static readonly double zoneAngleRange = 2 * π / orientationSymbols.Length;

        static string ToOrientation(Vector v)
        {
            var angle = 2 * π + Math.Atan2(v.Y, v.X);
            var zone = (int)Math.Round(angle / zoneAngleRange) % orientationSymbols.Length;
            return orientationSymbols[zone];
        }

        static void DeltaChanged(MainWindow window, Vector? delta)
        {
            window.Orientation = delta == null ? null : ToOrientation(delta.Value);
        }
    }
}
