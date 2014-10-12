using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Collision;
using SpaceInvaders.Sprites;

namespace SpaceInvaders.Shield
{
    class ShieldFactory
    {
        private TextureManager tm;
        private CollidableManager cm;
        private GameObjManager gom;

        private static readonly ShieldFactory instance = new ShieldFactory();
        private ShieldFactory()
        {
            tm = TextureManager.Instance;
            cm = CollidableManager.CurrentInstance;
            gom = GameObjManager.CurrentInstance;
        }

        public static ShieldFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public Shield Create(GraphicsDevice g, SpriteBatchC sb, GOKey name, Rectangle bounds, int rows, int columns, int BrickDurability)
        {
            int brickWidth = bounds.Width/columns;
            int brickHeight = bounds.Height/rows;
            Texture2D[] ex = new Texture2D[3];
            ex[0] = ((Text2D)tm.getTexture(TextKey.Explosion1).getTexture()).Instance;
            ex[1] = ((Text2D)tm.getTexture(TextKey.Explosion2).getTexture()).Instance;
            ex[2] = ((Text2D)tm.getTexture(TextKey.Explosion3).getTexture()).Instance;
            ShieldSprite ss = new ShieldSprite(((Text2D)tm.getTexture(TextKey.Shield).getTexture()).Instance, ex, ((Text2D)tm.getTexture(TextKey.Erase).getTexture()).Instance, bounds, g);
            ConcreteSpritePos cs = new ConcreteSpritePos(ss, new Vector2(bounds.X, bounds.Y));
            sb.addSprite(cs);
            cm.addSimpleCollidable(GOKey.Shield1, bounds, Color.Blue, g);
            Shield s = new Shield(name, cs, cm.getCollidable(GOKey.Shield1), bounds);
            //create columns and bricks
            ShieldColumn[] scs = new ShieldColumn[columns];
            for (int i = 0; i < columns; i++)
            {
                int brickX = bounds.X + i * brickWidth;
                scs[i] = new ShieldColumn(new Vector2(brickX, bounds.Y), cm.addSimpleCollidable(Rectangle.Empty, Color.Wheat, g), rows);
                for (int j = 0; j < rows; j++)
                {
                    if(checkMid(i, j, rows, columns)) break;
                    int brickY = bounds.Y + j * brickHeight;
                    ShieldBrick brick = new ShieldBrick(new Vector2(brickX, brickY), cm.addSimpleCollidable(new Rectangle(brickX, brickY, brickWidth, brickHeight), Color.Red, g), 3);
                    scs[i].add(brick);
                }
                scs[i].update();
            }
            s.add(scs);
            return s;
        }

        private bool checkMid(int i, int j, int rows, int columns)
        {
            if (!(i < 1 || i > columns - 2) && (j == rows - 1)) return true;
            else return false;
        }
    }
}
