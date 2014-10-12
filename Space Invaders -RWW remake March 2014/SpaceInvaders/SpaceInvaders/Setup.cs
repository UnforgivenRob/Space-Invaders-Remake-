using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Sprites;
using SpaceInvaders.Screens;
using SpaceInvaders.Shield;
using SpaceInvaders.Collision;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Sound;


namespace SpaceInvaders
{
    class Setup
    {
        private TextureManager tm;
        private ImageManager im;
        private SpriteManager sm;
        private SpriteBatchManager sbm;
        private AnimationManager am;
        private ScreenManager scm;
        private CollidableManager cm;
        private GameObjManager gom;
        private CollisionGroupManager cgm;
        private CollisionPairManager cpm;
        //private Scoreboard score;
        private SoundManager sound;
        private AlienFactory af;
        private ShieldFactory sf;
        private ScreenText st;
        private TimeServer.TimeServer ts;


        private static readonly Setup instance = new Setup();
        private Setup()
        {
            tm = TextureManager.Instance;
            im = ImageManager.Instance;
            sm = SpriteManager.Instance;
            sbm = SpriteBatchManager.Instance;
            //am = AnimationManager.CurrentInstance;
            scm = ScreenManager.Instance;
           // cm = CollidableManager.CurrentInstance;
            //gom = GameObjManager.CurrentInstance;
           // cgm = CollisionGroupManager.CurrentInstance;
           // cpm = CollisionPairManager.CurrentInstance;
            //score = Scoreboard.Instance;
            sf = ShieldFactory.Instance;
            af = AlienFactory.Instance;
            sound = SoundManager.Instance;
            st = new ScreenText();
            ts = TimeServer.TimeServer.CurrentInstance;
        }

        public static Setup Instance
        {
            get
            {
                return instance;
            }
        }

        public void load(Game g)
        {
            loadSound(g);
            loadTextures(g);
            loadImages(g);
            loadSprites(g);
          //  loadAnimation(g);
            loadSB(g);
            loadScreens(g);
          //  loadColGroups(g);
          //  loadColPairs(g);
           // loadCollision(g);
           // loadGameObjects(g);
        }

        public void gameUnload()
        {
            cgm = CollisionGroupManager.CurrentInstance;
            cgm.unload();
            cpm = CollisionPairManager.CurrentInstance;
            cpm.unload();
            cm = CollidableManager.CurrentInstance;
            cm.unload();
            gom = GameObjManager.CurrentInstance;
            gom.unload();
            ts = TimeServer.TimeServer.CurrentInstance;
            ts.unload();
            am = AnimationManager.CurrentInstance;
            am.unload();
        }

        public void gameLoad(Game g, int level)
        {
            loadAnimation(g, level);
            reloadSB(g);
            loadColGroups(g);
            loadColPairs(g);
            loadCollision(g);
            loadGameObjects(g, level);
            
        }

        //loads Textures
        public void loadTextures(Game g)
        {
            tm.load("SpriteFont1", TextKey.Font1, 2079, FormatType.XNF, true, TextType.SF, g);
            tm.load("SpaceInvadersFont", TextKey.Font2, 2079, FormatType.XNF, true, TextType.SF, g);
            tm.load("invaders-from-spaceAlpha2", TextKey.Aliens, 1436549, FormatType.PNG, true, TextType.T2D, g);
            tm.load("missiles", TextKey.Missiles, 17510, FormatType.PNG, true, TextType.T2D, g);
            tm.load("missiles2", TextKey.Missiles2, 17510, FormatType.PNG, true, TextType.T2D, g);
            tm.load("Shield", TextKey.Shield, 173056, FormatType.PNG, true, TextType.T2D, g);
            tm.load("Explosion1b", TextKey.Explosion1, 6431, FormatType.PNG, true, TextType.T2D, g);
            tm.load("Explosion2", TextKey.Explosion2, 40243, FormatType.PNG, true, TextType.T2D, g);
            tm.load("Explosion3B", TextKey.Explosion3, 40243, FormatType.PNG, true, TextType.T2D, g);
            tm.load("Erase", TextKey.Erase, 8295, FormatType.PNG, true, TextType.T2D, g);
            tm.load("ShipExplosion", TextKey.ShipExplosion, 8295, FormatType.PNG, true, TextType.T2D, g);
            //tm.load("Rect", TextKey.Rectangle, 8295, FormatType.PNG, true, TextType.T2D, g);
        }

