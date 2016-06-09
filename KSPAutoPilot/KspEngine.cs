using System;
using System.Threading.Tasks;

namespace KSPAutoPilot
{
    public class KspEngine : IKspEngine
    {
        private readonly bool async;

        private readonly IKrpcHandler handler;
        private Task currentTask;

        public KspEngine(IKrpcHandler handler) : this(true, handler)
        {
        }

        public KspEngine(bool async, IKrpcHandler handler)
        {
            this.async = async;
            this.handler = handler;
            if (handler != null)
            {
                handler.Message += MessageFromHandler;
            }
        }

        public bool IsActive { get; private set; }

        public string LastMessage { get; set; }

        public void TakeOff(int orbit, Action<string> updateInformation)
        {
            if (async)
            {
                var t = Task.Factory.StartNew(() => TakeOffInternal(orbit, updateInformation));
                SetCurrentTask(t);
            }
            else
            {
                TakeOffInternal(orbit, updateInformation);
            }
        }

        public void WaitForTaskToEnd()
        {
            if (currentTask != null)
            {
                currentTask.Wait();
            }

            this.IsActive = false;
        }

        private void MessageFromHandler(object sender, EventArgs<string> e)
        {
            if (sender != null && e != null)
            {
                this.LastMessage = e.Value;
            }
        }

        private void SetCurrentTask(Task t)
        {
            this.currentTask = t;
            this.IsActive = true;
        }

        private void TakeOffInternal(int orbit, Action<string> updateInformation)
        {
            if(updateInformation == null)
            {
                throw new ArgumentNullException(nameof(updateInformation));
            }

            updateInformation("Take off!");

            var vessel = handler.GetActiveVessel();
            if (vessel != null)
            {
                updateInformation(vessel.Name);
            }
        }
    }
}