using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoalesceTypes;

namespace CoalesceOutputPlugin
{
    public interface ICoalesceOutputPlugin
    {
        OutputPlugInDetails GetDetails();

        void Initialise();
        void ShowConfig();
        void ShowMonitor();

        string GetConfig();
        void SetConfig(string config);

        void AcceptPoll(List<JointPackage> packages);
    }

    public class OutputPlugInDetails
    {
        public Guid Id { get; set; }
        public string ShortName { get; set; }
        public string Author { get; set; }
        public string SupportLink { get; set; }
        public double Version { get; set; }
    }
}
