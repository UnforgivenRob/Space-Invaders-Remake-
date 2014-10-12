using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Sprites;
using SpaceInvaders.Collision;
using SpaceInvaders.Shield;

namespace SpaceInvaders.GameObjects
{
    public enum GOKey
    {
        none,
        Grid,
        Column1,
        Column2,
        Column3,
        Column4,
        Column5,
        Column6,
        Column7,
        Column8,
        Column9,
        Column10,
        Column11,

        Alien1_1,
        Alien1_2,
        Alien1_3,
        Alien1_4,
        Alien1_5,
        Alien2_1,
        Alien2_2,
        Alien2_3,
        Alien2_4,
        Alien2_5,
        Alien3_1,
        Alien3_2,
        Alien3_3,
        Alien3_4,
        Alien3_5,
        Alien4_1,
        Alien4_2,
        Alien4_3,
        Alien4_4,
        Alien4_5,
        Alien5_1,
        Alien5_2,
        Alien5_3,
        Alien5_4,
        Alien5_5,
        Alien6_1,
        Alien6_2,
        Alien6_3,
        Alien6_4,
        Alien6_5,
        Alien7_1,
        Alien7_2,
        Alien7_3,
        Alien7_4,
        Alien7_5,
        Alien8_1,
        Alien8_2,
        Alien8_3,
        Alien8_4,
        Alien8_5,
        Alien9_1,
        Alien9_2,
        Alien9_3,
        Alien9_4,
        Alien9_5,
        Alien10_1,
        Alien10_2,
        Alien10_3,
        Alien10_4,
        Alien10_5,
        Alien11_1,
        Alien11_2,
        Alien11_3,
        Alien11_4,
        Alien11_5,

        
        Ship,
        UFO,
        Wall1,
        Wall2,
        Top,
        Bottom,
        Missile1,
        Missile2,
        Missile3,
        Missile4,
        Missile5,
        Bomb1,
        Bomb2,
        Bomb3,
        Bomb4,
        Bomb5,
        Shield1,
        Shield2,
        Shield3,
        Shield4,
    }

    class GameObjManager
    {
        private GOLL active;
        private GOLL inactive;
        private CollidableManager cm;
        private SpriteManager sm;
        private SpriteBatchManager sbm;
        private SpriteBatchC sb;

        private static readonly GameObjManager instance1 = new GameObjManager();
        private static readonly GameObjManager instance2 = new GameObjManager();
        private static GameObjManager currentInstance = instance1;
        private GameObjManager()
        {
            active = new GOLL();
            inactive = new GOLL();
            cm = CollidableManager.CurrentInstance;
            sm = SpriteManager.Instance;
            sbm = SpriteBatchManager.Instance;
        }

        public static GameObjManager CurrentInstance
        {
            get
            {
                return currentInstance;
            }
        }

        public void switchInstance()
        {
            if (currentInstance == instance1) currentInstance = instance2;
            else currentInstance = instance1;
        }

        public void setSB(SpriteBatchC sb)
        {
            this.sb = sb;
        }

        //adds an Alien
        public void addAlien(GOKey key, SpriteKey sKey, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos, Vector2 velocity, int value)
        {
            SpriteProxy sp = sm.getSprite(sKey, spPos);
            sb.addSprite(sp);
            GameObject go = new Alien(key, sp, spPos, collision, csPos, velocity, value);
            active.add(key, go);
        }

        //adds a Ship
        public void addShip(GOKey key, SpriteKey sKey, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos)
        {
            SpriteProxy sp = sm.getSprite(sKey, spPos);
            sb.addSprite(sp);
            GameObject go = new Ship(key, sp, spPos, collision, csPos, 4);
            active.add(key, go);
        }

        //adds an UFO
        public void addUFO(GOKey key, SpriteKey sKey, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos, Vector2 velocity, int delayMin, int delayMax)
        {
            SpriteProxy sp = sm.getSprite(sKey, spPos);
            sb.addSprite(sp);
            GameObject go = new UFO(key, sp, spPos, collision, csPos, velocity, delayMin, delayMax);
            active.add(key, go);
        }

        //adds a missile
        public void addMissile(GOKey key, SpriteKey sKey, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos, Vector2 velocity)
        {
            SpriteProxy sp = sm.getSprite(sKey, spPos);
            sb.addSprite(sp);
            GameObject go = new Missile(key, sp, spPos, collision, csPos, velocity);
            active.add(key, go);
        }

        //adds a bomb
         public void addBomb(GOKey key, SpriteKey sKey, Vector2 spPos, SimpleCollisionObj collision, Vector2 csPos, Vector2 velocity, int delaySecs)
        {
            SpriteProxy sp = sm.getSprite(sKey, spPos);
            sb.addSprite(sp);
            GameObject go = new Bomb(key, sp, spPos, collision, csPos, velocity, delaySecs);
            active.add(key, go);
        }
        
        //adds shield, will be updated in PA4
        public void addShield(GraphicsDevice g, SpriteBatchC sb, GOKey name, Rectangle bounds, int rows, int columns, int BrickDurability)
        {
            GameObject go = ShieldFactory.Instance.Create(g, sb, name, bounds, rows, columns, BrickDurability);
            active.add(name, go);
        }

