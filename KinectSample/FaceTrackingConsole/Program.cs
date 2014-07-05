using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var tracker = new SkeletonFaceTracker();

            var isTracked = false;
            tracker.IsSkeletonTrackedUpdated += b =>
            {
                var isTracked_old = isTracked;
                isTracked = b;
                if (isTracked_old != isTracked)
                {
                    Console.WriteLine(isTracked ? "Tracked" : "Not Tracked");
                }
            };

            var jawLower = JawLowerState.None;
            tracker.JawLowerUpdated += v =>
            {
                var jawLower_old = jawLower;
                jawLower =
                    !v.HasValue ? JawLowerState.None :
                    v.Value < 0.5 ? JawLowerState.Closed :
                    JawLowerState.Open;
                if (jawLower_old != jawLower)
                {
                    Console.WriteLine(jawLower);
                }
            };

            Console.WriteLine("Press [Enter] key to exit.");
            Console.ReadLine();
        }

        enum JawLowerState
        {
            None,
            Closed,
            Open,
        }
    }
}
