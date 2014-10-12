using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace SpaceInvaders.Sound
{
    public enum Sounds
    {
        None,
        Walk1,
        Walk2,
        Walk3,
        Walk4,
        UFO,
        UFOExplosion,
        Shoot,
        Kill,
        Explosion,
    }

    class SoundManager
    {
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        SoundLL sounds;

        private static readonly SoundManager instance = new SoundManager();
        private SoundManager()
        {
            audioEngine = new AudioEngine(@"Content\SpaceInvadersSound.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Sound Bank.xsb");
            sounds = new SoundLL();
            
        }

        public static SoundManager Instance
        {
            get
            {
                return instance;
            }
        }

        //updates audio engine
        public void update()
        {
            audioEngine.Update();
        }

        //adds sound to linked list
        public void add(Sounds name, string asset)
        {
            Sound s = new Sound(name, asset, soundBank);
            sounds.add(name, s);
        }

        //gets sound from ll
        public Sound getSound(Sounds name)
        {
            return sounds.getSound(name);
        }

        //removes sound from ll
        public void remove(Sounds name)
        {
            sounds.remove(name);
        }

        //clears ll
        public void unload()
        {
            sounds.clear();
        }

    }

    //LL class to hold Sounds
    internal class SoundLL
    {
        private node head = new node(Sounds.None, null);
        private node tail = new node(Sounds.None, null);

        public SoundLL()
        {
            head.next = tail;
        }

        //adds sound to LL, replaces value if key already in LL
        public void add(Sounds name, Sound s)
        {
            node r = head;
            bool contains = false;
            while (r.next != tail)
            {
                if (r.next.key == name)
                {
                    contains = true;
                    break;
                }
                r = r.next;
            }

            if (!contains)
            {
                node n = new node(name, s);
                n.next = r.next;
                r.next = n;
            }
            else
            {
                r.next.s = s;
            }
        }

        //remove sound of a give key
        public void remove(Sounds name)
        {
            node r = head;
            while (r.next != tail)
            {
                if (r.next.key == name)
                {
                    r.next = r.next.next;
                }
                else r = r.next;
            }
        }

        //returns sound associated with key
        public Sound getSound(Sounds name)
        {
            node r = head;
            while (r != tail)
            {
                if (r.key == name)
                {
                    return r.s;
                }
                r = r.next;
            }

            return null;
        }

        //clears LL
        public void clear()
        {
            head.next = tail;
        }

        private class node
        {
            public node next;
            public Sounds key;
            public Sound s;

            public node(Sounds name, Sound sound)
            {
                key = name;
                s = sound;
            }
        }
    }
}
