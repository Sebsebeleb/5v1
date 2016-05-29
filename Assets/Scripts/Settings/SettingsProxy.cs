namespace BBG.Settings
{
    using UnityEngine;
    using UnityEngine.Audio;

    /// <summary>
    /// Provides as a proxy between UI updates and the static Settings class
    /// </summary>
    public class SettingsProxy : MonoBehaviour
    {

        [SerializeField]
        private AudioMixer mixer;

        private void Start()
        {
            //LoadAndApplySettings();
        }


        public void ApplyChanges()
        {
            Settings.NotifyChanges();
        }

        public void CancelChanges()
        {

        }

        #region SettingProperties

        /// <summary>
        /// Updates the speed of enemies' animations
        /// </summary>
        /// <param name="speed"></param>
        public void UpdateAnimationSpeed(float speed)
        {
            Settings.AnimationTime = speed / 10f;

            Debug.Log("Set to: " + speed);
        }

        /// <summary>
        /// Updates master volume level
        /// </summary>
        /// <param name="volumeLevel"></param>
        public void UpdateMasterVolume(float volumeLevel)
        {
            Settings.MasterVolume = volumeLevel;

            this.mixer.SetFloat("Master_Volume", volumeLevel);
        }

        /// <summary>
        /// Updates SFX volume level
        /// </summary>
        /// <param name="volumeLevel"></param>
        public void UpdateSfxVolume(float volumeLevel)
        {
            Settings.SfxVolume = volumeLevel;

            this.mixer.SetFloat("Sfx_Volume", volumeLevel);
        }

        /// <summary>
        /// Updates music volume level
        /// </summary>
        /// <param name="volumeLevel"></param>
        public void UpdateMusicVolume(float volumeLevel)
        {
            Settings.MusicVolume = volumeLevel;

            this.mixer.SetFloat("Music_Volume", volumeLevel);
        }

        #endregion



    }
}
