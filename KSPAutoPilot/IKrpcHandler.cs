using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KRPC.Client.Services.SpaceCenter;

namespace KSPAutoPilot
{
    public interface IKrpcHandler : IDisposable
    {
        event EventHandler<EventArgs<string>> Message;

        Vessel GetActiveVessel();

        string GetStatus();
    }
}
