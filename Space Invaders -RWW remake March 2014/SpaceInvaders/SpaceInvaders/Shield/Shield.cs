using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Sprites;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Collision;

namespace SpaceInvaders.Shield
{
    //placeholder, visitor methods will be updated for game
    class Shield : GameObject
    {
        private GraphicsDevice g;
        private Rectangle bounds;
        //private Vector2 pos;

        private ShieldColumn[] columns;


        public Shield(GOKey key, SpriteProxy s, SimpleCollisionObj cs, Rectangle bounds)
            : base(key, s,  new Vector2(bounds.X, bounds.Y), cs, new Vector2(bounds.X, bounds.Y))
        {
            //pos.X = bounds.X;
           // pos.Y = bounds.Y;
        }

        public void add(ShieldColumn[] columns)
        {
            this.columns = columns;
            foreach (ShieldColumn sc in columns)
            {
                sc.setShield(this);
            }
        }

        //returns ShieldSPrite associated with Shield
        public ShieldSprite sprite()
        {
            return base.sp.getShieldSprite();
        }
        
        //erases brick's worth of area at pos
        public void erase(Vector2 pos)
        {
            sp.getShieldSprite().erase(pos);
        }

        //renders erosion from collision on bot
        public void erodeBot(Vector2 pos)
        {
            pos.X -= 15;
            pos.Y -= 25;
            sp.getShieldSprite().render(pos);
        }

        //renders erosion from collision on top
        public void erodeTop(Vector2 pos)
        {
            pos.X -= 15;
            pos.Y += 2;
            sp.getShieldSprite().render(pos);
        }

        //accepts visitor
        public override void collide(CollisionVisitor v)
        {
            v.visitShield(this);
        }

        //reaction if this collides with Missile
        public override void visitMissile(Missile m)
        {
            foreach (ShieldColumn sc in columns)
            {

                if (sc == null) break;
                if (sc.Collision().Bounds().Intersects(m.Collision().Bounds()))
                {
                    m.collide(sc);
                    sc.collide(m);
                }
            }
        }

        //reaction if this collides with bomb
        public override void visitBomb(Bomb b)
        {
            foreach (ShieldColumn sc in columns)
            {
                if (sc == null) break;
                if (sc.Collision().Bounds().Intersects(b.Collision().Bounds()))
                {
                    b.collide(sc);
                    sc.collide(b);
                }
            }
        }

        //reaction if this collides with Alien
        public override void visitAlien(Alien a)
        {
            foreach (ShieldColumn sc in columns)
            {
                if (sc == null) break;
                if (sc.Collision().Bounds().Intersects(a.Collision().Bounds()))
                {
                    a.collide(sc);
                    sc.collide(a);
                }
            }
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
    }
}
