namespace BaseClasses
{
    using UnityEngine;

    public class Specialization : ITooltip
    {
        private readonly string Name;

        private readonly string Tooltip;

        private readonly string effectName;

        private Color displayColor;

        private BaseSkill.SkillCategory focus;

        /// <summary>
        /// Color of the title
        /// </summary>
        public Color DisplayColor
        {
            get
            {
                return displayColor;
            }

            set
            {
                displayColor = value;
            }
        }

        public BaseSkill.SkillCategory Focus
        {
            get
            {
                return focus;
            }
            private set
            {
                focus = value;
            }
        }

        /// <summary>
        /// Define a new Specialization
        /// </summary>
        /// <param name="name">Name of the specialization</param>
        /// <param name="tooltip">Rich text tooltip</param>
        /// <param name="effectOnLearn">This effect will be given to the player when the specialization is learned, this effect should have the actual behaviour</param>
        /// <param name="_displayColor">Color of title</param>
        /// <param name="focus">What categories this specialization provides focus on</param>
        public Specialization(string name, string tooltip, string effectOnLearn, Color _displayColor, BaseSkill.SkillCategory focus)
        {
            this.focus = focus;
            this.Name = name;
            this.Tooltip = tooltip;
            this.effectName = effectOnLearn;
            this.DisplayColor = _displayColor;
        }

        public virtual string GetName()
        {
            return this.Name;
        }

        public virtual string GetTooltip()
        {
            return this.Tooltip;
        }

        /// <summary>
        /// Called when the player learns this specialization. Should init effects etc.
        /// </summary>
        public void OnLearned()
        {
            Effect v = GameResources.LoadEffect(this.effectName);

            Actor.Player.effects.AddEffect(v);
        }
    }
}