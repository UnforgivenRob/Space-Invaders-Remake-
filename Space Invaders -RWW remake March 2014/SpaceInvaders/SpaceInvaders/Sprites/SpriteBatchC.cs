using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Sprites
{
    //spritebatch class that holds 0...n sprites with capability to draw them
    class SpriteBatchC
    {
        private SpriteBatch sb;
        private SpriteSortMode sortmode;
        private BlendState blendstate;
        private SpriteList sprites;
        private int numSprites;

        public SpriteBatchC (SpriteBatch sb, SpriteSortMode sortmode, BlendState blendstate)
        {
            this.sb = sb;
            this.sortmode = sortmode;
            this.blendstate = blendstate;
            sprites = new SpriteList();
            numSprites = 0;
        }
        
        //adds sprite to LL
        public void addSprite(DisplayObj sprite)
        {
            sprites.add(sprite);
            sprite.setSB(this);
            numSprites++;
        }

        //Added in PA3, removes sprites from LL
        public void removeSprite(DisplayObj sprite)
        {
            sprites.remove(sprite);
        }

        public void draw()
        {
            sprites.draw(sb, sortmode, blendstate);
        }

        //class that holds sprites in order of insertion
        private class SpriteList
        {
            private node head = new node(null);
            private node tail = new node(null);
            private node[] pool;

            public SpriteList()
            {
                head.next = tail;
                pool = new node[50];
                for (int i = 0; i < 50; i++)
                {
                    pool[i] = new node(null);
                }
            }

            //retrieve available node from pool
            private node getNode(DisplayObj d)
            {
                bool nodeFound = false;
                for (int i = 0; i < 20; i++)
                {
                    if (!pool[i].active)
                    {
                        nodeFound = true;
                        pool[i].active = true;
                        pool[i].i = d;
                        return pool[i];
                    }
                }

                if (!nodeFound)
                {
                    return new node(d);
                }
                else
                {
                    //should never be reached
                    return null;
                }
            }

            //add sprite to end of LL
            public void add(DisplayObj s)
            {
                node n = getNode(s);
                node r = head;
                while (r.next != tail)
                {
                    r = r.next;
                }
                n.next = r.next;
                r.next = n;
            }

            //added in PA3, removes from LL
            public void remove(DisplayObj s)
            {
      
                node r = head;
                while (r.next != tail)
                {
                    if (r.next.i == s)
                    {
                        r.next.active = false;
                        r.next = r.next.next;
                        break;
                    }
                    r = r.next;
                }
            }


            //draws all sprites in list
            public void draw(SpriteBatch sb, SpriteSortMode sortmode, BlendState blendstate)
            {
                node r = head.next;
                sb.Begin(sortmode, blendstate);
                while (r != tail)
                {
                    r.i.draw(sb);
                    r = r.next;
                }
                sb.End();
            }

            private class node
            {
                public node next;
                public DisplayObj i;
                public bool active;

                public node(DisplayObj I)
                {
                    i = I;
                    active = false;
                }
            }
        }

    }
}
