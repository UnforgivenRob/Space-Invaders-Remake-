using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.TimeServer
{
    class Event
    {
        private del callback;
        private bool active;

        public delegate void del();

        public Event()
        {
            callback = null;
            active = false;
        }

        //adds a callback to event
        public void addCallBack(del Callback)
        {
            callback = Callback;
        }

        //returns whether or not event is currently active
        public bool isActive()
        {
            return active;
        }

        //activates event
        public void activate()
        {
            active = true;
        }

        //deactivates instance of event
        public void deactivate()
        {
            callback = null;
            active = false;
        }

        //processes event's callback
        public void process()
        {
            callback();
            deactivate();
        }
    }
}
