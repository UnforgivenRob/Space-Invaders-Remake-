using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects
{
    abstract class MissileState
    {
        protected Missile m;
        protected bool play;

        protected MissileState(Missile m)
        {
            this.m = m;
          
        }
        public abstract void update();

        public bool Play()
        {
            return play;
        }
    }

    class ReadyState : MissileState
    {
        Ship s;

        public ReadyState(Missile m, Ship s)
            : base(m)
        {
            this.s = s;
            play = true;
        }

        public override void update()
        {
            //m.moveTo(s.mPos());
        }
    }

    class FiredState : MissileState
    {
        public FiredState(Missile m)
            : base(m)
        {
            play = false;
        }

        public override void update()
        {

            m.update(m.Velocity());
        }
    }
}
