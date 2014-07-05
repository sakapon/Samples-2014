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
            tracker.JawLowerUpdated += v =>
            {
                Console.WriteLine(v);
            };

            Console.WriteLine("Press [Enter] key to exit.");
            Console.ReadLine();
        }
    }
}
