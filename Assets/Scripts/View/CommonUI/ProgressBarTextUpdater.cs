namespace BBG.View.CommonUI
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Updates its text component based on progress of parent slider (for example to make 0% - 100% progress bar)
    /// </summary>
    [ExecuteInEditMode]
    public class ProgressBarTextUpdater : MonoBehaviour
    {

        private Slider slider;

        [SerializeField]
        private Text textGraphic;

        private void Awake()
        {
            this.slider = this.GetComponentInParent<Slider>();
        }

        private void Update()
        {
            this.textGraphic.text = string.Format("{0:P1}", this.slider.value);
        }
    }
}