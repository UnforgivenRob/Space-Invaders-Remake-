using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Sprites;

namespace SpaceInvaders.Screens
{
    class SelectScreen : Screen
    {
        private SpriteProxy text;
        private SpriteProxy disabled;
        private Input.InputReader ir;
        private Scoreboard score;

        public SelectScreen(SpriteBatchC sb, Screen next)
            : base(sb, next)
        {
            text = sm.getSprite(SpriteKey.SelectText, new Vector2(150, 280));
            disabled = sm.getSprite(SpriteKey.DisabledText, new Vector2(180, 380));
            ir = Input.InputReader.Instance;
            score = Scoreboard.Instance;
        }

        public override void update()
        {
            if (active)
            {
                if (ir.isPressed2(Keys.C)) score.addCredit();
                selectionCheck();
            }
        }

        private void selectionCheck()
        {
             if (ir.isPressed(Keys.D1))
                {
                    if (score.Credits >= 1)
                    {
                        score.removeCredit();
                        end();
                    }
                    else { }
                }
                else if (ir.isPressed(Keys.D2))
                {
                    //code for 2player initialization
                }
        }

        public override void start()
        {
            active = true;
            sb.addSprite(text);
            sb.addSprite(disabled);
        }

        public override void end()
        {
            active = false;
            sb.removeSprite(text);
            sb.removeSprite(disabled);
            next.start();
        }
    }
}
