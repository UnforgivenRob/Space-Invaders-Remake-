using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace SpaceInvaders.Sound
{
    class Sound
    {
        private Sounds name;
        private string asset;
        private SoundBank sb;
        private Cue cue;

        public Sound(Sounds name, string asset, SoundBank sb)
        {
            this.name = name;
            this.asset = asset;
            this.sb = sb;
        }

        public void play()
        {
            cue = sb.GetCue(asset);
            cue.Play();
        }
    }
}
