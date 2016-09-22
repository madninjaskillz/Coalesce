using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coalesce;
using CoalesceInputPlugin;
using CoalesceTypes;
using LightBuzz.Vitruvius;
using Microsoft.Kinect;

namespace CoalesceInputKinectV2
{
    public class CoalesceInputKinectV2 : ICoalesceInputPlugin
    {
        private KinectMonitor monitor = null;
        private List<SensorReadingPackage> readings = new List<SensorReadingPackage>();
        private List<SensorDetails> sensors = new List<SensorDetails>();
        PlayersController _playersController;

        KinectSensor sensor = null;

        public PlugInDetails GetDetails()
        {
            return new PlugInDetails
            {
                Author = "James Johnston",
                Id = Guid.Parse("0a72e481-6976-48e8-94d8-28b1e3bb6f82"),
                ShortName = "Kinect V2",
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
                    ProvidesPosition = true,
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
            // Kinect sensor initialization
            sensor = KinectSensor.GetDefault();

            sensor?.Open();

            var reader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color |
                                             FrameSourceTypes.Depth |
                                             FrameSourceTypes.Infrared |
                                             FrameSourceTypes.Body);
            reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;

            _playersController = new PlayersController();
            _playersController.BodyEntered += UserReporter_BodyEntered;
            _playersController.BodyLeft += UserReporter_BodyLeft;
            _playersController.Start();
        }

        private void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();

            // Color
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (monitor != null)
                    {
                        monitor.viewer.Image = frame.ToBitmap();

                    }
                }
            }

            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    var bodies = frame.Bodies();
                    _playersController?.Update(bodies);

                    foreach (Body body in bodies)
                    {
                        monitor?.viewer.DrawBody(body);
                    }

                    var currentBody = bodies.FirstOrDefault(y => y.IsTracked);
                    if (currentBody != null)
                    {

                        {
                            foreach (KeyValuePair<JointType, Joint> joint in currentBody.Joints)
                            {
                                SensorDetails sensorDetails = sensors.First(t => t.SensorName == joint.Key.ToString());

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
                                    X = joint.Value.Position.X,
                                    Y = joint.Value.Position.Y,
                                    Z = joint.Value.Position.Z
                                };
                            }
                        }
                    }
                }
            }
        }
        void UserReporter_BodyEntered(object sender, PlayersControllerEventArgs e)
        {
            // A new user has entered the scene.
        }

        void UserReporter_BodyLeft(object sender, PlayersControllerEventArgs e)
        {
            monitor?.viewer.Clear();
        }


        public void ShowConfig()
        {
            throw new NotImplementedException();
        }

        public void ShowMonitor()
        {
                monitor = new KinectMonitor();
         

            monitor.Show();
        }

        public string GetConfig()
        {
            return "noConfig";
        }

        public void SetConfig(string config)
        {

        }
    }
}
