using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Sprites;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Collision
{

    public enum ColKey
    {
        None,
        Alien1,
        Alien2,
        Alien3,
        Alien4,
        Alien5,
        Alien6,
        Alien7,
        Alien8,
        Alien9,
        Column1,
        Column2,
        Column3,
        Column4,
        Grid1,
        Ship,
        UFO,
        Shield1,
        Missile1,
        Missile2,
        Missile3,
        Missile4,
        Missile5,
        Bomb1,
        Bomb2,
        Bomb3,
        Bomb4,
        Bomb5,
        LWall,
        RWall,
        Top,
        Bot,
    }

    class CollidableManager
    {
        private SimpleCollisionObjLL active;
        private SimpleCollisionObjLL inactive;
        private SpriteBatchC debug;

        private static readonly CollidableManager instance1 = new CollidableManager();
        private static readonly CollidableManager instance2 = new CollidableManager();
        private static CollidableManager currentInstance = instance1;

        private CollidableManager()
        {
            active = new SimpleCollisionObjLL();
            inactive = new SimpleCollisionObjLL();
        }

        public static CollidableManager CurrentInstance
        {
            get
            {
                return currentInstance;
            }
        }

        //sets the SpriteBatchC the manager adds CollisionSprites to
        public void setSpriteBatch(SpriteBatchC sb)
        {
            debug = sb;
        }

        public void switchInstance()
        {
            if (currentInstance == instance1) currentInstance = instance2;
            else currentInstance = instance1;
        }

        //adds simple Collision Obj to LL
        public void addSimpleCollidable(GOKey key, Rectangle r, Color color, GraphicsDevice g)
        {
            SimpleCollisionObj c = new SimpleCollisionObj(key, r, color, g);
            active.add(key, c );
            debug.addSprite(c.getSprite());
        }

        public SimpleCollisionObj addSimpleCollidable(Rectangle r, Color color, GraphicsDevice g)
        {
            SimpleCollisionObj c = new SimpleCollisionObj(GOKey.none, r, color, g);
            active.add(c.Name(), c);
            debug.addSprite(c.getSprite());
            return c;
        }

        //activates a deactivated SimpleCollisionObj
        public void activate(SimpleCollisionObj c)
        {
            inactive.remove(c);
            active.add(c.Name(), c);
            debug.addSprite(c.getSprite());
        }

        //deactivates an active SimpleCollisionObj
        public void deactivate(SimpleCollisionObj c)
        {
            active.remove(c);
            //c.Group().deactivate(c);
            inactive.add(c.Name(), c);
            debug.removeSprite(c.getSprite());
        }

        //returns SimpleCollisionObj for a given key
        public SimpleCollisionObj getCollidable(GOKey key)
        {
            return active.getSimpleCollisionObj(key);
        }


        //clears data
        public void unload()
        {
            active.clear();
            inactive.clear();
        }

    }

    //LL class to hold iamges
    internal class SimpleCollisionObjLL
    {
        private node head = new node(GOKey.none, null);
        private node tail = new node(GOKey.none, null);
        private node[] pool;

        public SimpleCollisionObjLL()
        {
            head.next = tail;
            pool = new node[20];
            for (int i = 0; i < 20; i++)
            {
                pool[i] = new node(GOKey.none , null);
            }

        }

        //retrieve available node from pool
        private node getNode(GOKey key, SimpleCollisionObj c)
        {
            bool nodeFound = false;
            for (int i = 0; i < 20; i++)
            {
                if (!pool[i].active)
                {
                    nodeFound = true;
                    pool[i].active = true;
                    pool[i].c = c;
                    pool[i].key = key;
                    return pool[i];
                }
            }

            if (!nodeFound)
            {
                return new node(key, c);
            }
            else
            {
                //should never be reached
                return null;
            }
        }

        //adds SimpleCollisionObj to LL, replaces value if key already in LL
        public void add(GOKey key, SimpleCollisionObj c)
        {
            node r = head;
            bool contains = false;
            while (r != tail)
            {
                if (r.key == key)
                {
                    contains = true;
                    break;
                }
                r = r.next;
            }

            if (!contains)
            {
                node n = getNode(key, c);
                n.next = head.next;
                head.next = n;
            }
            else
            {
                r.c = c;
            }
        }

        //remove SimpleCollisionObj for a give key
        public SimpleCollisionObj remove(SimpleCollisionObj obj)
        {
            node r = head;
            SimpleCollisionObj ret = null;
            while (r.next != tail)
            {
                if (r.next.c == obj)
                {
                    ret = r.next.c;
                    r.next.active = false;
                    r.next = r.next.next;
                    break;
                }
                r = r.next;
            }
            return ret;
        }

        //returns SimpleCollisionObj associated with key
        public SimpleCollisionObj getSimpleCollisionObj(GOKey key)
        {
            node r = head;
            while (r != tail)
            {
                if (r.key == key)
                {
                    return r.c;
                }
                r = r.next;
            }
            return null;
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
            public GOKey key;
            public SimpleCollisionObj c;
            public bool active;

            public node(GOKey Key, SimpleCollisionObj C)
            {
                key = Key;
                c = C;
                active = false;
            }
        }
    }
}
