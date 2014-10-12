using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Sprites;
using SpaceInvaders.Collision;
using SpaceInvaders.TimeServer;

namespace SpaceInvaders.GameObjects
{
    //placeholder, visitor methods will be updated later when needed for game
    class Ship : GameObject
    {
        private Input.InputReader ir;
        private Vector2 velocity = Vector2.Zero;
        private Vector2 missilePos;
        private Vector2 delta;
        private Vector2 movePos;
        private Sound.Sound sound;
        private ConcreteSpritePos explosion;
        private TimeServer.Event currentEvent;
        private TimeSpan deltaT;

        public Ship(GOKey key, SpriteProxy Sprite, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos, float movespeed)
            : base(key, Sprite, spPos, collision, csPos)
        {
            ir = Input.InputReader.Instance;
            velocity.X = movespeed;
            missilePos = csPos;
            missilePos.Y += 5;
            missilePos.X += (collision.bounds.Width / 2) -2;
            movePos = pos;
            sound = soundMan.getSound(Sound.Sounds.Explosion);
            explosion = (ConcreteSpritePos)SpriteManager.Instance.getSprite(SpriteKey.ShipExplosion, new Vector2(-100, -100));
            deltaT = new TimeSpan(0, 0, 0, 0, 1100);
        }

        public override void update()
        {
            if (ir.isPressed(Keys.Left)) 
            {
                update(velocity * -1);
                missilePos += velocity * -1;
            }
            if (ir.isPressed(Keys.Right))
            {
                update(velocity);
                missilePos += velocity;
            }
        }

        public Vector2 mPos()
        {
            return missilePos;
        }

        //moves missile to designated position
        public void moveTo(Vector2 pos)
        {
            delta = pos - this.pos;
            base.update(delta);
            missilePos += delta;
        }

        //collide with a visitor
        public override void collide(CollisionVisitor v)
        {
            v.visitShip(this);
        }

        //reaction if this collides with Alien
        public override void visitAlien(Alien a)
        {
            //needs to be updated
            sound.play();
            gom.deactivate(this);
        }

        //reaction if this collides with bomb
        public override void visitBomb(Bomb b)
        {
            gom.deactivate(this);
            sound.play();
            explode();
            Scoreboard.Instance.update(this);
        }

        //reaction if this collides with wall
        public override void visitWall(Wall w)
        {
            if (pos.X >= 200) //right wall
            {
                movePos.X = w.Collision().bounds.Left - Collision().bounds.Width - 3;
            }
            else //left wall
            {
                movePos.X = w.Collision().bounds.Right - offset.X;
            }
            moveTo(movePos);
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
            explosion.update(pos - explosion.position());
            SpriteBatchManager.Instance.getSB(SBKey.Game1).addSprite(explosion);
            currentEvent = TimeServer.EventFactory.Instance.getEvent();
            currentEvent.addCallBack(explosionCallBack);
            ts.enqueue(currentEvent, ts.currentTime() + deltaT);
        }

        public void explosionCallBack()
        {
            SpriteBatchManager.Instance.getSB(SBKey.Game1).removeSprite(explosion);
            gom.activate(name);
        }
    }

    //placeholder, visitor methods will be updated later when needed for game
    class UFO : GameObject
    {
        private TimeServer.TimeServer TS;
        private Event currentEvent;
        private Event currentEvent1;
        private Vector2 velocity;
        private Vector2 rightVelocity;
        private Vector2 leftVelocity;
        private Sound.Sound moveSound;
        private Sound.Sound explodeSound;
        private Random r;
        private Vector2 Leftpos;
        private Vector2 Rightpos;
        private Vector2 inactivePos;
        private TimeSpan moveSoundDelay;
        private UFOState uState;
        private ActiveState activeState;
        private InactiveState inactiveState;
        private ConcreteSpritePos explosion;
        private TimeSpan deltaT;

        public UFO(GOKey key, SpriteProxy Sprite, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos, Vector2 Velocity, int delayMin, int delayMax)
            : base(key, Sprite, spPos, collision, csPos)
        {
            TS = TimeServer.TimeServer.CurrentInstance;
            rightVelocity = Velocity;
            leftVelocity = Velocity * -1;
            velocity = Velocity;
            activeState = new ActiveState(this);
            inactiveState = new InactiveState(this, delayMin, delayMax);
            uState = inactiveState;
            moveSound = soundMan.getSound(Sound.Sounds.UFO);
            explodeSound = soundMan.getSound(Sound.Sounds.UFOExplosion);
            r = new Random();
            Leftpos = new Vector2(-2, 81);
            Rightpos = new Vector2(615, 81);
            inactivePos = new Vector2(-400, -400);
            moveSoundDelay = new TimeSpan(0, 0, 0, 0, 150);
            explosion = (ConcreteSpritePos)SpriteManager.Instance.getSprite(SpriteKey.UFOExplosion, new Vector2(-200, -200));
            deltaT = new TimeSpan(0, 0, 0, 0, 200);
        }

        public override void update()
        {
            uState.update();
        }

        public void move1()
        {
            base.update(velocity);
        }

        //collide with a visitor
        public override void collide(CollisionVisitor v)
        {
            v.visitUFO(this);
        }

        //reaction if this collides with Missile
        public override void visitMissile(Missile m)
        {
            explodeSound.play();
            explode();
            removeCall();
            Scoreboard.Instance.update(this);
            
        }

        //reaction if this collides with wall
        public override void visitWall(Wall w)
        {
            removeCall();
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

        public void moveTo(Vector2 pos)
        {
            Vector2 delta = pos - this.pos;
            base.update(delta);
        }

        public void start()
        {
            moveTo(inactivePos);
            uState.activate();
        }

        //callback to be used in time
        public void UFOCallBack()
        {
            uState = activeState;
            uState.activate();
            int i = r.Next(0, 2);
            if (i == 0) //left to right
            {
                moveTo(Leftpos);
                velocity = rightVelocity;
            }
            else //right to left
            {
                moveTo(Rightpos);
                velocity = leftVelocity;
            }
        }

        public void explode()
        {
            explosion.update(pos - explosion.position());
            SpriteBatchManager.Instance.getSB(SBKey.Game1).addSprite(explosion);
            currentEvent1 = TimeServer.EventFactory.Instance.getEvent();
            currentEvent1.addCallBack(explosionCallBack);
            ts.enqueue(currentEvent1, ts.currentTime() + deltaT);
        }

        public void explosionCallBack()
        {
            SpriteBatchManager.Instance.getSB(SBKey.Game1).removeSprite(explosion);
        }

        public void moveCallBack()
        {
            moveSound.play();
            currentEvent = EventFactory.Instance.getEvent();
            currentEvent.addCallBack(moveCallBack);
            TS.enqueue(currentEvent, TS.currentTime() + moveSoundDelay);
        }

        public void removeCall()
        {
            moveTo(inactivePos);
            TS.dequeue(currentEvent);
            uState = inactiveState;
            uState.activate();         
        }
    }
}
