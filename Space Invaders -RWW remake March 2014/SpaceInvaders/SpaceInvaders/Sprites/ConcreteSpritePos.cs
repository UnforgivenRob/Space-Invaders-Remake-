using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Sprites
{
    class ConcreteSpritePos : SpriteProxy
    {

        private Vector2 pos;
        DisplayObj obj;

        public ConcreteSpritePos(DisplayObj s, Vector2 Pos)
            : base(s)
        {
            pos = Pos;
            obj = s;
        }

        public override void update(Vector2 delta)
        {
            pos += delta;
            obj.update(delta);
        }

        public Vector2 position()
        {
            return pos;
        }

        public override void draw(SpriteBatch sb)
        {
            obj.draw(sb, pos);
        }

        public override void draw(SpriteBatch sb, Vector2 pos)
        {
            obj.draw(sb, pos);
        }

        public override void draw(SpriteBatch sb, Vector2 pos, Color color)
        {
            obj.draw(sb, pos, color);
        }




    }

}
