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
        WavesPlayer wavesPlayer;

        public MainWindow()
        {
            InitializeComponent();

            var sounds = Enum.GetNames(typeof(AnswerResult))
                .ToDictionary(n => n, n => string.Format(@"Sounds\{0}.wav", n));
            wavesPlayer = new WavesPlayer(sounds);
            wavesPlayer.LoadAsync();
        }

        void GestureCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            var gestureCanvas = (InkCanvas)sender;

            gestureCanvas.SetEnabledGestures(new[]
            {
                ApplicationGesture.Circle,
                ApplicationGesture.DoubleCircle,
                ApplicationGesture.Triangle,
                ApplicationGesture.Check,
                ApplicationGesture.ArrowDown,
                ApplicationGesture.ChevronDown,
                ApplicationGesture.DownUp,
                ApplicationGesture.Up,
                ApplicationGesture.Down,
                ApplicationGesture.Left,
                ApplicationGesture.Right,
                ApplicationGesture.Curlicue,
                ApplicationGesture.DoubleCurlicue,
            });
        }

        void GestureCanvas_Gesture(object sender, InkCanvasGestureEventArgs e)
        {
#if DEBUG
            var debug_resultsQuery = e.GetGestureRecognitionResults()
                .Where(r => r.ApplicationGesture != ApplicationGesture.NoGesture);
            foreach (var r in debug_resultsQuery)
            {
                Debug.WriteLine("{0}: {1}", r.ApplicationGesture, r.RecognitionConfidence);
            }
            Debug.WriteLine("");
#endif
            // 信頼性 (RecognitionConfidence) を無視したほうが、Circle と Triangle の認識率は上がるようです。
            var gestureResult = e.GetGestureRecognitionResults()
                .FirstOrDefault(r => r.ApplicationGesture != ApplicationGesture.NoGesture);
            if (gestureResult == null)
            {
                wavesPlayer.Play(AnswerResult.None.ToString());
                return;
            }

            AnswerResult answerResult;
            switch (gestureResult.ApplicationGesture)
            {
                case ApplicationGesture.Circle:
                case ApplicationGesture.DoubleCircle:
                    answerResult = AnswerResult.Correct;
                    break;
                case ApplicationGesture.Triangle:
                    answerResult = AnswerResult.Intermediate;
                    break;
                case ApplicationGesture.Check:
                case ApplicationGesture.ArrowDown:
                case ApplicationGesture.ChevronDown:
                case ApplicationGesture.DownUp:
                case ApplicationGesture.Up:
                case ApplicationGesture.Down:
                case ApplicationGesture.Left:
                case ApplicationGesture.Right:
                case ApplicationGesture.Curlicue:
                case ApplicationGesture.DoubleCurlicue:
                    answerResult = AnswerResult.Incorrect;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            wavesPlayer.Play(answerResult.ToString());

            var gestureCanvas = (InkCanvas)sender;
            var question = (Question)gestureCanvas.DataContext;
            question.Result = answerResult;

            gestureCanvas.Strokes.Clear();
            gestureCanvas.Strokes.Add(e.Strokes);
        }
    }
}
