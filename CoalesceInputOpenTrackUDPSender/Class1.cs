using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoalesceInputPlugin;

namespace CoalesceInputOpenTrackUDPSender
{
    public class Class1 : ICoalesceInputPlugin
    {
        public PlugInDetails GetDetails()
        {
            return new PlugInDetails
            {
                Author = "James Johnston",
                Id = Guid.Parse("0a72e481-6976-48e8-94d8-28b1e3bb6f83"),
                ShortName = "OpenTrack UDP",
                SupportLink = "n/a",
                Version = 0.1
            };
        }

        public List<SensorDetails> GetSensors()
        {
            return new List<SensorDetails>()
            {
                new SensorDetails()
                {
                    Id = 1,
                    SensorName = "OpenTrack",
                    ProvidesOrientation=true,
                    ProvidesPosition = true,
                }
            };
        }

        public List<SensorReadingPackage> GetReadings()
        {
            //TODO
            return new List<SensorReadingPackage>();
        }

        public void Initialise()
        {
            //TODO
        }

        public void ShowConfig()
        {
            //TODO
        }

        public void ShowMonitor()
        {
            //TODO
        }

        public string GetConfig()
        {
            return "noConfig";
        }

        public void SetConfig(string config)
        {
            //TODO
        }
    }
}
