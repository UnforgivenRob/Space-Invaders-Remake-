using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Collision
{
    class SimpleCollisionObj 
    {
        private CollisionSprite sprite;
        public Rectangle bounds;
        public Rectangle oldBounds;
        public Vector2 pos;
        private GOKey name;
        private GameObject gameobj;
        private CollisionGroup group;

        public SimpleCollisionObj(GOKey name, Rectangle bounds, Color boxColor, GraphicsDevice g)
        {
            CollisionSprite ColSprite = new CollisionSprite(bounds, boxColor, g);
            sprite = ColSprite;
            this.bounds = bounds;
            oldBounds = bounds;
            this.name = name;
            pos.X = bounds.X;
            pos.Y = bounds.Y;
        }
        
        //sets GameObj associated with col obj
        public void setGameObj(GameObject go)
        {
            gameobj = go;
        }
        //returns GameObj associated wtih col obj
        public GameObject GameObject()
        {
            return gameobj;
        }

        //returns name of COllision obj
        public GOKey Name()
        {
            return name;
        }

        //returns Collision sprite
        public CollisionSprite getSprite()
        {
            return sprite;
        }

        //sets group
        public void setGroup(CollisionGroup group)
        {
            this.group = group;
        }
        //returns group
        public CollisionGroup Group()
        {
            return group;
        }

        //returns bounds
        public Rectangle Bounds()
        {
            return bounds;
        }

        public Vector2 Position()
        {
            return pos;
        }

        public void update(Vector2 delta)
        {
            oldBounds = bounds;
            pos += delta;
            bounds.X += (int)delta.X;
            bounds.Y += (int)delta.Y;
            sprite.update(delta);
        }

        //doesnt have children bounds will not change
        public void calcBounds()
        {
        }


    }
}
