using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.TimeServer;

namespace SpaceInvaders.GameObjects
{
    abstract class UFOState
    {
        protected UFO u;
        protected Event currentEvent;
        protected TimeServer.TimeServer TS;
        protected Random r;

        protected UFOState(UFO u)
        {
            this.u = u;
            r = new Random();
            TS = TimeServer.TimeServer.CurrentInstance;
        }

        public abstract void update();
        public abstract void activate();
    }

    class ActiveState : UFOState
    {
        public ActiveState(UFO u)
            : base(u)
        {
        }

        public override void update()
        {
            u.move1();
        }
        public override void activate()
        {
            u.moveCallBack();
        }
    }

    class InactiveState : UFOState
    {
        private int delayMin;
        private int delayMax;
        public InactiveState(UFO u, int delayMin, int delayMax)
            : base(u)
        {
            this.delayMax = delayMax;
            this.delayMin = delayMin;
        }

        public override void update()
        {
        }

        public override void activate()
        {
            TimeSpan interval = new TimeSpan(0, 0, r.Next(delayMin, delayMax + 1));
            currentEvent = EventFactory.Instance.getEvent();
            currentEvent.addCallBack(u.UFOCallBack);
            TS.enqueue(currentEvent, TS.currentTime() + interval);
        }
    }
}
