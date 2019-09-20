using UnityEngine;

namespace Assets.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager manager = null;

        private void Awake()
        {
            if (manager == null)
            {
                DontDestroyOnLoad(gameObject);
                manager = this;
            }
            else if (manager != this)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// The spinning sound when the wheels are moving.
        /// </summary>
        public AudioSource SpinningSound;

        /// <summary>
        /// The winning sound when a player wins.
        /// </summary>
        public AudioSource WinningSound;

        /// <summary>
        /// The stop sound after a spin.
        /// </summary>
        public AudioSource StartSound;

        /// <summary>
        /// The stop sound after a spin.
        /// </summary>
        public AudioSource StopSound;

        /// <summary>
        /// Plays the winning sound.
        /// </summary>
        public void PlayWinningSound()
        {
            WinningSound.Play();
        }

        /// <summary>
        /// Plays the start sound.
        /// </summary>
        public void PlayStartSound()
        {
            StartSound.Play();
        }

        /// <summary>
        /// Plays the stop sound.
        /// </summary>
        public void PlayStopSound()
        {
            StopSound.Play();
        }

        /// <summary>
        /// Plays the run sound.
        /// </summary>
        public void PlaySpinningSound()
        {
            SpinningSound.Play();
        }
    }
}
