namespace Tutorial
{
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Convenience class to provide functionality for permanently disabling tutorials and such
    /// </summary>
    public class TutorialButtonBehaviours : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent OnClosed;

        [SerializeField]
        private GameObject _rootTransform;

        private GameObject rootTransform
        {
            get
            {
                if (this._rootTransform != null)
                {
                    return this._rootTransform;
                }
                return gameObject;
            }
        }


        /// <summary>
        /// Called when the player doesnt want to show any more tutorials. Disables tutorials
        /// </summary>
        public void DontShowTutorialClicked()
        {
            TutorialManager.tutorialDisabled = true;

            this.rootTransform.SetActive(false);
            this.OnClosed.Invoke();
        }

        /// <summary>
        /// Called when the player clicks the "Ok" button
        /// </summary>
        public void OkButtonClicked()
        {
            this.rootTransform.SetActive(false);
            this.OnClosed.Invoke();
        }
    }
}