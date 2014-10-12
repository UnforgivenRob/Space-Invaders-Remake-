using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Sprites
{
    enum TextKey
    {
        None,
        Aliens,
        Missiles,
        Missiles2,
        Shield,
        Explosion1,
        Explosion2,
        Explosion3,
        ShipExplosion,
        Erase,
        Other,
        Font1,
        Font2,
        Rectangle,
    }

    enum TextType
    {
        T2D,
        SF
    }

    enum FormatType
    {
        JPEG,
        PNG,
        GIF,
        BMP,
        RAW,
        TIFF,
        XNF
    }

    //Class that manages the loading, storage and unloading of textures
    class TextureManager
    {
        /// <summary>
        /// Class that manages the various textures used in the game
        /// </summary>


        private static readonly TextureManager instance = new TextureManager();
        private static TextureLL textures;
        private float sz;
        private int count = 0;
        
        private TextureManager()
        {
          textures = new TextureLL();

        }

        public static TextureManager Instance
        {
            get
            {
                return instance;
            }
        }

        public Text getTexture(TextKey key)
        {
            return textures.getTexture(key);
        }

        // loads a Texture obj
        public void load(String AssetName, TextKey key, float byteSize, FormatType Format, bool alpha, TextType type, Game game)
        {
            sz += byteSize;
            count++;
            switch (type)
            {
                case TextType.T2D:
                    Text2D Texture = new Text2D(game.Content.Load<Texture2D>(AssetName));
                    Text Text = new Text(Texture, AssetName, key, byteSize, Format, alpha, type);
                    textures.add(key, Text);
                    break;
                case TextType.SF:
                    SprFont sf = new SprFont(game.Content.Load<SpriteFont>(AssetName));
                    Text spr = new Text(sf, AssetName, key, byteSize, Format, alpha, type);
                    textures.add(key, spr);
                    break;
                default:
                    throw new ArgumentException("Illegal Type");
            }
        }

        public void unload(TextKey key, Game game)
        {
            Text ret = textures.remove(key);
            count--;
            sz -= ret.size();
        }

        //clears all textures loaded
        public void unloadAllTextures(Game game)
        {
            textures.clear();
            game.Content.Unload();
        }

        //returns total number of textures
        public int NumTexts()
        {
            return count;
        }

        //computes total size of all texture
        public float totalSize()
        {
            return sz;
        }
    }

    //LL class to hold Texture2Ds
    internal class TextureLL
    {
        private node head = new node(0, null);
        private node tail = new node(0, null);

        public TextureLL()
        {
            head.next = tail;
        }

        //adds textureobj to LL, replaces value if key already in LL
        public void add(TextKey key, Text T)
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
                node n = new node(key, T);
                n.next = head.next;
                head.next = n;
            }
            else
            {
                r.t = T;
            }
        }

        //remove texture of a give key
        public Text remove(TextKey key)
        {
            node r = head;
            Text ret = null;
            while (r.next != tail)
            {
                if (r.next.key == key)
                {
                    ret = r.next.t;
                    r.next = r.next.next;
                }
                r = r.next;
            }
            return ret;
        }

        //returns texture associated with key
        public Text getTexture(TextKey key)
        {
            node r = head;
            while (r != tail)
            {
                if (r.key == key)
                {
                    return r.t;
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
            public TextKey key;
            public Text t;

            public node(TextKey Key, Text T)
            {
                key = Key;
                t= T;
            }
        }
    }
}
