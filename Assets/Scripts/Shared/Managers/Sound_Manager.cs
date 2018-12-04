using UnityEngine;

namespace Assets.Scripts.Shared.Managers
{
    public class Sound_Manager : MonoBehaviour
    {
        public static Sound_Manager instance = null;
        private AudioSource m_audioSource = null;

        public void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(this.gameObject);
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            m_audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void Boom()
        {
            var boomClip = Resources.Load<AudioClip>("Sounds/boom");
            m_audioSource.PlayOneShot(boomClip);
        }

        public void Jump()
        {
            var jumpClip = Resources.Load<AudioClip>("Sounds/jump");
            m_audioSource.PlayOneShot(jumpClip);
        }

        public void Pow()
        {
            var powClip = Resources.Load<AudioClip>("Sounds/pow");
            m_audioSource.PlayOneShot(powClip);
        }
    }
}
