using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InkGestureWpf
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var gestureResults = new ObservableCollection<ApplicationGesture>();
            GestureResultsBox.ItemsSource = gestureResults;

            // EditingMode プロパティを GestureOnly に設定するだけで、すべてのジェスチャが有効になります。
            // 有効にするジェスチャを明示的に指定するには、SetEnabledGestures メソッドを利用します。
            //GestureCanvas.SetEnabledGestures(new[] { ApplicationGesture.Circle, ApplicationGesture.Check });

            GestureCanvas.Gesture += (o, e) =>
            {
                var result = e.GetGestureRecognitionResults()
                    .Where(r => r.RecognitionConfidence == RecognitionConfidence.Strong)
                    .FirstOrDefault(r => r.ApplicationGesture != ApplicationGesture.NoGesture);
                if (result == null) return;

                gestureResults.Insert(0, result.ApplicationGesture);
            };
        }
    }
}
