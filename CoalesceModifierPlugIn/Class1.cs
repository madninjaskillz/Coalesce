using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoalesceTypes;

namespace CoalesceModifierPlugIn
{
    public interface ICoalesceModifierPlugIn : ICoalescePlugin
    {

        void Initialise();
        void ShowConfig();
        string GetConfig();
        void SetConfig(string config);

    }


}
