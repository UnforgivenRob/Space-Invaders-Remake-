using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Collision
{
    class CollisionPair
    {
        CollisionGroup a;
        CollisionGroup b;
        ColPairKey name;
        CollisionReader cr;

        public CollisionPair(ColPairKey key, CollisionGroup a, CollisionGroup b)
        {
            this.a = a;
            this.b = b;
            name = key;
            cr = new CollisionReader();
        }

        public void Collide()
        {
            SuperGObj A = a.getActive();
            SuperGObj B = b.getActive();
            foreach (GameObject childA in A.Children())
            {
                foreach (GameObject childB in B.Children())
                {
                    if (childB == null || childA == null) break;
                    cr.collide(childA, childB);
                }
            }
        }
    }
}
