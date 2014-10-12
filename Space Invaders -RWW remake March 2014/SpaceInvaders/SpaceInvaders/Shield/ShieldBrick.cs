using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Collision;

namespace SpaceInvaders.Shield
{
    class ShieldBrick : GameObject
    {
        private CollidableManager cm;
        private int durability;
        private Rectangle bounds;
        private Vector2 origPos;
        private int deltaH;
        private Shield shield;
        private Vector2 hitPos;

        public ShieldBrick(Vector2 pos, SimpleCollisionObj cs, int BrickDurability)
            : base(GOKey.none, null, pos, cs, pos)
        {
            cm = CollidableManager.CurrentInstance;
            origPos = pos;
            durability = BrickDurability;
            bounds = cs.bounds;
            deltaH = bounds.Height / durability;
            hitPos.Y = bounds.Bottom;

        }

        public void setParent(ShieldColumn parent)
        {
            this.parent = parent;
        }

        public void setShield(Shield s)
        {
            shield = s;
        }


        public override void collide(CollisionVisitor v)
        {
            v.visitShieldBrick(this);
        }

        public override void visitAlien(Alien a)
        {
            parent.remove(this);
            cm.deactivate(cs);
            shield.erase(origPos);
        }

        public override void visitMissile(Missile m)
        {
            bounds.Height -= deltaH;
            cs.bounds = bounds;
            cs.getSprite().height = bounds.Height;
            hitPos.X = m.Collision().pos.X;
            shield.erodeBot(hitPos);
            hitPos.Y = bounds.Bottom;
            durability--;
            if (durability <= 0)
            {
                shield.erase(origPos);
                parent.remove(this);
                cm.deactivate(cs);
            }
        }

        public override void visitBomb(Bomb b)
        {
            pos.Y += deltaH;
            bounds.Y += deltaH;
            bounds.Height -= deltaH;
            cs.update(pos - cs.pos);
            cs.getSprite().height = bounds.Height;
            shield.erodeTop(b.oldpos());
            durability--;
            if (durability <= 0)
            {
                shield.erase(origPos);
                parent.remove(this);
                cm.deactivate(cs);
            }
        }

    }
}
