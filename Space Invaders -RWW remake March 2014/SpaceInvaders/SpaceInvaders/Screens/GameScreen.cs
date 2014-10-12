using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Sprites;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Collision;

namespace SpaceInvaders.Screens
{
    class GameScreen : Screen
    {
        
        Input.InputReader ir;
        SpriteBatchManager sbm;
        SpriteBatchC debug;
        private bool d;
        GameObjManager gom;
        Setup setup;
        CollisionGroupManager cgm;
        CollisionPairManager cpm;
        AnimationManager am;
        Scoreboard score;
        Game g;

        public GameScreen(SpriteBatchC sb, SpriteBatchC debug, Screen next, Game g)
            : base(sb, next)
        {
            this.g = g;
            this.debug = debug;
            ir = Input.InputReader.Instance;
            sbm = SpriteBatchManager.Instance;
            gom = GameObjManager.CurrentInstance;
            cpm = CollisionPairManager.CurrentInstance;
            cgm = CollisionGroupManager.CurrentInstance;
            am = AnimationManager.CurrentInstance;
            setup = Setup.Instance;
            score = Scoreboard.Instance;
            active = false;
            d = false;
        }

        public override void update()
        {
            if (active)
            {
                cpm.CollideAll();
                gom.update();
                sbm.drawAll();
                checkInput();
                if (score.AliensKilled() == 55)
                {
                    score.addLevel();
                    setup.gameUnload();
                    objStart();
                }
            }
        }

        private void checkInput()
        {
            
            if (ir.isPressed(Keys.D))
            {
                
                if (d)
                {
                    sbm.remove(SBKey.Debug1);
                    d = false;
                }
                else
                {
                    sbm.add(SBKey.Debug1, debug);
                    d = true;
                }
            }
            if ((ir.isPressed(Keys.F1)) || (score.Lives <= 0))
            {
                score.resetLevel();
                end();
            } 
        }

        public override void start()
        {
            score.startLives();
            score.resetP1Score();
            sbm.add(SBKey.Game1, sb);
            objStart();
            active = true;
            
            
        }

        private void objStart()
        {
            score.resetAliensKilled();
            setup.gameLoad(g, score.Level());
            am.getAnimation(AnimKey.Alien1).start();
            am.getAnimation(AnimKey.Alien2).start();
            am.getAnimation(AnimKey.Alien3).start();
            am.getAnimation(AnimKey.Bomb1).start();
            am.getAnimation(AnimKey.Bomb2).start();
            am.getAnimation(AnimKey.ShipExplosion).start();
            cgm.getGroup(GOType.Alien).start();
            ((UFO)gom.getGameObject(GOKey.UFO)).start();
            debug = sbm.getSB(SBKey.Debug1);
            sbm.remove(SBKey.Debug1);
        }

        private void objStop()
        {
            am.getAnimation(AnimKey.Alien1).stop();
            am.getAnimation(AnimKey.Alien2).stop();
            am.getAnimation(AnimKey.Alien3).stop();
            am.getAnimation(AnimKey.Bomb1).stop();
            am.getAnimation(AnimKey.Bomb2).stop();
            cgm.getGroup(GOType.Alien).stop();
            setup.gameUnload();
            //((UFO)gom.getGameObject(GOKey.UFO)).stop();
        }

        public override void end()
        {
            active = false;
            objStop();
            sbm.remove(SBKey.Game1);
            sbm.remove(SBKey.Debug1);
            score.setHighScore(score.p1Score());
            next.start();
        }
    }
}
