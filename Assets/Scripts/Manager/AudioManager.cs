using UnityEngine;
using System.Collections.Generic;
using MiniGameComm;
namespace MiniGame
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public List<AudioClip> audioList;
        private AudioSource backMusicSource = null;
        private AudioSource soundSource = null;
        //public string musicName = "";

        // Use this for initialization
        private void Start()
        {

        }


        /// <summary>
        /// The init.
        /// </summary>
        protected override void Init()
        {
            backMusicSource = gameObject.AddComponent<AudioSource>();
            backMusicSource.loop = true;

            soundSource = gameObject.AddComponent<AudioSource>();
            soundSource.volume = 0.48f;

            backMusicSource.clip = this.audioList[0];
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
