using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Sprites
{
    // Class containing data on sprite font and text with attribs
    class TextSprite : DisplayObj
    {
        private SpriteFont font;
        private string txt;
        public Vector2 pos { get; set; }
        public Color color { get; set; }
        public Vector2 scale { get; set; }
        private Vector2 origin;
        private float rotation;
        private SpriteEffects effects;
        public float depth { get; set; }
        private SpriteBatchC SB;

        public TextSprite(SpriteFont Font, string text, Vector2 position, Color Color, float Rotation, Vector2 Origin, Vector2 Scale, SpriteEffects Effects, float Depth)
        {
            font = Font;
            txt = text;
            pos = position;
            color = Color;
            rotation = Rotation;
            origin = Origin;
            scale = Scale;
            effects = Effects;
            depth = Depth;
        }

        //sets SB associated with textsprite
        public void setSB(SpriteBatchC sb)
        {
            SB = sb;
        }

        //returns SB associated with textsprite
        public SpriteBatchC getSB()
        {
            return SB;
        }

        public void setText(String s)
        {
            txt = s;
        }

        //draws TextSprite
        public void draw(SpriteBatch sb)
        {
            sb.DrawString(font, txt, pos, color, rotation, origin, scale, effects, depth);
        }
        
        //draws TextSprtie at given pos
        public void draw(SpriteBatch sb, Vector2 pos)
        {
            sb.DrawString(font, txt, pos, color, rotation, origin, scale, effects, depth);
        }

        //draws TextSprtie at given pos
        public void draw(SpriteBatch sb, Vector2 pos, Color color)
        {
            sb.DrawString(font, txt, pos, color, rotation, origin, scale, effects, depth);
        }

        public void update(Vector2 deltaX)
        {
        }
    }
}
