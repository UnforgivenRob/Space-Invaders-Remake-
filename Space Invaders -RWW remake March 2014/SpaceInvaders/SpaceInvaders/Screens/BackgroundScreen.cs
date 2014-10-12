using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites;

namespace SpaceInvaders.Screens
{
    class BackgroundScreen : Screen
    {
        private SpriteProxy scoreHeader;
        private SpriteProxy creditHeader;
        private SpriteProxy p1Score;
        private SpriteProxy p2Score;
        private SpriteProxy highScore;
        private SpriteProxy lives;
        private SpriteProxy credits;
        private SpriteProxy liveSprite;

        public BackgroundScreen(SpriteBatchC sb, Screen next)
            : base(sb, next)
        {
            scoreHeader = sm.getSprite(SpriteKey.ScoreText, new Vector2(0, 10));
            creditHeader = sm.getSprite(SpriteKey.CreditText, new Vector2(480, 720));
            p1Score = sm.getSprite(SpriteKey.P1Score, new Vector2(50, 50));
            p2Score = sm.getSprite(SpriteKey.P2Score, new Vector2(500, 50));
            highScore = sm.getSprite(SpriteKey.HighScore, new Vector2(280, 50));
            lives = sm.getSprite(SpriteKey.Lives, new Vector2(10, 720));
            liveSprite = sm.getSprite(SpriteKey.Ship1, new Vector2(40, 700));
            credits = sm.getSprite(SpriteKey.Credits, new Vector2(620, 720));
        }

        public override void update()
        {
        }

        public override void start()
        {
            sb.addSprite(scoreHeader);
            sb.addSprite(creditHeader);
            sb.addSprite(p1Score);
            sb.addSprite(p2Score);
            sb.addSprite(highScore);
            sb.addSprite(lives);
            sb.addSprite(liveSprite);
            sb.addSprite(credits);
        }

        public override void end()
        {
        }
    }
}
