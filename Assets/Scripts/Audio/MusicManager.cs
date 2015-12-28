namespace Scripts.Audio
{
    using UnityEngine;
    public class MusicManager : MonoBehaviour
    {
        private AudioSource source;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            this.source = GetComponent<AudioSource>();
        }

    }
}