        //adds wall
        public void addWall(GOKey key, SimpleCollisionObj collision, Vector2 csPos)
        {
            GameObject go = new Wall(key, null, csPos, collision, csPos);
            active.add(key, go);
        }

        //adds top wall
        public void addTop(GOKey key, SimpleCollisionObj collision, Vector2 csPos)
        {
            GameObject go = new Top(key, null, csPos, collision, csPos);
            active.add(key, go);
        }

        //adds bottom wall
        public void addBottom(GOKey key, SimpleCollisionObj collision, Vector2 csPos)
        {
            GameObject go = new Bottom(key, null, csPos, collision, csPos);
            active.add(key, go);
        }

        //adds super container obj
        public void addSuper(GOKey key, SimpleCollisionObj collision, Vector2 csPos, int MaxChildren)
        {
            GameObject go = new SuperGObj(key, MaxChildren, collision, csPos);
            active.add(key, go);
        }

        //activates a deactivated Collidable
        public void activate(GOKey key)
        {
            GameObject c = inactive.remove(key);
            SimpleCollisionObj col = c.Collision();
            active.add(key, c);
            cm.activate(col);
            c.Sprite().getSB().addSprite(c.Sprite());
            c.getParent().add(c);
            c.active = true;
            

        }

        //deactivates an active Collidable
        public void deactivate(GOKey key)
        {
            GameObject c = active.remove(key);
            SimpleCollisionObj col = c.Collision();
            inactive.add(key, c);
            cm.deactivate(col);
            if (c.Sprite() != null)
            {
                c.Sprite().getSB().removeSprite(c.Sprite());
            }
            c.getParent().remove(c);
            c.active = false;
        }

        public void deactivate(GameObject c)
        {
            
            active.remove(c);
            SimpleCollisionObj col = c.Collision();
            inactive.add(c.name, c);
            cm.deactivate(col);
            if (c.Sprite() != null)
            {
                c.Sprite().getSB().removeSprite(c.Sprite());
            }
            if (c.getParent() != null)
            {
                c.getParent().remove(c);
            }
            c.active = false;
        }

        //returns Collidable for a given key
        public GameObject getGameObject(GOKey key)
        {
            return active.getGameObj(key);
        }

        //updates all game objects
        public void update()
        {
            active.update();
        }

        //unloads all content
        public void unload()
        {
            active.clear();
            inactive.clear();
        }

        //LL class to hold iamges
        internal class GOLL
        {
            private node head = new node(GOKey.none, null);
            private node tail = new node(GOKey.none, null);
            private node[] pool;

            public GOLL()
            {
                head.next = tail;
                //ideally should hold 85 to hold all Collidables in game, 20 will do for demo
                pool = new node[20];
                for (int i = 0; i < 20; i++)
                {
                    pool[i] = new node(GOKey.none, null);
                }

            }

            //retrieve available node from pool
            private node getNode(GOKey key, GameObject g)
            {
                bool nodeFound = false;
                for (int i = 0; i < 20; i++)
                {
                    if (!pool[i].active)
                    {
                        nodeFound = true;
                        pool[i].active = true;
                        pool[i].g = g;
                        pool[i].key = key;
                        return pool[i];
                    }
                }

                if (!nodeFound)
                {
                    return new node(key, g);
                }
                else
                {
                    //should never be reached
                    return null;
                }
            }

            //adds Collidable to LL, replaces value if key already in LL
            public void add(GOKey key, GameObject g)
            {
                node r = head;
                bool contains = false;
                while (r != tail)
                {
                    if (r.key == key)
                    {
                        contains = true;
                        break;
                    }
                    r = r.next;
                }

                if (!contains)
                {
                    node n = getNode(key, g);
                    n.next = head.next;
                    head.next = n;
                }
                else
                {
                    r.g = g;
                }
            }



            //remove GameObject for a give key
            public GameObject remove(GOKey key)
            {
                node r = head;
                GameObject ret = null;
                while (r.next != tail)
                {
                    if (r.next.key == key)
                    {
                        ret = r.next.g;
                        r.next.active = false;
                        r.next = r.next.next;
                        break;
                    }
                    r = r.next;
                }
                return ret;
            }

            // removes GameObject without need of GOKey
            public void remove(GameObject g)
            {
                node r = head;
                while (r.next != tail)
                {
                    if (r.next.g == g)
                    {
                        r.next.active = false;
                        r.next = r.next.next;
                        break;
                    }
                    r = r.next;
                }
            }

            //returns GameObj associated with key
            public GameObject getGameObj(GOKey key)
            {
                node r = head;
                while (r != tail)
                {
                    if (r.key == key)
                    {
                        return r.g;
                    }
                    r = r.next;
                }
                return null;
            }

            //updates all objs
            public void update()
            {
                node r = head.next;
                while (r != tail)
                {
                    r.g.update();
                    r = r.next;
                }
            }

            //clears LL
            public void clear()
            {
                head.next = tail;
                //pool = null;
            }

            private class node
            {
                public node next;
                public GOKey key;
                public GameObject g;
                public bool active;

                public node(GOKey Key, GameObject G)
                {
                    key = Key;
                    g = G;
                    active = false;
                }
            }
        }
    }
}
