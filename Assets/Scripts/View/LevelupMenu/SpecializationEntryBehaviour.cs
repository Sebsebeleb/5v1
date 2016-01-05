namespace Scripts.View.LevelupMenu
{
    using BaseClasses;

    using UnityEngine;
    using UnityEngine.UI;

    public class SpecializationEntryBehaviour : MonoBehaviour
    {

        [SerializeField]
        private Image icon;

        [SerializeField]
        private Text title;

        [SerializeField]
        private Text description;

        private Specialization spec;

        public void SetSpecialization(Specialization specialization)
        {
            this.spec = specialization;

            this.icon.sprite = GameResources.LoadSprite(specialization.GetName());

            this.title.text = specialization.GetName();
            this.title.color = specialization.DisplayColor;
            Debug.Log(specialization.DisplayColor);

            this.description.text = specialization.GetTooltip();
        }

        /// <summary>
        /// Returns the specialization bound to this button
        /// </summary>
        public Specialization GetSpecialization()
        {
            return this.spec;
        }

    }
}