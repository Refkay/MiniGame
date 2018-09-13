using UnityEngine;
using System.Collections;
/*

 SoundManager.PlaySfx(AudioClip)
*/
namespace MiniGame
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        public AudioClip musicsMenu;
        [Range(0, 1)]
        public float musicMenuVolume = 0.5f;
        public AudioClip musicsGame;
        [Range(0, 1)]
        public float musicsGameVolume = 0.5f;

        public AudioClip soundClick;
        public AudioClip soundGamefinish;
        public AudioClip soundGameover;

        private AudioSource musicAudio;
        private AudioSource soundFx;

        //GET and SET
        public static float MusicVolume
        {
            set { Instance.musicAudio.volume = value; }
            get { return Instance.musicAudio.volume; }
        }
        public static float SoundVolume
        {
            set { Instance.soundFx.volume = value; }
            get { return Instance.soundFx.volume; }
        }
    
        void Awake()
        {
            Instance = this;
            musicAudio = gameObject.AddComponent<AudioSource>();
            musicAudio.loop = true;
            musicAudio.volume = 0.5f;
            soundFx = gameObject.AddComponent<AudioSource>();
        }
        void Start()
        {          
            PlayMusic(musicsGame, musicsGameVolume);
        }

        public static void PlaySfx(AudioClip clip)
        {
            Instance.PlaySound(clip, Instance.soundFx);
        }

        public static void PlaySfx(AudioClip clip, float volume)
        {
            Instance.PlaySound(clip, Instance.soundFx, volume);
        }

        public static void PlayMusic(AudioClip clip)
        {
            Instance.PlaySound(clip, Instance.musicAudio);
        }

        public static void PlayMusic(AudioClip clip, float volume)
        {
            Instance.PlaySound(clip, Instance.musicAudio, volume);
        }

        private void PlaySound(AudioClip clip, AudioSource audioOut)
        {
            if (clip == null)
            {      
                return;
            }

            if (audioOut == musicAudio)
            {
                audioOut.clip = clip;
                audioOut.Play();
            }
            else
                audioOut.PlayOneShot(clip, SoundVolume);
        }

        private void PlaySound(AudioClip clip, AudioSource audioOut, float volume)
        {
            if (clip == null)
            {           
                return;
            }

            if (audioOut == musicAudio)
            {
                audioOut.clip = clip;
                audioOut.Play();
            }
            else
                audioOut.PlayOneShot(clip, SoundVolume * volume);
        }
    }

}

