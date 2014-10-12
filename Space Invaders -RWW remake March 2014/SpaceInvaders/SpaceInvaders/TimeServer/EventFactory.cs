using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.TimeServer
{
    class EventFactory
    {
        private static readonly EventFactory instance = new EventFactory();
        private Event[] pool;
        private int poolsz;

        private EventFactory()
        {
            poolsz = 40;
            pool = new Event[poolsz];
            for (int i = 0; i < poolsz; i++)
            {
                pool[i] = new Event();
            }

        }

        public static EventFactory Instance
        {
            get
            {
                return instance;
            }
        }

        //returns inactive event
        public Event getEvent()
        {
            bool eventFound = false;
            Event ret;
            for (int i = 0; i < poolsz; i++)
            {
                if (!pool[i].isActive())
                {
                    eventFound = true;
                    pool[i].activate();
                    return pool[i];
                }
            }

            if (!eventFound)
            {
                ret = new Event();
                ret.activate();
            }
            else
            { // should never be reached
                ret = null;
            }
            return ret;

        }

        //unloads all events from pool
        public void unload()
        {
            pool = null;
        }

    }
}
