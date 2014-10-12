using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites;
using SpaceInvaders.TimeServer;

namespace SpaceInvaders.Screens
{
    class StartScreen : Screen
    {
        private TimeSpan duration;
        private SpriteBatchManager sbm;
        private SpriteProxy text1;
        private SpriteProxy text2;
        private SpriteProxy text3;
        private SpriteProxy ufo;
        private SpriteProxy squid;
        private SpriteProxy crab;
        private SpriteProxy octopus;
        private Event currentEvent;


        public StartScreen(SpriteBatchC sb, TimeSpan duration, Screen next)
            : base(sb, next)
        {
            this.duration = duration;
            sbm = SpriteBatchManager.Instance;
            text1 = sm.getSprite(SpriteKey.StartText1, new Vector2(190, 200));
            text2 = sm.getSprite(SpriteKey.StartText2, new Vector2(240, 350));
            text3 = sm.getSprite(SpriteKey.StartText3, new Vector2(130, 530));
            ufo = sm.getSprite(SpriteKey.UFO, new Vector2(170, 340));
            squid = sm.getSprite(SpriteKey.Alien1, new Vector2(185, 385));
            crab = sm.getSprite(SpriteKey.Alien2, new Vector2(180, 420));
            octopus = sm.getSprite(SpriteKey.Alien3, new Vector2(178, 455));
            currentEvent = new Event();
        }

        //starts displaying screen
        public override void start()
        {
            sbm.remove(SBKey.Debug1);
            sbm.remove(SBKey.Game1);
            sb.addSprite(text1);
            sb.addSprite(text2);
            sb.addSprite(text3);
            sb.addSprite(ufo);
            sb.addSprite(squid);
            sb.addSprite(crab);
            sb.addSprite(octopus);
            currentEvent = EventFactory.Instance.getEvent();
            currentEvent.addCallBack(startCallBack);
            ts.enqueue(currentEvent, ts.currentTime()+ duration);
        }

        //stop displaying screen
        public override void end()
        {
            sb.removeSprite(text1);
            sb.removeSprite(text2);
            sb.removeSprite(text3);
            sb.removeSprite(ufo);
            sb.removeSprite(squid);
            sb.removeSprite(crab);
            sb.removeSprite(octopus);
        }

        public override void update()
        {
        }

        public void startCallBack()
        {
            end();
            next.start();

        }
    }


}
