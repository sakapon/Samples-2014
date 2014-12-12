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

            var recognizedGestures = new ObservableCollection<ApplicationGesture>();
            RecognizedGesturesBox.ItemsSource = recognizedGestures;

            // EditingMode プロパティを GestureOnly に設定するだけで、すべてのジェスチャが有効になります。
            // ジェスチャを個別に指定したい場合、SetEnabledGestures メソッドを利用します。
            //TheCanvas.SetEnabledGestures(new[] { ApplicationGesture.Circle, ApplicationGesture.Check });

            TheCanvas.Gesture += (o, e) =>
            {
                var results = e.GetGestureRecognitionResults();
                var result = results.FirstOrDefault(r => r.RecognitionConfidence == RecognitionConfidence.Strong);
                if (result == null || result.ApplicationGesture == ApplicationGesture.NoGesture) return;

                recognizedGestures.Insert(0, result.ApplicationGesture);
            };
        }
    }
}
