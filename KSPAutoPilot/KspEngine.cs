using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSPAutoPilot
{
    public class KspEngine : IKspEngine
    {
        private readonly bool async;

        private Task currentTask = null;

        public KspEngine()
        {
            this.async = true;
        }

        public KspEngine(bool async)
        {
            this.async = async;
        }

        public bool IsActive { get; private set; }
                
        public void TakeOff(int orbit, Action<string> updateInformation)
        {
            if (async)
            {
                var t = Task.Factory.StartNew(() => TakeOffInternal(orbit, updateInformation));
                SetCurrentTask(t);
            } else
            {
                TakeOffInternal(orbit, updateInformation);
            }
        }

        private void TakeOffInternal(int orbit, Action<string> updateInformation)
        {
            if (updateInformation != null)
            {
                updateInformation("Take off!");
            }
        }

        private void SetCurrentTask(Task t)
        {
            this.currentTask = t;
            this.IsActive = true;
        }

        public void WaitForTaskToEnd()
        {
            if(currentTask != null)
            {
                currentTask.Wait();
            }

            this.IsActive = false;
        }
    }
}
