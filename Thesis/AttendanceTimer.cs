using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;

namespace Thesis
{
    class TimerExampleState
    {
        public int counter = 0;
        public Timer tmr;
        public bool late = false;
    }
    class AttendanceTimer
    {
        private int dueTime;
        public TimerExampleState status;
        public AttendanceTimer(int dueTime)
        {
            this.dueTime = dueTime;
        }
        public void start()
        {
            status = new TimerExampleState();

            // Create the delegate that invokes methods for the timer.
            TimerCallback timerDelegate = new TimerCallback(CheckStatus);

            // Create a timer that waits one second, then invokes every second.
            Timer timer = new Timer(timerDelegate, status, 1000, 1000);
           
            // Keep a handle to the timer, so it can be disposed.
            status.tmr = timer;

            // The main thread does nothing until the timer is disposed.
            while(status.tmr != null)
                Thread.Sleep(0);

            //Console.WriteLine("Timer example done.");
            //Console.ReadKey();
        }
        public void CheckStatus(object state)
        {
            TimerExampleState status = (TimerExampleState)state;
            status.counter++;
            //Console.WriteLine("{0} Checking Status {1}.", DateTime.Now.TimeOfDay, status.counter);
            //if(s.counter == 2)
            //{
            //    // Shorten the period. Wait 10 seconds to restart the timer.
            //    (s.tmr).Change(1000, 1000);
            //    Console.WriteLine("changed...");
            //}
            if(status.counter == dueTime)
            {
                //Console.WriteLine("disposing of timer...");
                status.tmr.Dispose();
                status.tmr = null;
                status.late = true;
            }
        }
    }
}