using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.Sprites;

namespace SpaceInvaders.Screens
{
    class ScreenManager
    {
        private ScreenLL screens;
        SpriteBatchManager sbm;
        private SpriteBatchC sb;
        private Screen first;

        private static readonly ScreenManager instance = new ScreenManager();
        private ScreenManager()
        {
            screens = new ScreenLL();
            sbm = SpriteBatchManager.Instance;
        }

        public static ScreenManager Instance
        {
            get
            {
                return instance;
            }
        }

        //sets associated spritebatch
        public void setSB(SpriteBatchC sb)
        {
            this.sb = sb;
        }

        //returns associated spritebatch
        public SpriteBatchC getSB()
        {
            return sb;
        }

        public void setFirst(Screen s)
        {
            first = s;
        }

        public void add(Screen s)
        {
            screens.add(s);
        }

        public void remove(Screen s)
        {
            screens.remove(s);
        }

        public void start()
        {
            first.start();
        }

        //updates screens and draws active ones
        public void update()
        {
            screens.update();
        }

        public void draw()
        {
            sbm.drawAll();
        }

        public void unload()
        {
            screens.clear();
        }


        //LL class to hold iamges
        internal class ScreenLL
        {
            private node head = new node(null);
            private node tail = new node(null);
            private node[] pool;

            public ScreenLL()
            {
                head.next = tail;
                //ideally should hold 85 to hold all Collidables in game, 20 will do for demo
                pool = new node[20];
                for (int i = 0; i < 20; i++)
                {
                    pool[i] = new node(null);
                }

            }

            //retrieve available node from pool
            private node getNode(Screen s)
            {
                bool nodeFound = false;
                for (int i = 0; i < 20; i++)
                {
                    if (!pool[i].active)
                    {
                        nodeFound = true;
                        pool[i].active = true;
                        pool[i].s = s;
                        return pool[i];
                    }
                }

                if (!nodeFound)
                {
                    return new node(s);
                }
                else
                {
                    //should never be reached
                    return null;
                }
            }

            //adds Collidable to LL, replaces value if key already in LL
            public void add(Screen s)
            {
                node r = head;
                bool contains = false;
                while (r != tail)
                {
                    if (r.s == s)
                    {
                        contains = true;
                        break;
                    }
                    r = r.next;
                }

                if (!contains)
                {
                    node n = getNode(s);
                    n.next = head.next;
                    head.next = n;
                }
                else
                {
                    r.s = s;
                }
            }

            //remove GameObject for a give key
            public void remove(Screen s)
            {
                node r = head;
                while (r.next != tail)
                {
                    if (r.next.s == s)
                    {
                        r.next.active = false;
                        r.next = r.next.next;
                        break;
                    }
                    r = r.next;
                }
            }


            //updates all objs
            public void update()
            {
                node r = head.next;
                while (r != tail)
                {
                    r.s.update();
                    r = r.next;
                }
            }

            //clears LL
            public void clear()
            {
                head.next = tail;
                //pool = null;
            }

            private class node
            {
                public node next;
                public Screen s;
                public bool active;

                public node(Screen s)
                {
                    this.s = s;
                    active = false;
                }
            }
        }
    }
}
