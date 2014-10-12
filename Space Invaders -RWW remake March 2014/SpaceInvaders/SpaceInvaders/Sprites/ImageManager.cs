using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Sprites
{
    //enum for image keys
    enum ImageKey
    {
        None,
        Alien1A,
        Alien1B,
        Alien2A,
        Alien2B,
        Alien3A,
        Alien3B,
        UFO,
        Ship,
        Bomb1A,
        Bomb1B,
        Bomb2A,
        Bomb2B,
        BOmb3,
        Missile,
        Logo,
        Explosion1,
        Explosion2a,
        Explosion2b,
        Rectangle,
    }

    //class to manage Images. Currently not used
    class ImageManager
    {

        private static readonly ImageManager instance = new ImageManager();
        private ImageLL images;
        private ImageManager()
        {
            images = new ImageLL();
        }

        public static ImageManager Instance
        {
            get
            {
                return instance;
            }
        }

        //creates the image and adds it to LL
        public void addImage(ImageKey key, Texture2D texture, Rectangle source)
        {
            Image i = new Image(texture, source);
            images.add(key, i);
        }

        //retrives image for given key
        public Image getImage(ImageKey key)
        {
            return images.getImage(key);
        }

        //unloads all content from LL
        public void unload()
        {
            images.clear();
        }
    }

    //LL class to hold iamges
    internal class ImageLL
    {
        private node head = new node(ImageKey.None, null);
        private node tail = new node(ImageKey.None, null);

        public ImageLL()
        {
            head.next = tail;
        }

        //adds image to LL, replaces value if key already in LL
        public void add(ImageKey key, Image I)
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

        //remove image of a give key
        public void remove(ImageKey key)
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

        //returns image associated with key
        public Image getImage(ImageKey key)
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
            public ImageKey key;
            public Image i;

            public node(ImageKey Key, Image I)
            {
                key = Key;
                i = I;
            }
        }
    }
}
