using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Sprites
{
    enum AnimKey
    {
        None,
        Alien1,
        Alien2,
        Alien3,
        Bomb1,
        Bomb2,
        ShipExplosion,
    }

    class AnimationManager
    {
        private static readonly AnimationManager instance1 = new AnimationManager();
        private static readonly AnimationManager instance2 = new AnimationManager();
        private static AnimationManager currentInstance = instance1;
        private AnimLL animations;
        private AnimationManager()
        {
            animations = new AnimLL();
        }

        public static AnimationManager CurrentInstance
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

        public void addAnimation(AnimKey key, int frames, int intervalMS, Image[] images, Sprite sprite)
        {
            animations.add(key, new Animation(frames, intervalMS, images, sprite));
        }

        public void removeAnimation(AnimKey key)
        {
            animations.remove(key);
        }

        public Animation getAnimation(AnimKey key)
        {
            return animations.getAnimation(key);
        }

        public void unload()
        {
            animations.clear();
        }
        
        
        //LL class to hold Animations
        internal class AnimLL
        {
            private node head = new node(AnimKey.None, null);
            private node tail = new node(AnimKey.None, null);

            public AnimLL()
            {
                head.next = tail;
            }

            //adds Animation to LL, replaces value if key already in LL
             public void add(AnimKey key, Animation A)
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
                    node n = new node(key, A);
                    n.next = head.next;
                    head.next = n;
                }
                else
                {
                    r.a = A;
                }
            }

            //remove animation of a give key
            public void remove(AnimKey key)
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

            //returns animation associated with key
            public Animation getAnimation(AnimKey key)
            {
                node r = head;
                while (r != tail)
                {
                    if (r.key == key)
                    {
                        return r.a;
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
                public AnimKey key;
                public Animation a;

                public node(AnimKey Key, Animation A)
                {
                    key = Key;
                    a = A;
                }
            }

        }   
    }
}