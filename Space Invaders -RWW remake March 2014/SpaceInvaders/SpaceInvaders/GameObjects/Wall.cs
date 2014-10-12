using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites;
using SpaceInvaders.Collision;


namespace SpaceInvaders.GameObjects
{
    class Wall: GameObject
    {
        public Wall(GOKey key, SpriteProxy Sprite, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos)
            : base(key, Sprite, spPos, collision, csPos)
        {
        }

        //collide with a visitor
        public override void collide(CollisionVisitor v)
        {
            v.visitWall(this);
        }

         //reaction if this collides with Alien
        public override void visitAlien(Alien a)
        {
        }

        //reaction if this collides with Ship
        public override void visitShip(Ship s)
        {
            Console.WriteLine("Wall Collided with ship");
        }

        //reaction if this collides with UFO
        public override void visitUFO(UFO u)
        {
            Console.WriteLine("Wall Collided with UFO");
        }

        //reactive if this collidse with a container object
        public override void visitSuperGObj(SuperGObj s)
        {
        }

    }

    class Top : GameObject
    {
        public Top(GOKey key, SpriteProxy Sprite, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos)
            : base(key, Sprite, spPos, collision, csPos)
        {
        }

        //collide with a visitor
        public override void collide(CollisionVisitor v)
        {
            v.visitTop(this);
        }

        //reaction if this collides with missile
        public override void visitMissile(Missile m)
        {
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

    class Bottom : GameObject
    {
        public Bottom(GOKey key, SpriteProxy Sprite, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos)
            : base(key, Sprite, spPos, collision, csPos)
        {
        }

        //collide with a visitor
        public override void collide(CollisionVisitor v)
        {
            v.visitBottom(this);
        }

        //reaction of collision between this and bomb
        public override void visitBomb(Bomb b)
        {
            Console.WriteLine("Bottom collided with bomb");
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
