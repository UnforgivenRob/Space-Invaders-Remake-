using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites;
using SpaceInvaders.Collision;

namespace SpaceInvaders.GameObjects
{
    abstract class GameObject : CollisionVisitor, CollidableObj
    {
        public bool active { get; set; }
        protected SimpleCollisionObj cs;
        protected SpriteProxy sp;
        protected Vector2 pos;
        protected Vector2 offset;
        public GOKey name;
        protected SuperGObj parent;
        protected CollisionGroup group;
        protected GameObjManager gom;
        protected Sound.SoundManager soundMan;
        protected TimeServer.TimeServer ts;
        
        protected GameObject(GOKey name, SpriteProxy Sprite, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos)
        {
            cs = collision;
            cs.setGameObj(this);
            cs.bounds.X = (int)csPos.X;
            cs.bounds.Y = (int)csPos.Y;

            sp = Sprite;
            pos = spPos;
            offset = csPos - pos;
            this.name = name;
            gom = GameObjManager.CurrentInstance;
            soundMan = Sound.SoundManager.Instance;
            ts = TimeServer.TimeServer.CurrentInstance;
            active = true;
        }

        //returns sprite associated with GameObj
        public SpriteProxy Sprite()
        {
            return sp;
        }

        // empty method to simplify SuperGObj remove method recursive call
        public virtual void remove(GameObject g)
        {
        }

        //returns Collision Obj
        public SimpleCollisionObj Collision()
        {
            return cs;
        }

        //sets parent
        public void setParent(SuperGObj parent)
        {
            this.parent = parent;
        }

        //sets group
        public virtual void setGroup(CollisionGroup group)
        {
            this.group = group;
        }

        //returns parent
        public SuperGObj getParent()
        {
            return parent;
        }

        //updates GameObj by delta
        public void update(Vector2 delta)
        {
            pos += delta;
            cs.update(delta);
            sp.update(delta);
        }

        public virtual void update()
        {
        }

        public virtual void update(Wall w, int overlap)
        {
            Debug.Assert(false, "Not Implemented");
        }

        public virtual void move()
        {
        }

        public abstract void collide(CollisionVisitor v);

    }
}
