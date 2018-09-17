using UnityEngine;
using System.Collections.Generic;
using MiniGameComm;
namespace MiniGame
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public List<AudioClip> audioList;
        public AudioSource backMusicSource ;
        public AudioSource soundSource ;

        public float backMusicVolume = 0.4f;
        public float soundSourceVolume = 1.0f;

        // Use this for initialization
        private void Start()
        {

        }


        /// <summary>
        /// The init.
        /// </summary>
        protected override void Init()
        {
            backMusicSource.volume = backMusicVolume;
            backMusicSource.clip = this.audioList[0];

            soundSource.volume = soundSourceVolume;
            backMusicSource.Play();
        }
        private void PlayMusic(string musicPath)
        {
            AudioClip clip = Resources.Load(musicPath) as AudioClip;
            backMusicSource.clip = clip;
            backMusicSource.Play();
        }

        private void PlaySound(string soundPath)
        {
            AudioClip clip = Resources.Load(soundPath) as AudioClip;
            soundSource.PlayOneShot(clip);
        }

        public void PlayOneShotIndex(int i)
        {
            soundSource.PlayOneShot(this.audioList[i]);
        }
    }
}
