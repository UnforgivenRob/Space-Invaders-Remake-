using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.TimeServer;

namespace SpaceInvaders.GameObjects
{
    abstract class BombState
    {
        protected Bomb b;
        protected TimeServer.TimeServer ts;
        protected Random r;
        protected BombState(Bomb b)
        {
            this.b = b;
            ts = TimeServer.TimeServer.CurrentInstance;
            r = new Random();
        }
        public abstract void update();
        public abstract void activate();
    }

    class AvailableState : BombState
    {
        
        private TimeSpan intervalMin;
        private TimeSpan intervalMax;
        private Event currentEvent;

        public AvailableState(Bomb b, int intervalSecs)
            : base(b)
        {
            intervalMin = new TimeSpan(0,0, intervalSecs);
            intervalMax = new TimeSpan(0,0, intervalSecs * 3);
           
        }

        public override void update()
        {}

        public override void activate()
        {
            currentEvent = EventFactory.Instance.getEvent();
            currentEvent.addCallBack(bombCallBack);
            ts.enqueue(currentEvent, ts.currentTime() + intervalMin);
        }

        public void bombCallBack()
        {
           b.changeState();
        }
    }

    class DroppedState : BombState
    {
        private SuperGObj grid;

        public DroppedState(Bomb b, SuperGObj grid)
            : base(b)
        {
            this.grid = grid;
        }

        public override void update()
        {
            b.update(b.Velocity());
        }

        public override void activate()
        {
            //find location to drop
            int i = r.Next(0, grid.size());
            Vector2 movepos;
            Rectangle rect = grid.Children()[i].Collision().bounds;
            movepos.Y = rect.Bottom - 20;
            movepos.X = rect.X + rect.Width / 2;
            //move bomb
            b.moveTo(movepos);
        }

        
    }
}