        //loads Images
        public void loadImages(Game g)
        {
            im.addImage(ImageKey.Alien1A, ((Text2D)tm.getTexture(TextKey.Aliens).getTexture()).Instance, new Rectangle(110, 0, 115, 86));
            im.addImage(ImageKey.Alien1B, ((Text2D)tm.getTexture(TextKey.Aliens).getTexture()).Instance, new Rectangle(227, 0, 115, 86));
            im.addImage(ImageKey.Alien2A, ((Text2D)tm.getTexture(TextKey.Aliens).getTexture()).Instance, new Rectangle(344, 0, 90, 86));
            im.addImage(ImageKey.Alien2B, ((Text2D)tm.getTexture(TextKey.Aliens).getTexture()).Instance, new Rectangle(438, 0, 90, 86));
            im.addImage(ImageKey.Alien3A, ((Text2D)tm.getTexture(TextKey.Aliens).getTexture()).Instance, new Rectangle(534, 0, 122, 86));
            im.addImage(ImageKey.Alien3B, ((Text2D)tm.getTexture(TextKey.Aliens).getTexture()).Instance, new Rectangle(2, 136, 122, 86));
            im.addImage(ImageKey.UFO, ((Text2D)tm.getTexture(TextKey.Aliens).getTexture()).Instance, new Rectangle(97, 410, 115, 86));
            im.addImage(ImageKey.Ship, ((Text2D)tm.getTexture(TextKey.Aliens).getTexture()).Instance, new Rectangle(218, 410, 84, 86));
            im.addImage(ImageKey.Bomb1A, ((Text2D)tm.getTexture(TextKey.Missiles).getTexture()).Instance, new Rectangle(61, 8, 30, 47));
            im.addImage(ImageKey.Bomb1B, ((Text2D)tm.getTexture(TextKey.Missiles2).getTexture()).Instance, new Rectangle(61, 8, 30, 47));
            im.addImage(ImageKey.Bomb2A, ((Text2D)tm.getTexture(TextKey.Missiles).getTexture()).Instance, new Rectangle(24, 0, 38, 63));
            im.addImage(ImageKey.Bomb2B, ((Text2D)tm.getTexture(TextKey.Missiles2).getTexture()).Instance, new Rectangle(24, 0, 38, 63));
            im.addImage(ImageKey.Missile, ((Text2D)tm.getTexture(TextKey.Missiles).getTexture()).Instance, new Rectangle(0, 0, 25, 63));
            im.addImage(ImageKey.Explosion1, ((Text2D)tm.getTexture(TextKey.Aliens).getTexture()).Instance, new Rectangle(466, 422, 82, 74));
            im.addImage(ImageKey.Explosion2a, ((Text2D)tm.getTexture(TextKey.Aliens).getTexture()).Instance, new Rectangle(312, 413, 90, 83));
            im.addImage(ImageKey.Explosion2b, ((Text2D)tm.getTexture(TextKey.ShipExplosion).getTexture()).Instance, new Rectangle(0, 0, 90, 83));
            //im.addImage(ImageKey.Rectangle, ((Text2D)tm.getTexture(TextKey.Rectangle).getTexture()).Instance, new Rectangle(0, 0, 50, 50));
        }

