using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Screens
{
    class ScreenText
    {

        public String StartText1;
        public String StartText2;
        public String StartText3;
        public String SelectText;
        public String DisabledText;
        public String ScoreText;
        public String CreditText;
        public String GameOverText;

        public ScreenText()
        {
            StartText1 = "Space Invaders\n\n *Score Table*";
            StartText2 = "= ? Mystery \n= 30 Points \n= 20 Points \n= 10 Points";
            StartText3 = "         *Controls*\n       C = Add Credit\n  1/2 = Players Select\nLeft/Right = Move Ship\n  Space = Fire Missile";
            SelectText = "         Select \n\n<1> or <2> Players";
            ScoreText = "   Score <1>     High-Score     Score <2>";
            DisabledText = "(2 player Disabled)";
            CreditText = "Credit";
            GameOverText = "Game Over";
        }

        
        
        
    }
}
