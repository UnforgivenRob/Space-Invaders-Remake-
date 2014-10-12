using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.TimeServer;

namespace SpaceInvaders.Sprites
{
    //class to handle animation
    class Animation
    {
        private int frames;
        private TimeSpan interval;
        private TimeSpan delta;
        private Image[] images;
        private int curr;
        private Sprite sprite;
        private TimeServer.TimeServer TS;
        private Event currentEvent;
        private bool running;
      
        public Animation(int Frames, int intervalMS, Image[] Images, Sprite sprite)
        {
            frames = Frames;
            interval = new TimeSpan(0, 0, 0, 0, intervalMS);
            delta = new TimeSpan(0, 0, 0, 0, 17);
            images = Images;
            curr = 0;
            this.sprite = sprite;
            TS = TimeServer.TimeServer.CurrentInstance;
            currentEvent = new Event();
            running = false;

        }

        public void decrease()
        {
            interval -= delta;
        }

        public TimeSpan Interval()
        {
            return interval;
        }

        public TimeSpan Delta()
        {
            return delta;
        }

        //returns whether animation is currently running
        public bool isRunning()
        {
            return running;
        }

        //stops animation
        public void stop()
        {
            TS.dequeue(currentEvent);
            running = false;
        }

        //starts the animation
        public void start()
        {
            currentEvent = EventFactory.Instance.getEvent();
            currentEvent.addCallBack(AnimCallBack);
            TS.enqueue(currentEvent, TS.currentTime());
            running = true;
        }

        public Image currentImage()
        {
            return images[curr];
        }

        public void advance()
        {
            curr++;
            if (curr >= frames) curr = 0;
        }

        public void AnimCallBack()
        {
            advance();
            sprite.image = images[curr];
            currentEvent = EventFactory.Instance.getEvent();
            currentEvent.addCallBack(AnimCallBack);
            TS.enqueue(currentEvent, TS.currentTime() + interval);
        }
    }
}
