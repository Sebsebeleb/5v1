using UnityEngine;

namespace BBG.Settings
{
    /// Global settings loaded from playerprefab
    public static class Settings
    {
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
    }
}