using UnityEngine;

namespace BBG.Settings
{
    using System;

    /// Global settings loaded from playerprefab
    public static class Settings
    {

        public static event Action SettingsChanged;

        public static float AnimationTime
        {
            get
            {
                return loadFloat("AnimationTime", 0.5f);
            }

            set
            {
                setFloat("AnimationTime", value);
            }

        }

        // The duration for icons that appear when an effect is applied
        public static float IconAnimationTime
        {
            get
            {
                return loadFloat("IconAnimationTime", 1.25f);
            }
            set
            {
                setFloat("IconAnimationTime", value);
            }
        }

        #region Audio
        public static float MasterVolume
        {
            get
            {
                return loadFloat("Audio_MasterVolume", 0.75f);
            }
            set
            {
                setFloat("Audio_MasterVolume", value);
            }
        }

        public static float MusicVolume
        {
            get
            {
                return loadFloat("Audio_MusicVolume", 1f);
            }
            set
            {
                setFloat("Audio_MusicVolume", value);
            }
        }

        public static float SfxVolume
        {
            get
            {
                return loadFloat("Audio_SfxVolume", 1f);
            }
            set
            {
                setFloat("Audio_SfxVolume", value);
            }
        }
        #endregion


        private static int loadInt(string identifier, int fallback)
        {
            return PlayerPrefs.GetInt(identifier, fallback);
        }

        private static float loadFloat(string identifier, float fallback)
        {
            float f;
            float.TryParse(PlayerPrefs.GetString(identifier, fallback.ToString()), out f);

            return f;
        }

        private static void setInt(string identifier, int val)
        {
            PlayerPrefs.SetInt(identifier, val);
        }

        private static void setFloat(string identifier, float val)
        {
            PlayerPrefs.SetString(identifier, val.ToString());
        }

        /// <summary>
        /// Should be called whenever settings are updated, to update appropriate parts of the game.
        /// </summary>
        public static void NotifyChanges()
        {
            var handler = SettingsChanged;
            if (handler != null) handler();
        }
    }
}