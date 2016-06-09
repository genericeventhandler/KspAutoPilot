using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSPAutoPilot
{
    public interface IKspEngine
    {
        bool IsActive { get; }

        void TakeOff(int orbit, Action<string> updateInformation);

        void WaitForTaskToEnd();
    }
}
