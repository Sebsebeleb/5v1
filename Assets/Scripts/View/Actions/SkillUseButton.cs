using UnityEngine;

namespace BBG.View.Actions
{
    using BBG.BaseClasses;

    using UnityEngine.UI;

    using Debug = UnityEngine.Debug;

    /// <summary>
    /// Selects the active skill for the player to this button's selected skill
    /// </summary>
    public class SkillUseButton : MonoBehaviour
    {
        public Image IconImage;
        public Text CooldownText;

        private GameObject _player;
        private SkillBehaviour _playerSkills;

        private BaseSkill _cachedSkill;

        // The skill that should be used if this button is pressed when targeting
        public BaseSkill AssociatedSkill
        {
        
            get { return this._playerSkills.GetKnownSkills()[this.SkillIndex]; }
        }

        // The #n skill that should be used on player's SkillBehaviour when this is used
        public int SkillIndex;

        public void Awake()
        {
            this._player = GameObject.FindWithTag("Player");
            this._playerSkills = this._player.GetComponent<SkillBehaviour>();
        }

        public void Update()
        {
            if (this.AssociatedSkill != null) {
                this.UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            if (this.AssociatedSkill.CurrentCooldown > 0) {
                this.CooldownText.text = this.AssociatedSkill.CurrentCooldown.ToString();
            }
            else {
                this.CooldownText.text = "";
            }
            if (this.AssociatedSkill != this._cachedSkill) {
                this.UpdateIcon();
                this._cachedSkill = this.AssociatedSkill;
            }
        }

        private void UpdateIcon()
        {
            // TODO: Lazy way but ok
            Sprite icon = Resources.Load<Sprite>(this.AssociatedSkill.SkillName);
            if (icon == null) {
                Debug.LogError("Warning, couldnt load icon for skill with name: " + this.AssociatedSkill.SkillName);
            }
            else {
                this.IconImage.sprite = icon;
            }
        }
    }
}