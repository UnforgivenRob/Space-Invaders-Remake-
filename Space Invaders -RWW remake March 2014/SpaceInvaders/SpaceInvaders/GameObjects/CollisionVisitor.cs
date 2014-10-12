using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using SpaceInvaders.Shield;

namespace SpaceInvaders.GameObjects
{
    interface CollidableObj
    {
        void collide(CollisionVisitor v);
    }

    class CollisionVisitor
    {
        public virtual void visitAlien(Alien a)
        {
            Debug.Assert(false, "Not Implemented");
        }

        public virtual void visitShip(Ship s)
        {
            Debug.Assert(false, "Not Implemented");
        }

        public virtual void visitUFO(UFO u)
        {
            Debug.Assert(false, "Not Implemented");
        }

        public virtual void visitMissile(Missile m)
        {
            Debug.Assert(false, "Not Implemented");
        }

        public virtual void visitBomb(Bomb b)
        {
            Debug.Assert(false, "Not Implemented");
        }

        public virtual void visitShield(Shield.Shield s)
        {
            Debug.Assert(false, "Not Implemented");
        }

       public virtual void visitShieldColumn(ShieldColumn sb)
        {
            Debug.Assert(false, "Not Implemented");
        }

        public virtual void visitShieldBrick(ShieldBrick sb)
        {
            Debug.Assert(false, "Not Implemented");
        }

        public virtual void visitWall(Wall w)
        {
            Debug.Assert(false, "Not Implemented");
        }

        public virtual void visitTop(Top t)
        {
            Debug.Assert(false, "Not Implemented");
        }

        public virtual void visitBottom(Bottom b)
        {
            Debug.Assert(false, "Not Implemented");
        }

        public virtual void visitSuperGObj(SuperGObj s)
        {
            Debug.Assert(false, "Not Implemented");
        }

    }
}
