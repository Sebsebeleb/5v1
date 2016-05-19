using DG.Tweening;

using UnityEngine;

namespace BBG.View
{
    using BBG.Actor;

    using UnityEngine.UI;

    /// <summary>
    /// Displays general info about the game, like player HP, boss coutner etc.
    /// </summary>
    public class InfoDisplay : MonoBehaviour
    {
        public Text PlayerHealthText;
        public Text BossCounterText;
        public Text PlayerXPText;

        public Text PlayerManaText;

        public Color ManaColour;
        public Color HealthColour;
        public Color XPColour;

        private Damagable _playerHealth;
        private TurnManager _turnManager;
        private PlayerExperience _experience;

        private int oldCurrentHealth;
        private int oldMaxHealth;
        private int oldExperience;

        private int oldMana;

        private int oldMaxMana;

        private

            void Awake()
        {
            this._playerHealth = GameObject.FindWithTag("Player").GetComponent<Damagable>();
            this._turnManager = GameObject.FindWithTag("GM").GetComponent<TurnManager>();
            this._experience = GameObject.FindWithTag("Player").GetComponent<PlayerExperience>();
        }

        void Update()
        {
            int newCurrentHealth = this._playerHealth.CurrentHealth;
            int newMaxHealth = this._playerHealth.MaxHealth;
            int newExperience = this._experience.GetCurrentXP();
            int newMana = Actor.Player.CurrentMana;
            int newMaxMana = Actor.Player.MaxMana.Value;

            // Update Health
            if (newCurrentHealth != this.oldCurrentHealth || newMaxHealth != this.oldMaxHealth){
                this.PlayerHealthText.text = this._playerHealth.CurrentHealth.ToString() + "/" + this._playerHealth.MaxHealth.ToString();
                this.UpdateProp(this.PlayerHealthText, this.HealthColour);
            }
            // Update Experience
            if (newExperience != this.oldExperience){
                this.PlayerXPText.text = this._experience.GetCurrentXP() + "/" + this._experience.GetNeededXP();
                this.UpdateProp(this.PlayerXPText, this.XPColour);
            }

            // Update Mana
            if (newMana != this.oldMana || newMaxMana != this.oldMaxMana)
            {
                this.PlayerManaText.text = string.Format("{0}/{1}", newMana, newMaxMana);
                this.UpdateProp(this.PlayerManaText, this.ManaColour);
                this.oldMana = newMana;
                this.oldMaxMana = newMaxMana;
            }

            // Update Boss counter
            this.BossCounterText.text = TurnManager.BossCounter.ToString();

            this.oldCurrentHealth = newCurrentHealth;
            this.oldMaxHealth = newMaxHealth;
            this.oldExperience = newExperience;
        }

        // Triggers an animation for the text component
        private void UpdateProp(Text component, Color color){
            color.a = 1.0f; // TODO: This is just a stupid temp solution because of linux editor
            component.DOColor(Color.white, 0.3f).OnComplete(() => component.DOColor(color, 0.3f));
            component.transform.DOShakeScale(0.3f, 0.88f, 8, 0);
            //component.transform.DOShakeRotation(0.2f, 30f, 3);
        }

    }
}