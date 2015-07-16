using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using AstroFlare;

namespace Astro_Flare_XNASilverlight
{
    public static class Sounds
    {
        static SoundEffect explosion;
        static SoundEffect shot;
        static SoundEffect powerup;

        static List<SoundEffectInstance> soundInstances;

        public static void Initialize()
        {
            explosion = SoundEffect.FromStream(TitleContainer.OpenStream("explodeice3.wav"));
            shot = SoundEffect.FromStream(TitleContainer.OpenStream("Shot2.wav"));
            powerup = SoundEffect.FromStream(TitleContainer.OpenStream("FX1.wav"));

            soundInstances = new List<SoundEffectInstance>();
        }

        public static void PlaySound(string name)
        {
            if (Config.SoundFXOn)
            {
                if (soundInstances.Count > 10)
                {
                    soundInstances[0].Stop();
                    soundInstances.Remove(soundInstances[0]);
                }

                switch (name)
                {
                    case "explosion":
                        //explosion.Play();
                        SoundEffectInstance newInstance = explosion.CreateInstance();
                        newInstance.Volume = 0.5f;
                        newInstance.Play();
                        soundInstances.Add(newInstance);
                        break;
                    case "shot":
                        //shot.Play();
                        SoundEffectInstance newInstance2 = shot.CreateInstance();
                        newInstance2.Volume = 0.7f;
                        newInstance2.Play();
                        soundInstances.Add(newInstance2);
                        break;
                    case "powerup":
                        //powerup.Play(0.7f, 0.0f, 0.0f);
                        SoundEffectInstance newInstance3 = powerup.CreateInstance();
                        newInstance3.Volume = 0.5f;
                        newInstance3.Play();
                        soundInstances.Add(newInstance3);
                        break;
                }
            }
        }
    }
}
