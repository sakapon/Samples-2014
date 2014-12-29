using Microsoft.Kinect;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KinectAsyncWpf2
{
    public class AsyncKinectManager : IDisposable
    {
        public KinectSensor Sensor { get; private set; }

        public AsyncKinectManager(Action<KinectSensor> initialize)
        {
            if (KinectSensor.KinectSensors.Count == 0) return;

            Sensor = KinectSensor.KinectSensors[0];
            Task.Run(() => initialize(Sensor));
        }

        ~AsyncKinectManager()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Sensor != null)
                {
                    try
                    {
                        Sensor.Stop();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
            }
        }
    }
}
