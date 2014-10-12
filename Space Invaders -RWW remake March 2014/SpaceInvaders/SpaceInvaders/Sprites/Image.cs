using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Sprites
{
    class Image
    {
        private Texture2D text;
        private Rectangle source;

        public Image (Texture2D Texture, Rectangle Source)
        {
            text = Texture;
            source = Source;
        }

        public Rectangle sourceRectangle()
        {
            return source;
        }

        public Texture2D texture()
        {
            return text;
        }
    }
}
