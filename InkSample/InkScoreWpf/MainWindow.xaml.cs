using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InkScoreWpf
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void GestureCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            var gestureCanvas = (InkCanvas)sender;

            gestureCanvas.SetEnabledGestures(new[]
            {
                ApplicationGesture.Circle,
                ApplicationGesture.DoubleCircle,
                ApplicationGesture.Check,
                ApplicationGesture.ArrowDown,
                ApplicationGesture.ChevronDown,
                ApplicationGesture.DownUp,
                ApplicationGesture.Up,
                ApplicationGesture.Down,
                ApplicationGesture.Left,
                ApplicationGesture.Right,
            });
        }

        void GestureCanvas_Gesture(object sender, InkCanvasGestureEventArgs e)
        {
            var result = e.GetGestureRecognitionResults()
                .Where(r => r.ApplicationGesture != ApplicationGesture.NoGesture)
                .FirstOrDefault(r => r.RecognitionConfidence == RecognitionConfidence.Strong);
            if (result == null) return;

            string sign;
            switch (result.ApplicationGesture)
            {
                case ApplicationGesture.Circle:
                case ApplicationGesture.DoubleCircle:
                    sign = Question.Circle;
                    break;
                case ApplicationGesture.Check:
                case ApplicationGesture.ArrowDown:
                case ApplicationGesture.ChevronDown:
                case ApplicationGesture.DownUp:
                case ApplicationGesture.Up:
                case ApplicationGesture.Down:
                case ApplicationGesture.Left:
                case ApplicationGesture.Right:
                    sign = Question.Check;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            var gestureCanvas = (InkCanvas)sender;
            var question = (Question)gestureCanvas.DataContext;
            question.Sign = sign;

            gestureCanvas.Strokes.Clear();
            gestureCanvas.Strokes.Add(e.Strokes);
        }
    }
}
