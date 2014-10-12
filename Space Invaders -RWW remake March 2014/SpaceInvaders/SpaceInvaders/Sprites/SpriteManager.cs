using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Sprites
{
    enum SpriteKey
    {
        None,
        Alien1,
        Alien2,
        Alien3,
        UFO,
        UFO2,
        Missile,
        Bomb1,
        Bomb2,
        Bomb3,
        Ship1,
        Ship2,
        Ship3,
        UFOExplosion,
        AlienExplosion,
        MissileExplosion,
        ShipExplosion,
        TopStrip,
        BotStrip,

        DemoText,
        StartText1,
        StartText2,
        StartText3,
        SelectText,
        DisabledText,
        ScoreText,
        CreditText,
        GameOverText,
        P1Score,
        P2Score,
        HighScore,
        Lives,
        Credits,
    }

    class SpriteManager
    {

        private static readonly SpriteManager instance = new SpriteManager();
        private SpriteLL sprites;
        private SpriteManager()
        {
            sprites = new SpriteLL();
        }

        public static SpriteManager Instance
        {
            get
            {
                return instance;
            }
        }

        //adds a sprite to LL
        public void addSprite(SpriteKey key, Image Image, Vector2 Position, Color Color, Single Rotation, Vector2 origin, Vector2 Scale, SpriteEffects Effects, float Depth)
        {
            Sprite sprite = new Sprite(Image, Position, Color, Rotation, origin, Scale, Effects, Depth);
            sprites.add(key, sprite);
        }

        //adds a textSprite to LL
        public void addTextSprite(SpriteKey key, SpriteFont Font, string text, Vector2 position, Color Color, float Rotation, Vector2 Origin, Vector2 Scale, SpriteEffects Effects, float Depth)
        {
            TextSprite ts = new TextSprite(Font, text, position, Color, Rotation, Origin, Scale, Effects, Depth);
            sprites.add(key, ts);
        }

        //returns a spriteproxy for a given Sprite retrieved by key. Creates sprite proxy for given pos
        public SpriteProxy getSprite(SpriteKey key, Vector2 pos)
        {
            return new ConcreteSpritePos(sprites.getSprite(key), pos);
        }

        //unloads all content from LL
        public void unload()
        {
            sprites.clear();
        }
    }


    //LL class to hold sprites
    internal class SpriteLL
    {
        private node head = new node(SpriteKey.None, null);
        private node tail = new node(SpriteKey.None, null);

        public SpriteLL()
        {
            head.next = tail;
        }

        //adds sprite to LL, replaces value if key already in LL
        public void add(SpriteKey key, DisplayObj I)
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
                node n = new node(key, I);
                n.next = head.next;
                head.next = n;
            }
            else
            {
                r.i = I;
            }
        }

        //remove sprite of a give key
        public void remove(SpriteKey key)
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

        //returns whether or not this sprite has been retrieved already
        public bool beenRetrieved(SpriteKey key)
        {
            node r = head;
            while (r != tail)
            {
                if (r.key == key)
                {
                    if (r.retrieved == false) return false;
                    else return true;
                }
                r = r.next;
            }
            return false;
        }
        //returns sprite associated with key
        public DisplayObj getSprite(SpriteKey key)
        {
            node r = head;
            while (r != tail)
            {
                if (r.key == key)
                {
                    return r.i;
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
            public SpriteKey key;
            public DisplayObj i;
            public bool retrieved;

            public node(SpriteKey Key, DisplayObj I)
            {
                key = Key;
                i = I;
                retrieved = false;
            }
        }
    }
}