        //load Sprites
        public void loadSprites(Game g)
        {
            //add TextSprites
            sm.addTextSprite(SpriteKey.StartText1, ((SprFont)tm.getTexture(TextKey.Font2).getTexture()).Instance, st.StartText1, new Vector2(200, 100), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            sm.addTextSprite(SpriteKey.StartText2, ((SprFont)tm.getTexture(TextKey.Font2).getTexture()).Instance, st.StartText2, new Vector2(200, 200), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            sm.addTextSprite(SpriteKey.StartText3, ((SprFont)tm.getTexture(TextKey.Font2).getTexture()).Instance, st.StartText3, new Vector2(200, 200), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            sm.addTextSprite(SpriteKey.SelectText, ((SprFont)tm.getTexture(TextKey.Font2).getTexture()).Instance, st.SelectText, new Vector2(200, 280), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            sm.addTextSprite(SpriteKey.DisabledText, ((SprFont)tm.getTexture(TextKey.Font1).getTexture()).Instance, st.DisabledText, new Vector2(200, 280), Color.Gray, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            sm.addTextSprite(SpriteKey.ScoreText, ((SprFont)tm.getTexture(TextKey.Font2).getTexture()).Instance, st.ScoreText, new Vector2(200, 280), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            sm.addTextSprite(SpriteKey.CreditText, ((SprFont)tm.getTexture(TextKey.Font2).getTexture()).Instance, st.CreditText, new Vector2(200, 280), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            sm.addTextSprite(SpriteKey.GameOverText, ((SprFont)tm.getTexture(TextKey.Font2).getTexture()).Instance, st.GameOverText, new Vector2(200, 280), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            sm.addTextSprite(SpriteKey.P1Score, ((SprFont)tm.getTexture(TextKey.Font2).getTexture()).Instance,"", new Vector2(200, 280), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            sm.addTextSprite(SpriteKey.P2Score, ((SprFont)tm.getTexture(TextKey.Font2).getTexture()).Instance, "", new Vector2(200, 280), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            sm.addTextSprite(SpriteKey.HighScore, ((SprFont)tm.getTexture(TextKey.Font2).getTexture()).Instance, "", new Vector2(200, 280), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            sm.addTextSprite(SpriteKey.Lives, ((SprFont)tm.getTexture(TextKey.Font2).getTexture()).Instance, "", new Vector2(200, 280), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            sm.addTextSprite(SpriteKey.Credits, ((SprFont)tm.getTexture(TextKey.Font2).getTexture()).Instance, "", new Vector2(200, 280), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            
            //add Sprites
            sm.addSprite(SpriteKey.Alien1, im.getImage(ImageKey.Alien2A), Vector2.One, Color.White, 0, Vector2.Zero, new Vector2(.4f, .4f), SpriteEffects.None, .5f);
            sm.addSprite(SpriteKey.Alien2, im.getImage(ImageKey.Alien1A), Vector2.One, Color.White, 0, Vector2.Zero, new Vector2(.4f, .4f), SpriteEffects.None, .5f);
            sm.addSprite(SpriteKey.Alien3, im.getImage(ImageKey.Alien3A), Vector2.One, Color.White, 0, Vector2.Zero, new Vector2(.4f, .4f), SpriteEffects.None, .5f);
            sm.addSprite(SpriteKey.UFO, im.getImage(ImageKey.UFO), Vector2.One, Color.Red, 0, Vector2.Zero, new Vector2(.5f, .5f), SpriteEffects.None, .25f);
            sm.addSprite(SpriteKey.Ship1, im.getImage(ImageKey.Ship), Vector2.One, Color.Green, 0, Vector2.Zero, new Vector2(.75f, .75f), SpriteEffects.None, 1f);
            sm.addSprite(SpriteKey.Missile, im.getImage(ImageKey.Missile), Vector2.Zero, Color.White, 0, Vector2.One, new Vector2(.2f, .25f), SpriteEffects.None, 0f);
            sm.addSprite(SpriteKey.Bomb1, im.getImage(ImageKey.Bomb2A), Vector2.One, Color.White, 0, Vector2.Zero, new Vector2(.25f, .35f), SpriteEffects.None, 0);
            sm.addSprite(SpriteKey.Bomb2, im.getImage(ImageKey.Bomb1A), Vector2.One, Color.White, 0, Vector2.Zero, new Vector2(.25f, .35f), SpriteEffects.None, 0);
            sm.addSprite(SpriteKey.Bomb3, im.getImage(ImageKey.Missile), Vector2.One, Color.White, 0, Vector2.Zero, new Vector2(.18f, .42f), SpriteEffects.None, 0);
            sm.addSprite(SpriteKey.UFOExplosion, im.getImage(ImageKey.Explosion1), Vector2.One, Color.Red, 0, Vector2.Zero, new Vector2(.6f, .5f), SpriteEffects.None, 0);
            sm.addSprite(SpriteKey.AlienExplosion, im.getImage(ImageKey.Explosion1), Vector2.One, Color.White, 0, Vector2.Zero, new Vector2(.45f, .5f), SpriteEffects.None, 0);
            sm.addSprite(SpriteKey.MissileExplosion, im.getImage(ImageKey.Explosion1), Vector2.One, Color.White, 0, Vector2.Zero, new Vector2(.22f, .3f), SpriteEffects.None, 0);
            sm.addSprite(SpriteKey.ShipExplosion, im.getImage(ImageKey.Explosion2a), Vector2.One, Color.Green, 0, Vector2.Zero, new Vector2(.75f, .75f), SpriteEffects.None, 0);
            //sm.addSprite(SpriteKey.TopStrip, im.getImage(ImageKey.Rectangle), Vector2.One, Color.Red, 0, Vector2.One, new Vector2(14f, 2f), SpriteEffects.None, 1);
            //sm.addSprite(SpriteKey.BotStrip, im.getImage(ImageKey.Rectangle), Vector2.One, new Color(0, 255, 0, 20), 0, Vector2.One, new Vector2(14f, 3f), SpriteEffects.None, 1);
        }

        //setup SpriteBatches
        public void loadSB(Game g)
        {
            
            sbm.add(SBKey.Background, new SpriteBatchC(new SpriteBatch(g.GraphicsDevice), SpriteSortMode.FrontToBack, BlendState.AlphaBlend));
            sbm.add(SBKey.Debug1, new SpriteBatchC(new SpriteBatch(g.GraphicsDevice), SpriteSortMode.FrontToBack, BlendState.AlphaBlend));
            sbm.add(SBKey.Game1, new SpriteBatchC(new SpriteBatch(g.GraphicsDevice), SpriteSortMode.FrontToBack, BlendState.AlphaBlend));
            //sbm.add(SBKey.Foreground, new SpriteBatchC(new SpriteBatch(g.GraphicsDevice), SpriteSortMode.FrontToBack, BlendState.Additive));
            //SpriteBatchC sb = sbm.getSB(SBKey.Foreground);
            //sb.addSprite(sm.getSprite(SpriteKey.TopStrip, new Vector2(0, 80)));
            //sb.addSprite(sm.getSprite(SpriteKey.BotStrip, new Vector2(0, 550)));
        }

        private void reloadSB(Game g)
        {
            sbm.remove(SBKey.Debug1);
            sbm.remove(SBKey.Game1);
            SpriteBatchC sb = sbm.getSB(SBKey.Foreground);
            sbm.remove(SBKey.Foreground);
            sbm.add(SBKey.Debug1, new SpriteBatchC(new SpriteBatch(g.GraphicsDevice), SpriteSortMode.FrontToBack, BlendState.AlphaBlend));
            sbm.add(SBKey.Game1, new SpriteBatchC(new SpriteBatch(g.GraphicsDevice), SpriteSortMode.FrontToBack, BlendState.AlphaBlend));
            sbm.getSB(SBKey.Game1).addSprite(new CollisionSprite(new Rectangle(0, 710, g.GraphicsDevice.Viewport.Width, 1), Color.Green, g.GraphicsDevice));
            //sbm.add(SBKey.Foreground, sb);
        }


        //load all screens in game
        public void loadScreens(Game g)
        {

            scm.setSB(sbm.getSB(SBKey.Background));
           
            GameOverScreen gameOver = new GameOverScreen(scm.getSB(), new TimeSpan(0, 0, 3), null);
            Screen game = new GameScreen(sbm.getSB(SBKey.Game1), sbm.getSB(SBKey.Debug1), gameOver, g);
            Screen select = select = new SelectScreen(scm.getSB(), game); 
            Screen start = new StartScreen(scm.getSB(), new TimeSpan(0,0, 4), select);
            gameOver.setNext(select);
            Screen background = new BackgroundScreen(scm.getSB(), null);
            scm.add(start);
            scm.add(select);
            scm.add(background);
            scm.add(game);
            scm.setFirst(start);
            background.start();
        }

        //load CollisionGroups
        public void loadColGroups(Game g)
        {
            cgm = CollisionGroupManager.CurrentInstance;
            cgm.add(GOType.Alien, g.GraphicsDevice);
            cgm.add(GOType.Bomb, g.GraphicsDevice);
            cgm.add(GOType.Bottom, g.GraphicsDevice);
            cgm.add(GOType.Missile, g.GraphicsDevice);
            cgm.add(GOType.Shield, g.GraphicsDevice);
            cgm.add(GOType.Ship, g.GraphicsDevice);
            cgm.add(GOType.Top, g.GraphicsDevice);
            cgm.add(GOType.UFO, g.GraphicsDevice);
            cgm.add(GOType.Wall, g.GraphicsDevice);
        }
        //load CollisionPairs
        public void loadColPairs(Game g)
        {
            cpm = CollisionPairManager.CurrentInstance;
            cgm = CollisionGroupManager.CurrentInstance;
            CollisionGroup alien = cgm.getGroup(GOType.Alien);
            CollisionGroup bomb = cgm.getGroup(GOType.Bomb);
            CollisionGroup bottom = cgm.getGroup(GOType.Bottom);
            CollisionGroup missile = cgm.getGroup(GOType.Missile);
            CollisionGroup shield = cgm.getGroup(GOType.Shield);
            CollisionGroup ship = cgm.getGroup(GOType.Ship);
            CollisionGroup top = cgm.getGroup(GOType.Top);
            CollisionGroup ufo = cgm.getGroup(GOType.UFO);
            CollisionGroup wall = cgm.getGroup(GOType.Wall);

            cpm.add(ColPairKey.AlienBottom, alien, bottom);
            cpm.add(ColPairKey.AlienMissile, alien, missile);
            cpm.add(ColPairKey.AlienShield, alien, shield);
            cpm.add(ColPairKey.AlienShip, alien, ship);
            cpm.add(ColPairKey.AlienWall, alien, wall);
            cpm.add(ColPairKey.BombBottom, bomb, bottom);
            cpm.add(ColPairKey.MissileBomb, bomb, missile);
            cpm.add(ColPairKey.MissileTop, missile, top);
            cpm.add(ColPairKey.ShieldBomb, bomb, shield);
            cpm.add(ColPairKey.ShieldMissile, shield, missile);
            cpm.add(ColPairKey.ShipBomb, bomb, ship);
            cpm.add(ColPairKey.ShipWall, ship, wall);
            cpm.add(ColPairKey.UFOMissile, missile, ufo);
            cpm.add(ColPairKey.UFOWall, wall, ufo);
        }

        //load CollisionObjects
        public void loadCollision(Game g)
        {
            cm = CollidableManager.CurrentInstance;
            cm.setSpriteBatch(sbm.getSB(SBKey.Debug1));
            //Alien and Shield CollisionObjects created by each's factory
            //missile
            cm.addSimpleCollidable(GOKey.Missile1, new Rectangle(-110, 650, 6, 16), Color.GreenYellow, g.GraphicsDevice);
            //ship
            cm.addSimpleCollidable(GOKey.Ship, new Rectangle(117, 655, 45, 28), Color.Red, g.GraphicsDevice);
            //ufo
            cm.addSimpleCollidable(GOKey.UFO, new Rectangle(105, 91, 50, 25), Color.Red, g.GraphicsDevice);
            //walls
            cm.addSimpleCollidable(GOKey.Top, new Rectangle(0, 0, g.GraphicsDevice.Viewport.Width, 90), Color.Gray, g.GraphicsDevice);
            cm.addSimpleCollidable(GOKey.Bottom, new Rectangle(0, g.GraphicsDevice.Viewport.Height - 70, g.GraphicsDevice.Viewport.Width, 70), Color.Gray, g.GraphicsDevice);
            cm.addSimpleCollidable(GOKey.Wall1, new Rectangle(0, 0, 1, g.GraphicsDevice.Viewport.Height), Color.Gray, g.GraphicsDevice);
            cm.addSimpleCollidable(GOKey.Wall2, new Rectangle(g.GraphicsDevice.Viewport.Width - 1, 0, 1, g.GraphicsDevice.Viewport.Height), Color.Gray, g.GraphicsDevice);
            //bomb
            cm.addSimpleCollidable(GOKey.Bomb1, new Rectangle(-100, -100, 10, 20), Color.Red, g.GraphicsDevice);
            cm.addSimpleCollidable(GOKey.Bomb2, new Rectangle(-100, -100, 9, 16), Color.Red, g.GraphicsDevice);
            cm.addSimpleCollidable(GOKey.Bomb3, new Rectangle(-100, -97, 5, 20), Color.Red, g.GraphicsDevice);
        }

        // load Game Objects
        public void loadGameObjects(Game g, int level)
        {
            gom = GameObjManager.CurrentInstance;
            cgm = CollisionGroupManager.CurrentInstance;
            SpriteBatchC sb = sbm.getSB(SBKey.Game1);
            gom.setSB(sb);
            //create Aliens
            af.createGrid(cgm.getGroup(GOType.Alien), level, g.GraphicsDevice);
            //create ship
            gom.addShip(GOKey.Ship, SpriteKey.Ship1, new Vector2(110, 630), cm.getCollidable(GOKey.Ship), new Vector2(117, 655));
            cgm.getGroup(GOType.Ship).add(gom.getGameObject(GOKey.Ship));
            //create missile
            gom.addMissile(GOKey.Missile1,SpriteKey.Missile, new Vector2(-110, 650), cm.getCollidable(GOKey.Missile1), new Vector2(-110, 650), new Vector2(0, -12));
            cgm.getGroup(GOType.Missile).add(gom.getGameObject(GOKey.Missile1));
            //create UFO
            gom.addUFO(GOKey.UFO, SpriteKey.UFO, new Vector2(100, 81), cm.getCollidable(GOKey.UFO), new Vector2(105, 91), new Vector2(3, 0), 10, 21);
            cgm.getGroup(GOType.UFO).add(gom.getGameObject(GOKey.UFO));
            //create bomb
            gom.addBomb(GOKey.Bomb1, SpriteKey.Bomb1, new Vector2(-100, -100), cm.getCollidable(GOKey.Bomb1), new Vector2(-100, -100), new Vector2(0, 4), 0);
            cgm.getGroup(GOType.Bomb).add(gom.getGameObject(GOKey.Bomb1));
            gom.addBomb(GOKey.Bomb2, SpriteKey.Bomb2, new Vector2(-100, -100), cm.getCollidable(GOKey.Bomb2), new Vector2(-100, -100), new Vector2(0, 5), 7);
            cgm.getGroup(GOType.Bomb).add(gom.getGameObject(GOKey.Bomb2));
            gom.addBomb(GOKey.Bomb3, SpriteKey.Bomb3, new Vector2(-100, -100), cm.getCollidable(GOKey.Bomb3), new Vector2(-100, -97), new Vector2(0, 4), 16);
            cgm.getGroup(GOType.Bomb).add(gom.getGameObject(GOKey.Bomb3));
            //create walls
            gom.addTop(GOKey.Top, cm.getCollidable(GOKey.Top), Vector2.Zero);
            gom.addBottom(GOKey.Bottom, cm.getCollidable(GOKey.Bottom), new Vector2(0, g.GraphicsDevice.Viewport.Height - 70));
            gom.addWall(GOKey.Wall1, cm.getCollidable(GOKey.Wall1), Vector2.Zero);
            gom.addWall(GOKey.Wall2, cm.getCollidable(GOKey.Wall2), cm.getCollidable(GOKey.Wall2).pos);
            cgm.getGroup(GOType.Top).add(gom.getGameObject(GOKey.Top));
            cgm.getGroup(GOType.Bottom).add(gom.getGameObject(GOKey.Bottom));
            cgm.getGroup(GOType.Wall).add(gom.getGameObject(GOKey.Wall1));
            cgm.getGroup(GOType.Wall).add(gom.getGameObject(GOKey.Wall2));
            //create shields
            gom.addShield(g.GraphicsDevice, sb, GOKey.Shield1, new Rectangle(80, 550, 80, 80), 4, 4, 3);
            gom.addShield(g.GraphicsDevice, sb, GOKey.Shield2, new Rectangle(220, 550, 80, 80), 4, 4, 3);
            gom.addShield(g.GraphicsDevice, sb, GOKey.Shield3, new Rectangle(360, 550, 80, 80), 4, 4, 3);
            gom.addShield(g.GraphicsDevice, sb, GOKey.Shield4, new Rectangle(500, 550, 80, 80), 4, 4, 3);
           
            CollisionGroup shield = cgm.getGroup(GOType.Shield);
            shield.add(gom.getGameObject(GOKey.Shield1));
            shield.add(gom.getGameObject(GOKey.Shield2));
            shield.add(gom.getGameObject(GOKey.Shield3));
            shield.add(gom.getGameObject(GOKey.Shield4));
        }

        //load animations
        public void loadAnimation(Game g, int level)
        {
            am = AnimationManager.CurrentInstance;
            Image[] image1 = new Image[2];
            Image[] image2 = new Image[2];
            Image[] image3 = new Image[2];
            Image[] image4 = new Image[2];
            Image[] image5 = new Image[2];
            Image[] image6 = new Image[2];
            image1[0] = im.getImage(ImageKey.Alien2B);
            image1[1] = im.getImage(ImageKey.Alien2A);
            image2[0] = im.getImage(ImageKey.Alien1A);
            image2[1] = im.getImage(ImageKey.Alien1B);
            image3[0] = im.getImage(ImageKey.Alien3A);
            image3[1] = im.getImage(ImageKey.Alien3B);
            image4[0] = im.getImage(ImageKey.Bomb2A);
            image4[1] = im.getImage(ImageKey.Bomb2B);
            image5[0] = im.getImage(ImageKey.Bomb1A);
            image5[1] = im.getImage(ImageKey.Bomb1B);
            image6[0] = im.getImage(ImageKey.Explosion2a);
            image6[1] = im.getImage(ImageKey.Explosion2b);
            am.addAnimation(AnimKey.Alien1, 2, 1008 - (17 * level), image1, sm.getSprite(SpriteKey.Alien1, new Vector2(40, 100)).getSprite());
            am.addAnimation(AnimKey.Alien2, 2, 1008 - (17 * level), image2, sm.getSprite(SpriteKey.Alien2, new Vector2(40, 186)).getSprite());
            am.addAnimation(AnimKey.Alien3, 2, 1008 - (17 * level), image3, sm.getSprite(SpriteKey.Alien3, new Vector2(40, 272)).getSprite());
            am.addAnimation(AnimKey.Bomb1, 2, 200, image4, sm.getSprite(SpriteKey.Bomb1, new Vector2(40, 272)).getSprite());
            am.addAnimation(AnimKey.Bomb2, 2, 160, image5, sm.getSprite(SpriteKey.Bomb2, new Vector2(40, 272)).getSprite());
            am.addAnimation(AnimKey.ShipExplosion, 2, 80, image6, sm.getSprite(SpriteKey.ShipExplosion, Vector2.Zero).getSprite());
        }

        public void loadSound(Game g)
        {
            sound.add(Sounds.Explosion, "Explosion");
            sound.add(Sounds.Kill, "Kill");
            sound.add(Sounds.Shoot, "Shoot");
            sound.add(Sounds.UFO, "UFO");
            sound.add(Sounds.UFOExplosion, "UFOExplosion");
            sound.add(Sounds.Walk1, "Walk1");
            sound.add(Sounds.Walk2, "Walk2");
            sound.add(Sounds.Walk3, "Walk3");
            sound.add(Sounds.Walk4, "Walk4");
        }


    }
}
