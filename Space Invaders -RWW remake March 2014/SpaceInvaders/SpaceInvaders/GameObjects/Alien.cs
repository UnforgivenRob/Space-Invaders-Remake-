using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites;
using SpaceInvaders.Collision;
using SpaceInvaders.Shield;

namespace SpaceInvaders.GameObjects
{
    class Alien : GameObject
    {
        private Vector2 velocity;
        private Vector2 reverse = new Vector2(-1, 0);
        private Random rand;
        private Vector2 delta;
        private Vector2 movePos;
        private Sound.Sound sound;
        private int value;
        private ConcreteSpritePos explosion;
        private TimeServer.Event currentEvent;
        private TimeSpan deltaT;

        public Alien(GOKey key, SpriteProxy Sprite, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos, Vector2 velocity, int value)
            : base(key, Sprite, spPos, collision, csPos)
        {
            this.velocity = velocity;
            rand = new Random();
            sound = soundMan.getSound(Sound.Sounds.Kill);
            this.value = value;
            explosion = (ConcreteSpritePos)SpriteManager.Instance.getSprite(SpriteKey.AlienExplosion, new Vector2(-400, -200));
            deltaT = new TimeSpan(0, 0, 0, 0, 200);
        }

        public void moveTo(Vector2 pos)
        {
            delta = pos - this.pos;
            base.update(delta);
        }

        public int Value()
        {
            return value;
        }

        public override void update()
        {
           //base.update(velocity);
        }

        public override void move()
        {
            base.update(velocity);
        }

        public override void update(Wall w, int overlap)
        {
            movePos = pos;
            movePos.X += overlap;
            visitWall(w);
        }

        //accepts visitor
        public override void collide(CollisionVisitor v)
        {
            v.visitAlien(this);
        }

        
        public override void visitMissile(Missile m)
        {
            gom.deactivate(name);
            Scoreboard.Instance.update(this);
            explode();
            sound.play();
            group.Callback();
            
        }

        public override void visitWall(Wall w)
        {
            movePos.Y += 32;
            velocity *= reverse;
            moveTo(movePos);
            
         }

        //reaction if this collides with Shield
        public override void visitShield(Shield.Shield s)
        {
            //sp.update(Color.Green);
        }

        //reaction if this collides with ShieldColumn
        public override void visitShieldColumn(ShieldColumn sc)
        {
        }

        //reaction if this collides with ShieldColumn
        public override void visitShieldBrick(ShieldBrick sb)
        {
        }

        public override void visitBottom(Bottom b)
        {
            Scoreboard.Instance.Lives = 0;
        }

        public override void visitShip(Ship s)
        {
            Scoreboard.Instance.Lives = 0;
        }

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
        }
    }
}
