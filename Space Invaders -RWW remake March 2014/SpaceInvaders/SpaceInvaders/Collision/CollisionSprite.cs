using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Sprites;

namespace SpaceInvaders.Collision
{
    class CollisionSprite : DisplayObj
    {
        public Vector2 pos;
        public int width;
        public int height;
        private VertexPositionColor[] vertices;
        private Color boxColor;
        private SpriteBatchC SB;
        private BasicEffect basicEffect;

        public CollisionSprite(Rectangle r, Color BoxColor, GraphicsDevice g)
        {
            //set bounds of box
            pos.X = r.Left;
            pos.Y = r.Top;
            width = r.Width;
            height = r.Height;

            //set vertices of display box
            vertices = new VertexPositionColor[5];
            boxColor = BoxColor;

            //set effect
            basicEffect = new BasicEffect(g);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, g.Viewport.Width, g.Viewport.Height, 0, 0, 1);
            
        }

        //sets SB associated with colsprite
        public void setSB(SpriteBatchC sb)
        {
            SB = sb;
        }

        //returns SB associated with colsprite
        public SpriteBatchC getSB()
        {
            return SB;
        }

        //accessors for bounds of box
        public float left()
        {
            return pos.X;
        }
        public float right()
        {
            return pos.X + width;
        }
        public float top()
        {
            return pos.Y;
        }
        public float bottom()
        {
            return pos.Y + height;
        }

        //draws the collision sprite, uses Draw()
        public void draw(SpriteBatch sb)
        {
            Draw(sb.GraphicsDevice, pos);
        }

        //draws the collision sprite, uses Draw()
        public void draw(SpriteBatch sb, Vector2 pos)
        {
            Draw(sb.GraphicsDevice, pos);
        }

        public void draw(SpriteBatch sb, Vector2 pos, Color color)
        {
            Draw(sb.GraphicsDevice, pos);
        }

        //updates pos of sprite by delta
        public void update(Vector2 delta)
        {
            pos += delta;
        }

        //updates size of sprite
        public void update(Rectangle r)
        {
            pos.X = r.Left;
            pos.Y = r.Top;
            width = r.Width;
            height = r.Height;
        }
        
        //draws the Collision Sprite for given pos
        private void Draw(GraphicsDevice g, Vector2 pos )
        {
           
            basicEffect.CurrentTechnique.Passes[0].Apply();
            vertices[0].Color = boxColor;
            vertices[0].Position.X = pos.X;
            vertices[0].Position.Y = pos.Y;
            vertices[1].Color = boxColor;
            vertices[1].Position.X = pos.X;
            vertices[1].Position.Y = pos.Y + height;
            vertices[2].Color = boxColor;
            vertices[2].Position.X = pos.X + width;
            vertices[2].Position.Y = pos.Y + height;
            vertices[3].Color = boxColor;
            vertices[3].Position.X = pos.X + width;
            vertices[3].Position.Y = pos.Y;
            vertices[4] = vertices[0];
            g.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vertices, 0, 4);
        }
    }
}
