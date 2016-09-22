using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoalesceModifierPlugIn
{
    public interface ICoalesceModifierPlugIn
    {

        void Initialise();
        void ShowConfig();
        string GetConfig();
        void SetConfig(string config);

    }


}
