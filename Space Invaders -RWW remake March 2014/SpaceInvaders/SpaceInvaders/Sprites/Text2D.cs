using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceInvaders.Sprites
{
    class Text2D : TextureObj
    {
        private Texture2D texture;
        public Text2D(Texture2D text)
        {
            texture = text;
           
        }

        public Texture2D Instance
        {
            get
            {
                return texture;
            }
        }
    }
}
