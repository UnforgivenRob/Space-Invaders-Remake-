using System;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace SpaceInvaders.TimeServer
{
    class TimeServerLL
    {
        private node head = new node(new TimeSpan(), null);
        private node tail = new node (new TimeSpan(), null);
        private node[] pool;

        public TimeServerLL()
        {
            head.next = tail;
            pool = new node[50];
            for (int i = 0; i < 50; i++)
            {
                pool[i] = new node(new TimeSpan(), null);
            }

        }

        //retrieve available node from pool
        private node getNode(TimeSpan t, Event e)
        {
            bool nodeFound = false;
            for (int i = 0; i < 50; i++)
            {
                if (!pool[i].active)
                {
                    nodeFound = true;
                    pool[i].active = true;
                    pool[i].e = e;
                    pool[i].key = t;
                    return pool[i];
                }
            }

            if (!nodeFound)
            {
                return new node(t, e);
            }
            else
            {
                //should never be reached
                return null;
            }
        }

        //adds the event to the LL,sorting by the key numerically
        public void add(TimeSpan key, Event E)
        {
            node n = getNode(key, E);
            node r = head;
            while (r.next != tail)
            {
                if (r.next.key.CompareTo(key) < 0) r = r.next;
                else break;
            }
            n.next = r.next;
            r.next = n;
        }
        //removes a certain event
        public void dequeue(Event e)
        {
            node r = head;
            while (r.next != tail)
            {
                if (r.next.e == e)
                {
                    r.next.active = false;
                    r.next = r.next.next;
                    break;
                    
                }
                r = r.next;
            }
        }

        //removes the Event with the lowest key
        public Event remove()
        {
            if (head.next == tail)
            {
                Debug.Assert(false, "LL is empty");
                return null;
            }
            else
            {
                node ret = head.next;
                head.next = head.next.next;
                ret.active = false;
                return ret.e;
            }
        }

        //returns value of Lowest key, throws exception if empty
        public TimeSpan getLowestKey()
        {
            if (head.next == tail)
            {
                Debug.Assert(false, "LL is empty");
                return TimeSpan.Zero;
            }
            else
            {
                return head.next.key;
            }

        }

        public void clear()
        {
            head.next = tail;
        }

        private class node
        {
            public node next;
            public TimeSpan key;
            public Event e;
            public bool active;

            public node(TimeSpan Key, Event E)
            {
                key = Key;
                e = E;
                active = false;
            }
        }
    }
}
