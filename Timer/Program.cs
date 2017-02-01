using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
namespace Timer
{
    class TimerExampleState
    {
        public int counter = 0;
        public System.Threading.Timer tmr;
    }


    class Program
    {
        static void Main(string[] args)
        {
            TimerExampleState s = new TimerExampleState();

            // Create the delegate that invokes methods for the timer.
            TimerCallback timerDelegate = new TimerCallback(CheckStatus);

            // Create a timer that waits one second, then invokes every second.
            System.Threading.Timer timer = new System.Threading.Timer(timerDelegate, s, 1000, 1000);

            // Keep a handle to the timer, so it can be disposed.
            s.tmr = timer;

            // The main thread does nothing until the timer is disposed.
            while(s.tmr != null)
                Thread.Sleep(0);
            Console.WriteLine("Timer example done.");
            Console.ReadKey();

        }
        static void CheckStatus(object state)
        {
            TimerExampleState s = (TimerExampleState)state;
            s.counter++;
            Console.WriteLine("{0} Checking Status {1}.", DateTime.Now.TimeOfDay, s.counter);
            if(s.counter == 2)
            {
                // Shorten the period. Wait 10 seconds to restart the timer.
                (s.tmr).Change(1000, 1000);
                Console.WriteLine("changed...");
            }
            if(s.counter == 10)
            {
                Console.WriteLine("disposing of timer...");
                s.tmr.Dispose();
                s.tmr = null;
            }
        }
    }
}
