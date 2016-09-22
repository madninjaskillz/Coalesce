using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using coalesce;
using CoalesceInputPlugin;
using Microsoft.Kinect;

namespace CoalesceInputKinectV1
{
    public class CoalesceInputKinectV1 : ICoalesceInputPlugin
    {
        private KinectSensor sensor;
        private List<SensorReadingPackage> readings = new List<SensorReadingPackage>();
        private List<SensorDetails> sensors = new List<SensorDetails>();


     

        public PlugInDetails GetDetails()
        {
            return new PlugInDetails
            {
                Author = "James Johnston",
                Id = Guid.Parse("0a72e481-6976-48e8-94d8-28b1e3bb6f81"),
                ShortName = "Kinect V1",
                SupportLink = "n/a",
                Version = 0.1
            };
        }

        public List<SensorDetails> GetSensors()
        {
            foreach (JointType joint in Enum.GetValues(typeof(JointType)))
            {
                sensors.Add(new SensorDetails
                {
                    Id = (int)joint,
                    SensorName = joint.ToString(),
                    ProvidesOrientation = false,
                    ProvidesPosition = true
                });
            }

            return sensors;
        }

        public List<SensorReadingPackage> GetReadings()
        {
            return readings;
        }

        public void Initialise()
        {
            foreach (KinectSensor potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
                this.sensor.SkeletonStream.Enable();
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                try
                {
                    this.sensor.Start();
                    GetSensors();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
            }
        }

        private Int64 counter = 0;
        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
            }

            var temp = skeletons.FirstOrDefault(x => x.TrackingState == SkeletonTrackingState.Tracked);

            if (temp != null)
            {
                foreach (Joint joint in temp.Joints)
                {
                    SensorDetails sensorDetails = sensors.First(t => t.SensorName == joint.JointType.ToString());

                    SensorReadingPackage package = readings.FirstOrDefault(x => x.Id == sensorDetails.Id);

                    if (package == null)
                    {
                        package = new SensorReadingPackage
                        {
                            Id = sensorDetails.Id,
                        };

                        readings.Add(package);
                    }

                    package.Position = new PositionalReading()
                    {
                        X = joint.Position.X,
                        Y = joint.Position.Y,
                        Z = joint.Position.Z
                    };
                }

                kinectMonitor?.DoSimpleDraw(readings, sensors);

            }

        }

      

        public void ShowConfig()
        {
            throw new NotImplementedException();
        }

        private KinectMonitor kinectMonitor;
        public void ShowMonitor()
        {
            if (kinectMonitor == null)
            {
                kinectMonitor = new KinectMonitor();
            }

            kinectMonitor.Show();
            kinectMonitor.Width = 680;
            kinectMonitor.Height = 540;
        }
    }
}
