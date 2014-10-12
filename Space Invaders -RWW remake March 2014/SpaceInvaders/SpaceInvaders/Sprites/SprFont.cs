using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Sprites
{
    class SprFont: TextureObj
    {
        private SpriteFont font;
        public SprFont(SpriteFont sf)
        {
            font = sf;
        }

        public SpriteFont Instance
        {
            get
            {
                return font;
            }
        }
    }
}
