using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Collision
{

    public enum GOType
    {
        None,
        Super,
        Alien,
        Ship,
        UFO,
        Missile,
        Bomb,
        Shield,
        Wall,
        Top,
        Bottom,
    }

    class CollisionGroupManager
    {
        private static readonly CollisionGroupManager instance1 = new CollisionGroupManager();
        private static readonly CollisionGroupManager instance2 = new CollisionGroupManager();
        private static CollisionGroupManager currentInstance = instance1;

        private ColGroupLL groups;
        
        private CollisionGroupManager()
        {
            groups = new ColGroupLL();
        }

        public static CollisionGroupManager CurrentInstance
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

        //adds a group to manager
        public void add(GOType key, GraphicsDevice gd)
        {
            CollisionGroup g = new CollisionGroup(key, gd);
            groups.add(key, g);
        }

        //returns group for a given key
        public CollisionGroup getGroup(GOType key)
        {
            return groups.getGroup(key);
        }

        //clears all content from LL
        public void unload()
        {
            groups.clear();
        }

    }

    //LL class to hold CollisionPairs
    internal class ColGroupLL
    {
        private node head = new node(GOType.None, null);
        private node tail = new node(GOType.None, null);

        public ColGroupLL()
        {
            head.next = tail;
        }

        //adds group to LL, replaces value if key already in LL
        public void add(GOType key, CollisionGroup g)
        {
            node r = head;
            bool contains = false;
            while (r.next != tail)
            {
                if (r.next.key == key)
                {
                    contains = true;
                    break;
                }
                r = r.next;
            }

            if (!contains)
            {
                node n = new node(key, g);
                n.next = r.next;
                r.next = n;
            }
            else
            {
                r.g = g;
            }
        }

        //remove group of a give key
        public void remove(GOType key)
        {
            node r = head;
            while (r.next != tail)
            {
                if (r.next.key == key)
                {
                    r.next = r.next.next;
                }
                r = r.next;
            }
        }

        //returns group associated with key
        public CollisionGroup getGroup(GOType key)
        {
            node r = head;
            while (r != tail)
            {
                if (r.key == key)
                {
                    return r.g;
                }
                r = r.next;
            }

            return null;
        }

        //clears LL
        public void clear()
        {
            head.next = tail;
        }

        private class node
        {
            public node next;
            public GOType key;
            public CollisionGroup g;

            public node(GOType Key, CollisionGroup g)
            {
                key = Key;
                this.g = g;
            }
        }
    }
}
