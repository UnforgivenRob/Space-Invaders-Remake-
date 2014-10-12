using System;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Sprites
{
    //class handling image and attribs of a sprite
    class Sprite : DisplayObj
    {
        public  Image image;
        public Vector2 pos { get; set; }
        public Color color { get; set; } 
        private float rotation;
        public Vector2 scale { get; set; } 
        private SpriteEffects effects;
        public float depth { get; set; }
        private Vector2 origin;
        private SpriteBatchC SB;

        public Sprite(Image Image, Vector2 Position, Color Color, Single Rotation, Vector2 origin, Vector2 Scale, SpriteEffects Effects, float Depth)
        {
            image = Image;
            pos = Position;
            color = Color;
            rotation = Rotation;
            scale = Scale;
            effects = Effects;
            depth = Depth;
            this.origin = origin;
        }

        //gets SB associated with sprite
        public SpriteBatchC getSB()
        {
            return SB;
        }

        //returns SB associated with sprite
        public void setSB(SpriteBatchC sb)
        {
            SB = sb;
        }

        // draws sprite for current attribs
        public void draw(SpriteBatch sb)
        {
            sb.Draw(image.texture(), pos, image.sourceRectangle(), color, rotation, origin, scale, effects, depth);
        }

        // draws sprite at specified pos
        public void draw(SpriteBatch sb, Vector2 pos)
        {
            sb.Draw(image.texture(), pos, image.sourceRectangle(), color, rotation, origin, scale, effects, depth);
        }

        public void draw(SpriteBatch sb, Vector2 pos, Color color)
        {
            sb.Draw(image.texture(), pos, image.sourceRectangle(), color, rotation, origin, scale, effects, depth);
        }

        public void update(Vector2 delta)
        {
        }
    }
}
