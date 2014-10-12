using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Collision
{
    public enum ColPairKey
    {
        None,
        AlienWall,
        AlienMissile,
        AlienShield,
        AlienShip,
        AlienBottom,
        ShipBomb,
        ShipWall,
        ShieldBomb,
        ShieldMissile,
        UFOMissile,
        UFOWall,
        MissileTop,
        MissileBomb,
        BombBottom,

    }

    class CollisionPairManager
    {
        private static readonly CollisionPairManager instance1 = new CollisionPairManager();
        private static readonly CollisionPairManager instance2 = new CollisionPairManager();
        private static CollisionPairManager currentInstance = instance1;
        private ColPairLL pairs;
        
        private CollisionPairManager()
        {
            pairs = new ColPairLL();
        }

        public static CollisionPairManager CurrentInstance
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
        //adds a spritebatch to manager
        public void add(ColPairKey key, CollisionGroup a, CollisionGroup b)
        {
            CollisionPair pair = new CollisionPair(key, a, b);
            pairs.add(key, pair);
        }

        //returns pair for a given key
        public CollisionPair getpair(ColPairKey key)
        {
            return pairs.getPair(key);
        }

        //collides all pairs
        public void CollideAll()
        {
            pairs.Collide();
        }

        //clears all content from LL
        public void unload()
        {
            pairs.clear();
        }

    }

    //LL class to hold CollisionPairs
    internal class ColPairLL
    {
        private node head = new node(ColPairKey.None, null);
        private node tail = new node(ColPairKey.None, null);

        public ColPairLL()
        {
            head.next = tail;
        }

        //adds pair to LL, replaces value if key already in LL
        public void add(ColPairKey key, CollisionPair pair)
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
                node n = new node(key, pair);
                n.next = r.next;
                r.next = n;
            }
            else
            {
                r.pair = pair;
            }
        }

        //remove ColPair of a give key
        public void remove(ColPairKey key)
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

        //returns pair associated with key
        public CollisionPair getPair(ColPairKey key)
        {
            node r = head;
            while (r != tail)
            {
                if (r.key == key)
                {
                    return r.pair;
                }
                r = r.next;
            }

            return null;
        }

        //collides each pair
        public void Collide()
        {
            node r = head.next;
            while (r != tail)
            {
                r.pair.Collide();
                r = r.next;
            }
        }

        //clears LL
        public void clear()
        {
            head.next = tail;
        }

        private class node
        {
            public node next;
            public ColPairKey key;
            public CollisionPair pair;

            public node(ColPairKey Key, CollisionPair pair)
            {
                key = Key;
                this.pair = pair;
            }
        }
    }
}
