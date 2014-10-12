using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites;
using SpaceInvaders.Collision;

namespace SpaceInvaders.GameObjects
{
    class SuperGObj : GameObject
    {
        private GameObject[] children;
        private int max;
        private int sz;
        private Rectangle bounds;

        public SuperGObj(GOKey name, int MaxChildren, SimpleCollisionObj collision, Vector2 csPos)
            : base(name, null, csPos, collision, csPos)
        {
            bounds = collision.bounds;
            children = new GameObject[MaxChildren];
            max = MaxChildren;
            sz = 0;
        }

        public override void collide(CollisionVisitor v)
        {
            v.visitSuperGObj(this);
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

        public override void visitMissile(Missile m)
        {
            foreach (GameObject g in children)
            {
                if (g == null) break;
                if (g.Collision().Bounds().Intersects(m.Collision().Bounds()))
                {
                    m.collide(g);
                    g.collide(m);
                }
            }
        }

        public override void visitWall(Wall w)
        {
            int i;
            if (bounds.X > 10) i = (w.Collision().bounds.Left - bounds.Right) -1;
            else i = (w.Collision().bounds.Right - bounds.Left) + 1;
            update(w , i);
            update();
        }

        public override void visitShield(Shield.Shield s)
        {
            foreach (GameObject g in children)
            {
                if (g == null) break;
                if (g.Collision().Bounds().Intersects(s.Collision().Bounds()))
                {
                    s.collide(g);
                    g.collide(s);
                }
            }
        }

        public override void visitShip(Ship s)
        {
            foreach (GameObject g in children)
            {
                if (g == null) break;
                if (g.Collision().Bounds().Intersects(s.Collision().Bounds()))
                {
                    s.collide(g);
                    g.collide(s);
                }
            }
        }

        public override void visitBottom(Bottom b)
        {
            foreach (GameObject g in children)
            {
                if (g == null) break;
                if (g.Collision().Bounds().Intersects(b.Collision().Bounds()))
                {
                    b.collide(g);
                    g.collide(b);
                }
            }
        }



        //returns children of supergobj
        public GameObject[] Children()
        {
            return children;
        }

        //returns number of children
        public int size()
        {
            return sz;
        }
        //adds child to 
        public void add(GameObject child)
        {
            if (sz >= max) 
            {
                Debug.Assert(false, "Full");
            }
            else
            {
                children[sz] = child;
                sz++;
                child.setParent(this);
                child.setGroup(group);
            }
        }

        //removes child
        public override void remove(GameObject child)
        {   
            for (int i = 0; i < sz - 1; i++)
            {
                if (children[i] == child) 
                {
                    shift(i);
                    children[sz - 1] = null;
                }
            }
            checkEnd(child);
            sz--;
            if (sz <= 0) gom.deactivate(this);
        }

        //shifts all objects in children over starting at i, used with remove()
        private void shift(int i)
        {
            for (int j = i; j < sz - 1; j++)
            {
                children[j] = children[j + 1];
            }
        }

        private void checkEnd(GameObject child)
        {
            if ((sz - 1 >= 0) && (children[sz - 1] == child)) children[sz - 1] = null;
        }

        public override void setGroup(CollisionGroup group)
        {
            base.setGroup(group);
            foreach (GameObject child in children)
            {
                if (child == null) break;
                child.setGroup(group);
            }
        }

        //updates supergobj
        public override void update()
        {
            calcBounds();
            
            if (parent != null)
            {
                parent.update();
            }
        }


        public override void update(Wall w, int i)
        {
            
             
            foreach (GameObject child in children)
            {
                if (child == null) break;
                child.update(w, i);
            }
            update();
        }

        public override void move()
        {
            foreach (GameObject child in children)
            {
                if (child == null) break;
                child.move();
            }
        }

        //calcs bounds
        private void calcBounds()
        {
            {

                Rectangle p;
                p.X = bounds.Center.X;
                p.Y = bounds.Center.Y;
                p.Width = 1;
                p.Height = 1;
                Rectangle r;
                foreach (GameObject g in children)
                {
                    if (g == null) break;
                    SimpleCollisionObj col = g.Collision();
                    r = col.Bounds();
                    p = Rectangle.Union(p, r);
                }
                bounds = p;
                cs.bounds = bounds;
                cs.getSprite().update(bounds);
            }
        }

    }
}
