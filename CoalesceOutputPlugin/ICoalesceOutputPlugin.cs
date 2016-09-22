using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoalesceTypes;

namespace CoalesceOutputPlugin
{
    public interface ICoalesceOutputPlugin : ICoalescePlugin
    {
        

        void Initialise();
        void ShowConfig();
        void ShowMonitor();

        string GetConfig();
        void SetConfig(string config);

        void AcceptPoll(List<JointPackage> packages);
    }


}
