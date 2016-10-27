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
        public IObservable<IObservable<Vector>> MouseDrag { get; }

        public MainWindow()
        {
            InitializeComponent();

            // Replace events with IObservable objects.
            var mouseDown = Observable.FromEventPattern<MouseButtonEventArgs>(this, nameof(MouseDown)).Select(e => e.EventArgs);
            var mouseUp = Observable.FromEventPattern<MouseButtonEventArgs>(this, nameof(MouseUp)).Select(e => e.EventArgs);
            var mouseLeave = Observable.FromEventPattern<MouseEventArgs>(this, nameof(MouseLeave)).Select(e => e.EventArgs);
            var mouseMove = Observable.FromEventPattern<MouseEventArgs>(this, nameof(MouseMove)).Select(e => e.EventArgs);
            var mouseEnd = mouseUp.Merge(mouseLeave.Select(e => default(MouseButtonEventArgs)));

            MouseDrag = mouseDown
                .Select(e => e.GetPosition(this))
                .Select(p0 => mouseMove
                    .Select(e => e.GetPosition(this) - p0)
                    .TakeUntil(mouseEnd));
        }
    }
}
