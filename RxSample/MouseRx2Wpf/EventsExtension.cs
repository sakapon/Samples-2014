using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MouseRx2Wpf
{
    public class EventsExtension
    {
        public Control Target { get; }
        public IObservable<IObservable<Vector>> MouseDrag { get; }

        public EventsExtension(Control target)
        {
            Target = target;

            // Replace events with IObservable objects.
            var mouseDown = Observable.FromEventPattern<MouseButtonEventArgs>(Target, nameof(Target.MouseDown)).Select(e => e.EventArgs);
            var mouseUp = Observable.FromEventPattern<MouseButtonEventArgs>(Target, nameof(Target.MouseUp)).Select(e => e.EventArgs);
            var mouseLeave = Observable.FromEventPattern<MouseEventArgs>(Target, nameof(Target.MouseLeave)).Select(e => e.EventArgs);
            var mouseMove = Observable.FromEventPattern<MouseEventArgs>(Target, nameof(Target.MouseMove)).Select(e => e.EventArgs);
            var mouseDownEnd = mouseUp.Merge(mouseLeave.Select(e => default(MouseButtonEventArgs)));

            MouseDrag = mouseDown
                .Select(e => e.GetPosition(Target))
                .Select(p0 => mouseMove
                    .Select(e => e.GetPosition(Target) - p0)
                    .TakeUntil(mouseDownEnd));
        }
    }
}
