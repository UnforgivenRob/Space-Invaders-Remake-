using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Sprites;
using SpaceInvaders.Collision;
using SpaceInvaders.Shield;

namespace SpaceInvaders.GameObjects
{
    class Missile : GameObject
    {
        private Vector2 velocity;
        private Vector2 oldPos;
        private Vector2 delta = Vector2.Zero;
        private Vector2 inactivePos;
        private MissileState mState;
        private ReadyState ready;
        private FiredState fired;
        private ConcreteSpritePos explosion;
        private TimeServer.Event currentEvent;
        private TimeSpan deltaT;
        private Ship ship;
        private Sound.Sound shoot;
        private bool play;
        private Input.InputReader ir;

        public Missile(GOKey key, SpriteProxy Sprite, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos, Vector2 Velocity)
            : base(key, Sprite, spPos, collision, csPos)
        {
            velocity = Velocity;
            inactivePos = new Vector2(-100, -100);
            ship = (Ship)gom.getGameObject(GOKey.Ship);
            ready = new ReadyState(this, ship);
            fired = new FiredState(this);
            mState = ready;
            ir = Input.InputReader.Instance;
            shoot = soundMan.getSound(Sound.Sounds.Shoot);
            explosion = (ConcreteSpritePos)SpriteManager.Instance.getSprite(SpriteKey.MissileExplosion, new Vector2(-600, -100));
            deltaT = new TimeSpan(0, 0, 0, 0, 140);
            play = false;
        }

        public GOKey Name()
        {
            return name;
        }

        public Vector2 Velocity()
        {
            return velocity;
        }

        public override void update()
        {
            oldPos = pos;
            if (ir.isPressed(Keys.Space) && ship.active)
            {
                if (mState.Play())
                {
                    shoot.play();
                    moveTo(ship.mPos());
                }
                mState = fired;
                play = true;
            }
            
            mState.update();
        }

        //moves missile to designated position
        public void moveTo(Vector2 pos)
        {
            delta = pos - this.pos;
            base.update(delta);
        }

        //collide with a visitor
        public override void collide(CollisionVisitor v)
        {
            v.visitMissile(this);
        }

        //reaction if this collides with Alien
        public override void visitAlien(Alien a)
        {
            moveTo(inactivePos);
            mState = ready;
           // gom.deactivate(name);
        }

        //reaction if this collides with Shield
        public override void visitShield(Shield.Shield s)
        {
        }

        //reaction if this collides with ShieldColumn
        public override void visitShieldColumn(ShieldColumn sc)
        {
        }

        //reaction if this collides with ShieldColumn
        public override void visitShieldBrick(ShieldBrick sb)
        {
            explode();
            moveTo(inactivePos);
            mState = ready;
            //gom.deactivate(name);
        }

        //reaction if this collides with UFO
        public override void visitUFO(UFO u)
        {
            moveTo(inactivePos);
            mState = ready;
            //gom.deactivate(name);
        }

        //reaction if this collides with Bomb
        public override void visitBomb(Bomb b)
        {
            explode();
            moveTo(inactivePos);
            mState = ready;
           // gom.deactivate(name);
            
        }

        //reaction if this collides with Top
        public override void visitTop(Top t)
        {
            explode();
            moveTo(inactivePos);
            mState = ready;
            //gom.deactivate(name);
        }

        //reactive if this collidse with a container object
        public override void visitSuperGObj(SuperGObj s)
        {
        }

        public void explode()
        {
            oldPos.X -= 4;
            oldPos.Y -= 10;
            explosion.update(oldPos - explosion.position());
            SpriteBatchManager.Instance.getSB(SBKey.Game1).addSprite(explosion);
            currentEvent = TimeServer.EventFactory.Instance.getEvent();
            currentEvent.addCallBack(explosionCallBack);
            ts.enqueue(currentEvent, ts.currentTime() + deltaT);
        }

        public void explosionCallBack()
        {
            SpriteBatchManager.Instance.getSB(SBKey.Game1).removeSprite(explosion);
        }
    }

    class Bomb : GameObject
    {
        private Vector2 velocity;
        private Vector2 oldPos;
        private Vector2 delta;
        private Vector2 inactivePos;
        private BombState bstate;
        private AvailableState available;
        private TimeServer.Event currentEvent;
        private TimeSpan deltaT;
        private DroppedState dropped;
        private int delay;
        private ConcreteSpritePos explosion;

        public Bomb(GOKey key, SpriteProxy Sprite, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos, Vector2 velocity, int delaySecs)
            : base(key, Sprite, spPos, collision, csPos)
        {
            this.velocity = velocity;
            delay = delaySecs;
            oldPos = spPos;
            delta = Vector2.Zero;
            inactivePos = new Vector2(-100, -300);
            available = new AvailableState(this, delay);
            explosion = (ConcreteSpritePos)SpriteManager.Instance.getSprite(SpriteKey.MissileExplosion, new Vector2(-600, -100));
            deltaT = new TimeSpan(0, 0, 0, 0, 140);
            dropped = new DroppedState(this, (SuperGObj)gom.getGameObject(GOKey.Grid));
            bstate = available;
            
        }

        public GOKey Name()
        {
            return name;
        }

        public Vector2 Velocity()
        {
            return velocity;
        }

        public Vector2 oldpos()
        {
            return oldPos;
        }

        public void changeState()
        {
            if (bstate == available) bstate = dropped;
            else bstate = available;
            bstate.activate();
        }

        public override void update()
        {
            oldPos = pos;
           // base.update(velocity);
            bstate.update();
        }

        public void moveTo(Vector2 pos)
        {
            delta = pos - this.pos;
            base.update(delta);
        }

        //collide with a visitor
        public override void collide(CollisionVisitor v)
        {
            v.visitBomb(this);
        }

        //reaction if this collides with Ship
        public override void visitShip(Ship s)
        {
            moveTo(inactivePos);
            changeState();
        }

        //reaction if this collides with Shield
        public override void visitShield(Shield.Shield s)
        {
        }

        //reaction if this collides with ShieldColumn
        public override void visitShieldColumn(ShieldColumn sc)
        {
        }

        //reaction if this collides with ShieldColumn
        public override void visitShieldBrick(ShieldBrick sb)
        {
            explode();
            moveTo(inactivePos);
            changeState();
        }

        //reaction if this collides with Missile
        public override void visitMissile(Missile m)
        {
            explode();
            moveTo(inactivePos);
            changeState();
        }

        //reaction if this collides with Bottom
        public override void visitBottom(Bottom b)
        {
            explode();
            moveTo(inactivePos);
            changeState();
        }

        //reactive if this collidse with a container object
        public override void visitSuperGObj(SuperGObj s)
        {
            foreach (GameObject g in s.Children())
            {
                if (g == null) break;
                if (g.Collision().Bounds().Intersects(this.Collision().Bounds()))
                {
                    this.collide(g);
                }
            }
        }

        public void explode()
        {
            oldPos.X -= 4;
            oldPos.Y += 5;
            explosion.update(oldPos - explosion.position());
            SpriteBatchManager.Instance.getSB(SBKey.Game1).addSprite(explosion);
            currentEvent = TimeServer.EventFactory.Instance.getEvent();
            currentEvent.addCallBack(explosionCallBack);
            ts.enqueue(currentEvent, ts.currentTime() + deltaT);
        }

        public void explosionCallBack()
        {
            SpriteBatchManager.Instance.getSB(SBKey.Game1).removeSprite(explosion);
        }

    }
}
