using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Collision;
using SpaceInvaders.Sprites;


namespace SpaceInvaders.GameObjects
{
    class AlienFactory
    {
        private CollidableManager cm;
        private GameObjManager gom;
        private int AlienX;
        private int AlienY;
        private int AlienWidth;
        private int AlienHeight;

        private static readonly AlienFactory instance = new AlienFactory();
        private AlienFactory()
        {
            cm = CollidableManager.CurrentInstance;
            gom = GameObjManager.CurrentInstance;
            AlienX = 50;
            AlienY = 150;
            AlienWidth = 39;
            AlienHeight = 26;
        }

        public static AlienFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public void createGrid(CollisionGroup group, int level, GraphicsDevice g)
        {
            cm.addSimpleCollidable(GOKey.Grid, new Rectangle(0, 0, 0, 0), Color.Blue, g);
            gom.addSuper(GOKey.Grid, cm.getCollidable(GOKey.Grid), Vector2.Zero, 11);
            SuperGObj grid = (SuperGObj)gom.getGameObject(GOKey.Grid);
            populateGrid(grid, AlienY + (36*level), g);
            group.add(grid);
            grid.setGroup(group);
        }

        private void populateGrid(SuperGObj grid, int AlienY, GraphicsDevice g)
        {
            for (int i = 0; i < 11; i++)
            {
                GOKey key = GOKey.Column1 + i;
                cm.addSimpleCollidable(key, new Rectangle(AlienX + 45 * i, AlienY, 0, 0), Color.Wheat, g);
                gom.addSuper(key, cm.getCollidable(key), new Vector2(AlienX + 45 * i, AlienY), 5);
                SuperGObj column = (SuperGObj)gom.getGameObject(key);
                grid.add(column);
                populateColumn(column, i + 1, AlienY, g);
                grid.update();
            }
            grid.update();

        }

        private void populateColumn(SuperGObj column, int columnNumber, int AlienY, GraphicsDevice g)
        {
            GOKey key = GOKey.Alien1_1 + (columnNumber - 1) * 5;
            cm.addSimpleCollidable(key, new Rectangle(AlienX + 45*columnNumber + 6, AlienY , AlienWidth - 12, AlienHeight), Color.Red, g);
            gom.addAlien(key, SpriteKey.Alien1, new Vector2(AlienX + 45 * columnNumber + 2, AlienY - 4), cm.getCollidable(key), cm.getCollidable(key).pos, new Vector2(20, 0), 30);
            cm.addSimpleCollidable(key + 1, new Rectangle(AlienX + 45 * columnNumber + 1, AlienY + 36, AlienWidth - 3, AlienHeight), Color.Red, g);
            gom.addAlien(key + 1, SpriteKey.Alien2, new Vector2(AlienX + 45 * columnNumber - 3, AlienY + 32), cm.getCollidable(key + 1), cm.getCollidable(key + 1).pos, new Vector2(20, 0), 20);
            cm.addSimpleCollidable(key + 2, new Rectangle(AlienX + 45 * columnNumber + 1, AlienY + 36 * 2, AlienWidth - 3, AlienHeight), Color.Red, g);
            gom.addAlien(key + 2, SpriteKey.Alien2, new Vector2(AlienX + 45 * columnNumber - 3, AlienY + 36*2 - 4), cm.getCollidable(key + 2), cm.getCollidable(key + 2).pos, new Vector2(20, 0), 20);
            cm.addSimpleCollidable(key + 3, new Rectangle(AlienX + 45 * columnNumber, AlienY + 36 * 3, AlienWidth, AlienHeight), Color.Red, g);
            gom.addAlien(key + 3, SpriteKey.Alien3, new Vector2(AlienX + 45 * columnNumber - 4, AlienY + 36*3 - 4), cm.getCollidable(key + 3), cm.getCollidable(key + 3).pos, new Vector2(20, 0), 10);
            cm.addSimpleCollidable(key + 4, new Rectangle(AlienX + 45 * columnNumber, AlienY + 36 * 4, AlienWidth, AlienHeight), Color.Red, g);
            gom.addAlien(key + 4, SpriteKey.Alien3, new Vector2(AlienX + 45 * columnNumber - 4, AlienY + 36*4 - 4), cm.getCollidable(key + 4), cm.getCollidable(key + 4).pos, new Vector2(20, 0), 10);
            column.add(gom.getGameObject(key));
            column.add(gom.getGameObject(key+1));
            column.add(gom.getGameObject(key+2));
            column.add(gom.getGameObject(key+3));
            column.add(gom.getGameObject(key+4)); 
            column.update();
        }
    }
}
