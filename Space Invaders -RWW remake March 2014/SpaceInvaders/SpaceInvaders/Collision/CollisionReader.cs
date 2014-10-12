using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Collision
{
    class CollisionReader
    {
        public CollisionReader()
        {
        }

        public void collide(GameObject a, GameObject b)
        {
            if (hasCollision(a.Collision(), b.Collision()))
            {
                a.collide(b);
                b.collide(a);
            }
        }

        public bool hasCollision(SimpleCollisionObj a, SimpleCollisionObj b)
        {
            Rectangle Abounds = a.Bounds();
            Rectangle Bbounds = b.Bounds();
            if (Abounds.Intersects(Bbounds)) return true;
            else return false;
        }
    }
}
