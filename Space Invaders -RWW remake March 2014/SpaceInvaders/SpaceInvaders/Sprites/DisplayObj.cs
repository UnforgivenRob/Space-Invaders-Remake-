using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Sprites
{
    interface DisplayObj
    {
        SpriteBatchC getSB();
        void setSB(SpriteBatchC sb);
        void draw(SpriteBatch sb);
        void draw(SpriteBatch sb, Vector2 pos);
        void draw(SpriteBatch sb, Vector2 pos, Color color);
        void update(Vector2 delta);
    }
}
