using KRPC.Client;
using KRPC.Client.Services;
using KRPC.Client.Services.KRPC;
using KRPC.Client.Services.SpaceCenter;
using System;
using System.Net;

namespace KSPAutoPilot
{
    public class KrpcHandler : IKrpcHandler
    {
        Connection connection;
        public event EventHandler<EventArgs<string>> Message;

        public KrpcHandler(string name, string ipAddress, int rpcPort, int streamPort)
        {
            connection = new Connection(name, IPAddress.Parse(ipAddress), rpcPort, streamPort);
            var krpc = connection.KRPC();
            if(krpc != null)
            {
                OnMessage(krpc.GetStatus().Version);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // do clean up
                if(connection != null)
                {
                    connection = null;
                }
            }
        }

        private void OnMessage(string message)
        {
            var local = Message;
            if(local != null)
            {
                local(this, new EventArgs<string>(message));
            }
        }

        public Vessel GetActiveVessel()
        {
            if(connection == null)
            {
                return null;
            }

            var spaceCenter = GetSpaceCenter();
            if(spaceCenter == null)
            {
                return null;
            }

            return spaceCenter.ActiveVessel;
        }

        private KRPC.Client.Services.SpaceCenter.Service GetSpaceCenter()
        {
            if(connection == null)
            {
                return null;
            }

            return connection.SpaceCenter();
        }

        public string GetStatus()
        {
            throw new NotImplementedException();
        }
    }
}