using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Sprites
{
    enum SBKey
    {
        None,
        Aliens1,
        Aliens2,
        Aliens3,
        Background,
        Debug1,
        Foreground,
        Game1,
    }

    class SpriteBatchManager
    {
        
        private static readonly SpriteBatchManager instance = new SpriteBatchManager();
        private SpriteBatchLL spriteBatches;
        
        private SpriteBatchManager()
        {
            spriteBatches = new SpriteBatchLL();
        }

        public static SpriteBatchManager Instance
        {
            get
            {
                return instance;
            }
        }

        //adds a spritebatch to manager
        public void add(SBKey key, SpriteBatchC sb)
        {
            spriteBatches.add(key, sb);
        }

        //removes a spritebatch from manager
        public void remove(SBKey key)
        {
            spriteBatches.remove(key);
        }


        public SpriteBatchC getSB(SBKey key)
        {
            return spriteBatches.getSB(key);
        }

        //draws content in all of the SpriteBatches
        public void drawAll()
        {
            spriteBatches.draw();
        }

        //clears all content from LL
        public void unload()
        {
            spriteBatches.clear();
        }

    }

    //LL class to hold Spritebatches
    internal class SpriteBatchLL
    {
        private node head = new node(SBKey.None, null);
        private node tail = new node(SBKey.None, null);

        public SpriteBatchLL()
        {
            head.next = tail;
        }

        //adds image to LL, replaces value if key already in LL
        public void add(SBKey key, SpriteBatchC SB)
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
                node n = new node(key, SB);
                n.next = r.next;
                r.next = n;
            }
            else
            {
                r.next.sb = SB;
            }
        }

        //remove spriteBatch of a give key
        public void remove(SBKey key)
        {
            node r = head;
            while (r.next != tail)
            {
                if (r.next.key == key)
                {
                    r.next = r.next.next;
                }
                else r = r.next;
            }
        }

        //returns spritebatch associated with key
        public SpriteBatchC getSB(SBKey key)
        {
            node r = head;
            while (r != tail)
            {
                if (r.key == key)
                {
                    return r.sb;
                }
                r = r.next;
            }

            return null;
        }

        //draws each spritebatch in LL
        public void draw()
        {
            node r = head.next;
            while (r != tail)
            {
                r.sb.draw();
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
            public SBKey key;
            public SpriteBatchC sb;

            public node(SBKey Key, SpriteBatchC SB)
            {
                key = Key;
                sb = SB;
            }
        }
    }
}
