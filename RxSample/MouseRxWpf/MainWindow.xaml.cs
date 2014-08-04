﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MouseRxWpf
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

            const double π = Math.PI;
            var orientationSymbols = new[] { "→", "↘", "↓", "↙", "←", "↖", "↑", "↗" };
            var zoneAngleRange = 2 * π / orientationSymbols.Length;

            // Replace events with IObservable objects.
            var mouseDown = Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseDown").Select(e => e.EventArgs);
            var mouseUp = Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseUp").Select(e => e.EventArgs);
            var mouseLeave = Observable.FromEventPattern<MouseEventArgs>(this, "MouseLeave").Select(e => e.EventArgs);

            mouseDown.Select(e => e.GetPosition(this))
                .SelectMany(p => mouseUp.Select(e => e.GetPosition(this)).Take(1),
                    (p1, p2) => new { Start = p1, End = p2 })
                .Do(o => Debug.WriteLine(o))
                .Select(o => o.End - o.Start)
                .Where(d => d.Length >= 100)
                .Select(d => 2 * π + Math.Atan2(d.Y, d.X))
                .Select(angle => (int)Math.Round(angle / zoneAngleRange) % orientationSymbols.Length)
                .Do(zone => Orientation = orientationSymbols[zone])
                .Subscribe();
        }
    }
}
