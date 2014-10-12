using System;

using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.TimeServer
{
    class TimeServer
    {
        private static readonly TimeServer instance1 = new TimeServer();
        private static readonly TimeServer instance2 = new TimeServer();
        private static TimeServer currentInstance = instance1;

        private TimeServerLL TS;
        private int numEvents;
        private TimeSpan minTime;
        private TimeSpan totalTime;
        
        private TimeServer()
        {
            TS = new TimeServerLL();
            numEvents = 0;
            minTime = new TimeSpan();
            totalTime = new TimeSpan();
            Event e = new Event();
            e.addCallBack(NullCallBack);
            enqueue(e, TimeSpan.Zero);
        }
        
        public static TimeServer CurrentInstance
        {
            get
            {
                return currentInstance;

            }
        }

        public void switchInstance()
        {
            if (currentInstance == instance1) currentInstance = instance2;
            else currentInstance = instance1;
        }

        //provides the current time
        public TimeSpan currentTime()
        {
            return totalTime;
        }

        //add an event to queue to be executed at waketime
        public void enqueue(Event e, TimeSpan wakeTime)
        {
            TS.add(wakeTime, e);
            numEvents++;
            if (wakeTime.CompareTo(minTime) < 0 || numEvents == 1) minTime = wakeTime;
        }

        //removes event from timeserver
        public void dequeue(Event e)
        {
            TS.dequeue(e);
            e.deactivate();
            numEvents--;
            if (numEvents == 0) { }
            else minTime = TS.getLowestKey();
        }

        //updates current time and processes any event whose waketime is reached
        public void update(GameTime gameTime)
        {
            totalTime = gameTime.TotalGameTime;
            if (numEvents == 0) { }
            else
            {
                while (minTime <= totalTime)
                {
                    if (numEvents == 0) break;
                    TS.remove().process();
                    Console.WriteLine(minTime);
                    numEvents--;
                    if (numEvents == 0) { }
                    else minTime = TS.getLowestKey();
                }
            }
        }

        //unloads all content from server
        public void unload()
        {
            TS.clear();
            numEvents = 0;
        }

        public void NullCallBack()
        {
        }
    }
}
