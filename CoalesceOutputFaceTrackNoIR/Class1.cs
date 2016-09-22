using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoalesceOutputPlugin;
using CoalesceTypes;

namespace CoalesceOutputFaceTrackNoIR
{
    public class FaceTrackNoIR : ICoalesceOutputPlugin
    {
        public CoalescePlugInDetails GetDetails()
        {
            return new CoalescePlugInDetails
            {
                Id = Guid.Parse("{B72D6804-CF67-4967-A7B2-A6A5603389D9}"),
                ShortName = "FaceTrackNoIR UDP Sender",
                Author = "James Johnston",
                SupportLink = "n/a",
                Version = 0.1
            };
        }

        public void Initialise()
        {
            
        }

        public void ShowConfig()
        {
            
        }

        public void ShowMonitor()
        {
            
        }

        public string GetConfig()
        {
            return "";
        }

        public void SetConfig(string config)
        {
            
        }

        public void AcceptPoll(List<JointPackage> packages)
        {
            throw new NotImplementedException();
        }
    }
}
