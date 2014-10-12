using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Sprites;

namespace SpaceInvaders
{
    class Scoreboard
    {
        private int P1Score;
        private int P2Score;
        private int HighScore;
        public int Lives;
        public int Credits;
        private int level;
        private int aliensKilled;
        private SpriteManager sm;
        private Random r;
        private TextSprite p1;
        private TextSprite p2;
        private TextSprite high;
        private TextSprite lives;
        private TextSprite credits;


        private static readonly Scoreboard instance = new Scoreboard();
        private Scoreboard()
        {
            sm = SpriteManager.Instance;
            p1 = sm.getSprite(SpriteKey.P1Score, Vector2.Zero).getTextSprite();
            p2 = sm.getSprite(SpriteKey.P2Score, Vector2.Zero).getTextSprite();
            high = sm.getSprite(SpriteKey.HighScore, Vector2.Zero).getTextSprite();
            lives = sm.getSprite(SpriteKey.Lives, Vector2.Zero).getTextSprite();
            credits = sm.getSprite(SpriteKey.Credits, Vector2.Zero).getTextSprite();
            P1Score = 0;
            P2Score = 0;
            HighScore = 0;
            Lives = 0;
            Credits = 0;
            aliensKilled = 0;
            level = 0;
            r = new Random();
            p1.setText(P1Score.ToString("d4"));
            p2.setText(P2Score.ToString("d4"));
            high.setText(HighScore.ToString("d4"));
            lives.setText(Lives.ToString("D"));
            credits.setText(Credits.ToString("D"));
        }

        public static Scoreboard Instance
        {
            get
            {
                return instance;
            }
        }

        public void startLives()
        {
            Lives = 3;
            lives.setText(Lives.ToString("D"));
        }

        public void addCredit()
        {
            Credits++;
            credits.setText(Credits.ToString("D"));
        }

        public void removeCredit()
        {
            Credits--;
            credits.setText(Credits.ToString("D"));
        }

        public int AliensKilled()
        {
            return aliensKilled;
        }

        public int Level()
        {
            return level;
        }

        public void addLevel()
        {
            level++;
        }
        public void resetLevel()
        {
            level = 0;
        }

        public int p1Score()
        {
            return P1Score;
        }

        public int p2Score()
        {
            return P2Score;
        }

        public void setHighScore(int Score)
        {
            if (Score > HighScore)
            {
                HighScore = Score;
                high.setText(HighScore.ToString("d4"));
            }
        }

        public void resetP1Score()
        {
            P1Score = 0;
            p1.setText(P1Score.ToString("d4"));
        }

        public void resetP2Score()
        {
            P2Score = 0;
            p2.setText(P2Score.ToString("d4"));
        }

        public void resetAliensKilled()
        {
            aliensKilled = 0;
        }

        public void update(Alien a)
        {
            P1Score += a.Value();
            aliensKilled++;
            p1.setText(P1Score.ToString("D4"));
        }
        public void update(UFO u)
        {
            P1Score += r.Next(1, 7) * 50;
            p1.setText(P1Score.ToString("D4"));
        }
        public void update(Ship s)
        {
            Lives--;
            lives.setText(Lives.ToString("D"));
        }
    }
}
