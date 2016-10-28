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
            DependencyProperty.Register(nameof(Delta), typeof(Vector?), typeof(MainWindow), new PropertyMetadata(null));

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

            var π = Math.PI;
            var orientationSymbols = new[] { "→", "↘", "↓", "↙", "←", "↖", "↑", "↗" };
            var zoneAngleRange = 2 * π / orientationSymbols.Length;
            Func<Vector, string> ToOrientation = v =>
            {
                var angle = 2 * π + Math.Atan2(v.Y, v.X);
                var zone = (int)Math.Round(angle / zoneAngleRange) % orientationSymbols.Length;
                return orientationSymbols[zone];
            };

            var events = new EventsExtension(this);
            events.MouseDrag.Subscribe(d => d.Subscribe(v =>
            {
                Delta = v;
                Orientation = ToOrientation(v);
            },
            () =>
            {
                Delta = null;
                Orientation = null;
            }));
        }
    }
}
