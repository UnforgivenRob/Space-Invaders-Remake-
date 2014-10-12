using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites;
using SpaceInvaders.TimeServer;

namespace SpaceInvaders.Screens
{
    class GameOverScreen : Screen
    {
        private TimeSpan duration;
        private SpriteProxy text;
        private Event currentEvent;

        public GameOverScreen(SpriteBatchC sb, TimeSpan duration, Screen next)
            : base(sb, next)
        {
            this.duration = duration;
            text = sm.getSprite(SpriteKey.GameOverText, new Vector2(230, 350));
        }

        public override void update()
        {
        }

        public override void start()
        {
            sb.addSprite(text);
            currentEvent = EventFactory.Instance.getEvent();
            currentEvent.addCallBack(gameOverCallBack);
            ts.enqueue(currentEvent, ts.currentTime() + duration);
        }

        public override void end()
        {
            sb.removeSprite(text);
        }

        public void gameOverCallBack()
        {
            end();
            next.start();
        }

        public void setNext(Screen s)
        {
            next = s;
        }
    }


}
