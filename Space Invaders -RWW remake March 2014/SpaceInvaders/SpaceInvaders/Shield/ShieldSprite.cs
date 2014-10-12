using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Sprites;

namespace SpaceInvaders.Shield
{
    class ShieldSprite : DisplayObj
    {
        private Rectangle bounds;
        private Rectangle textbounds;
        Texture2D text;
        Texture2D[] explosions;
        Texture2D Erase;
        int exSz;
        RenderTarget2D renderTarget;
        BlendState bs;
        Random r;
        SpriteBatchC sb;
        SpriteBatch spriteBatch;
        GraphicsDevice g;

        public ShieldSprite(Texture2D text, Texture2D[] explosions, Texture2D erase, Rectangle bounds, GraphicsDevice g)
        {
            this.g = g;
            this.text = text;
            this.explosions = explosions;
            Erase = erase;
            this.bounds = bounds;
            textbounds = text.Bounds;
            exSz = explosions.Length;
            renderTarget = new RenderTarget2D(g, text.Width, text.Height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
            bs = new BlendState();
            initializeBS(bs);
            r = new Random();
            //should probably extract this line
            spriteBatch = new SpriteBatch(g);
            initialize();
        }

        //initializes BlendState blending state for rendering
        private void initializeBS(BlendState bs)
        {
             bs.AlphaBlendFunction = BlendFunction.Add;
             bs.AlphaDestinationBlend = Blend.InverseSourceAlpha;
             bs.AlphaSourceBlend = Blend.Zero;
             bs.BlendFactor = Color.White;
             bs.ColorBlendFunction = BlendFunction.Add;
             bs.ColorDestinationBlend = Blend.InverseSourceAlpha;
             bs.ColorSourceBlend = Blend.Zero;
        }

        //initializes render target with shield texture
        public void initialize()
        {
            g.SetRenderTarget(renderTarget);
            g.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(text, Vector2.Zero, Color.White);
            spriteBatch.End();
            g.SetRenderTarget(null);
        }

        //renders target with explosion
        public void render(Vector2 pos)
        {
            int i = r.Next(0, exSz);
            pos.X = (textbounds.Width * (pos.X - bounds.X)) / bounds.Width;
            pos.Y = (textbounds.Height * (pos.Y - bounds.Y)) / bounds.Height;
            g.SetRenderTarget(renderTarget);
            spriteBatch.Begin(SpriteSortMode.Deferred, bs);
            spriteBatch.Draw(explosions[i], pos, Color.White);
            spriteBatch.End();
            g.SetRenderTarget(null);
        }

        //renders target with erase texture
        public void erase(Vector2 pos)
        {
            pos.X = (textbounds.Width  * (pos.X- bounds.X)) / bounds.Width;
            pos.Y = (textbounds.Height * (pos.Y - bounds.Y)) / bounds.Height;
            g.SetRenderTarget(renderTarget);
            spriteBatch.Begin(SpriteSortMode.Deferred, bs);
            spriteBatch.Draw(Erase, pos, Color.White);
            spriteBatch.End();
            g.SetRenderTarget(null);
        }        

        //sets SpriteBatchC associate with this sprite
        public void setSB(SpriteBatchC sb)
        {
            this.sb = sb;
        }
        //returns associated SpriteBatchC
        public SpriteBatchC getSB()
        {
            return sb;
        }

        public void draw(SpriteBatch sb)
        {
            sb.Draw(renderTarget, bounds, Color.White);
        }
        public void draw(SpriteBatch sb, Vector2 pos)
        {
            sb.Draw(renderTarget, bounds, Color.White);
        }
        public void draw(SpriteBatch sb, Vector2 pos, Color color)
        {
            bounds.X = (int)pos.X;
            bounds.Y = (int)pos.Y;
            sb.Draw(renderTarget, bounds, Color.White);
        }
        public void update(Vector2 delta)
        {
        }
    }
}
