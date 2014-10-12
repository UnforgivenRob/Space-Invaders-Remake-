using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Collision;

namespace SpaceInvaders.Shield
{
    class ShieldColumn : SuperGObj
    {
        private CollidableManager cm;
        private ShieldBrick[] bricks;
        private int head;
        private int tail;
        private int max;
        private Shield shield;

        public ShieldColumn(Vector2 Pos, SimpleCollisionObj cs, int rows)
            : base(GOKey.none, rows, cs, Pos)
        {
            cm = CollidableManager.CurrentInstance;
            bricks = new ShieldBrick[rows];
            head = 0;
            tail = head;
            max = rows;
        }

        public override void collide(CollisionVisitor v)
        {
            v.visitShieldColumn(this);
        }

        public override void visitAlien(Alien a)
        {
            if (head == tail || bricks[head] == null ) {}
            else if (bricks[head].Collision().Bounds().Intersects(a.Collision().Bounds()))
            {
                bricks[head].collide(a);
                a.collide(bricks[head]);
                update();
                
            }
        }

        public override void visitMissile(Missile m)
        {
            if (head == tail) { cm.deactivate(cs); }
            else if (bricks[tail-1].Collision().Bounds().Intersects(Rectangle.Union(m.Collision().Bounds(), m.Collision().oldBounds)))
            {
               
                m.collide(bricks[tail-1]);
                bricks[tail - 1].collide(m);
                update();
            }
        }

        public override void visitBomb(Bomb b)
        {
            if (head == tail || head == max) { cm.deactivate(cs); }
            else if (bricks[head].Collision().Bounds().Intersects(Rectangle.Union(b.Collision().Bounds(), b.Collision().oldBounds)))
            {
                bricks[head].collide(b);
                b.collide(bricks[head]);
                update();
            }
        }

        public void add(ShieldBrick brick)
        {
            base.add(brick);
            bricks[tail] = brick;
            brick.setShield(shield);
            tail++;
        }

        public override void remove(GameObject brick)
        {
            if (bricks[head] == brick) head++;
            else if (bricks[tail - 1] == brick) tail--;
            base.remove(brick);
            
        }

        public void setShield(Shield s)
        {
            shield = s;
            for (int i = head; i < tail; i++)
            {
                if (bricks[i] == null) break;
                bricks[i].setShield(s);
            }
        }
    }
}
