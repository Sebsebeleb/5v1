namespace Tutorial
{
    using UnityEngine;
    public class TutorialPropEnableOnActivated : MonoBehaviour
    {
        [SerializeField]
        private string ID;

        /// <summary>
        /// When this component is enabled, this transform will be enabled if it is supposed to be shown
        /// </summary>
        public Transform TargetTransform;

        public void OnEnable()
        {
            if (!TutorialManager.ShouldDisplayTutorialProp(this.ID))
            {
                gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }

            // Disables some inputs for a little while to prevent clicking past too fast

            TutorialManager.PauseInput(1.1f);
        }
    }
}