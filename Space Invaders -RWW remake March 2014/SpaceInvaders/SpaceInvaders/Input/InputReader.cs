using System;

using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace SpaceInvaders.Input
{
    /// <summary>
    /// class to read input
    /// </summary>
    public class InputReader
    {
        KeyboardState newStateK;
        KeyboardState oldStateK;

        private static readonly InputReader instance = new InputReader();
        private InputReader()
        {
            oldStateK = Keyboard.GetState();
        }

        public static InputReader Instance
        {
            get
            {
                return instance;
            }
        }

        // Helper function to detect when a key is pressed, returns true only the cycle it is pressed
        private bool isPressedK(Keys key)
        {
            //check state for key press
            if (newStateK.IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool isPressedK2(Keys key)
        {
            //check state for key press
            if (newStateK.IsKeyDown(key))
            {
                //if just pressed return true, else false
                if (oldStateK.IsKeyUp(key))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //reads keyboard input and converts it to Input Val using isPressedK()
        public bool isPressed(Keys key)
        {
            newStateK = Keyboard.GetState();
            bool ret;
            ret = isPressedK(key);
          
            // Update state
            oldStateK = newStateK;
            return ret;
        }

        public bool isPressed2(Keys key)
        {
            newStateK = Keyboard.GetState();
            bool ret;
            ret = isPressedK2(key);

            // Update state
            oldStateK = newStateK;
            return ret;
        }

    }
}
