using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Sprites;
using SpaceInvaders.TimeServer;

namespace SpaceInvaders.Collision
{
    class CollisionGroup
    {
        private GameObjManager GOM;
        private AnimationManager am;
        private TimeServer.TimeServer TS;
        private SuperGObj active;
        private SuperGObj inactive;
        private TimeSpan interval;
        private TimeSpan delta;
        private Event currentEvent;
        private Sound.SoundManager soundMan;
        private Sound.Sound[] sounds;
        private int soundCount;

        public CollisionGroup(GOType name, GraphicsDevice g)
        {
            GOM = GameObjManager.CurrentInstance;
            am = AnimationManager.CurrentInstance;
            TS = TimeServer.TimeServer.CurrentInstance;
            interval = am.getAnimation(AnimKey.Alien1).Interval();
            delta = am.getAnimation(AnimKey.Alien1).Delta();
            active = new SuperGObj(GOKey.none, 5, new SimpleCollisionObj(GOKey.none, Rectangle.Empty, Color.Azure, g), Vector2.Zero);
            active.setGroup(this);
            inactive = new SuperGObj(GOKey.none, 55, new SimpleCollisionObj(GOKey.none, Rectangle.Empty, Color.Azure, g), Vector2.Zero);
            inactive.setGroup(this);
            soundMan = Sound.SoundManager.Instance;
            sounds = new Sound.Sound[4];
            sounds[0] = soundMan.getSound(Sound.Sounds.Walk1);
            sounds[1] = soundMan.getSound(Sound.Sounds.Walk2);
            sounds[2] = soundMan.getSound(Sound.Sounds.Walk3);
            sounds[3] = soundMan.getSound(Sound.Sounds.Walk4);
            soundCount = 0;
        }

        //adds collidable to group
        public void add(GameObject g)
        {
            active.add(g);
            g.setGroup(this);
        }

        //returns active ColObj
        public SuperGObj getActive()
        {
            return active;
        }

        //deactivates colObj
        public void deactivate(GameObject g)
        {
            active.remove(g);
            inactive.add(g);
        }

        public void activate(GameObject g)
        {
            inactive.remove(g);
            g.getParent().add(g);
        }

        public void remove(GameObject g)
        {
            active.remove(g);
        }

        public void clear()
        {
            active = null;
            inactive = null;
        }

        //for use exclusively with Alien ColGroup
        public void start()
        {
            ((Bomb)GOM.getGameObject(GOKey.Bomb1)).changeState();
            ((Bomb)GOM.getGameObject(GOKey.Bomb2)).changeState();
            ((Bomb)GOM.getGameObject(GOKey.Bomb3)).changeState();
            currentEvent = EventFactory.Instance.getEvent();
            currentEvent.addCallBack(moveCallBack);
            TS.enqueue(currentEvent, TS.currentTime());
        }

        public void stop()
        {
            TS.dequeue(currentEvent);
        }
        public void moveCallBack()
        {
            sounds[soundCount].play();
            soundCount++;
            if (soundCount >= 4) soundCount = 0;
            active.move();
            currentEvent = EventFactory.Instance.getEvent();
            currentEvent.addCallBack(moveCallBack);
            TS.enqueue(currentEvent, TS.currentTime() + interval);
        }

        public void Callback()
        {
            interval -= delta;
            am.getAnimation(AnimKey.Alien1).decrease();
            am.getAnimation(AnimKey.Alien2).decrease();
            am.getAnimation(AnimKey.Alien3).decrease();
        }
    }
}
