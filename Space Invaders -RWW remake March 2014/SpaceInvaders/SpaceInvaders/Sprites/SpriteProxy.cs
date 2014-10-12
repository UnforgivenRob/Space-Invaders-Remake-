using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SpaceInvaders.Shield;
using SpaceInvaders.Collision;

namespace SpaceInvaders.Sprites
{
    abstract class SpriteProxy : DisplayObj
    {
        private DisplayObj sprite;
        private SpriteBatchC SB;

        protected SpriteProxy(DisplayObj sprite)
        {
            this.sprite = sprite;
        }

        //sets SB associated with proxy
        public void setSB(SpriteBatchC sb)
        {
            SB = sb;
        }

        public virtual TextSprite getTextSprite()
        {
            return (TextSprite)sprite;
        }

        //returns SB associated with proxy
        public SpriteBatchC getSB()
        {
            return SB;
        }

        public Sprite getSprite()
        {
            return (Sprite)sprite;
        }

        public CollisionSprite getCollisionSprite()
        {
            return (CollisionSprite)sprite;
        }

        public ShieldSprite getShieldSprite()
        {
            return (ShieldSprite)sprite;
        }

        public abstract void update(Vector2 delta);
        public abstract void draw(SpriteBatch sb);
        public abstract void draw(SpriteBatch sb, Vector2 pos);
        public abstract void draw(SpriteBatch sb, Vector2 pos, Color color);
    }
}
