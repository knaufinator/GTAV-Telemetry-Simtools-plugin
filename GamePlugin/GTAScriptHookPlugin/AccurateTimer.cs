using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace GTAHook
{

    //very accurate timer needed for calculating real time accelerations
    internal class AccurateTimer
    {
        private const int TIME_PERIODIC = 1;
        private const int EVENT_TYPE = 1;
        private Action mAction;
        private int mTimerId;
        private AccurateTimer.TimerEventDel mHandler;

        [DllImport("winmm.dll")]
        private static extern int timeBeginPeriod(int msec);

        [DllImport("winmm.dll")]
        private static extern int timeEndPeriod(int msec);

        [DllImport("winmm.dll")]
        private static extern int timeSetEvent(
          int delay,
          int resolution,
          AccurateTimer.TimerEventDel handler,
          IntPtr user,
          int eventType);

        [DllImport("winmm.dll")]
        private static extern int timeKillEvent(int id);

        public AccurateTimer(Action action, int delay)
        {
            this.mAction = action;
            AccurateTimer.timeBeginPeriod(1);
            this.mHandler = new AccurateTimer.TimerEventDel(this.TimerCallback);
            this.mTimerId = AccurateTimer.timeSetEvent(delay, 0, this.mHandler, IntPtr.Zero, 1);
        }

        public void Stop()
        {
            AccurateTimer.timeKillEvent(this.mTimerId);
            AccurateTimer.timeEndPeriod(1);
            Thread.Sleep(100);
        }

        private void TimerCallback(int id, int msg, IntPtr user, int dw1, int dw2)
        {
            if (this.mTimerId == 0)
                return;
            this.mAction();
        }

        private delegate void TimerEventDel(int id, int msg, IntPtr user, int dw1, int dw2);
    }
}
