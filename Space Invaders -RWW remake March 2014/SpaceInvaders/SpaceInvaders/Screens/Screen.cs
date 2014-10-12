using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.Sprites;

namespace SpaceInvaders.Screens
{
    abstract class Screen
    {
        protected SpriteBatchC sb;
        protected TimeServer.TimeServer ts;
        protected SpriteManager sm;
        protected ScreenManager scm;
        protected Screen next;
        protected bool active;

        protected Screen(SpriteBatchC sb, Screen next)
        {
            this.sb = sb;
            this.next = next;
            active = false;
            ts = TimeServer.TimeServer.CurrentInstance;
            sm = SpriteManager.Instance;
            scm = ScreenManager.Instance;
        }

        public abstract void start();
        public abstract void end();
        public abstract void update();
    }
}
