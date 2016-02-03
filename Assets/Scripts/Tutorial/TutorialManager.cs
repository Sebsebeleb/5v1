namespace Tutorial
{

    using UnityEngine;
    public class TutorialManager
    {
        private static bool initalized = false;

        private static bool disabled = false;

        private static float inputPause = 0;



        /// <summary>
        /// Pause some input commands for a little while
        /// </summary>
        /// <returns></returns>
        public static bool IsInputPaused()
        {
            return Time.time < inputPause;

        }

        /// <summary>
        /// Pauses some inputs for a little while
        /// </summary>
        /// <param name="delay"></param>
        public static void PauseInput(float delay)
        {
            inputPause = Time.time + delay;
        }


        public static bool tutorialDisabled
        {
            get
            {
                if (!initalized)
                {

                    disabled = (PlayerPrefs.GetInt("tutorial_disabled") == 1);
                    initalized = true;
                }

                return disabled;
            }

            set
            {
                disabled = value;
            }
        }

        /// <summary>
        /// Should the tutorial with this ID be displayed?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool ShouldDisplayTutorialProp(string id, bool disableProp = true)
        {
            if (tutorialDisabled)
            {
                return false;
            }

            if (PlayerPrefs.GetInt("tutorial_" + id) == 1)
            {
                return false;
            }

            if (disableProp)
            {
                PlayerPrefs.SetInt("tutorial_" + id, 1);
            }

            return true;
        }

        public static void DisableTutorialProp(string id)
        {
            PlayerPrefs.SetInt("tutorial_" + id, 1);
        }
    }
}