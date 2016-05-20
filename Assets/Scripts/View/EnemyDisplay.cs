namespace BBG.View
{
    using BBG.Actor;
    using BBG.Data;
    using BBG.Map;
    using BBG.ResourceManagement;
    using BBG.View.CommonUI;
    using DG.Tweening;
    using DG.Tweening.Core;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class EnemyDisplay : MonoBehaviour
    {
        // Public access to all the buttons
        public static Dictionary<GridPosition, EnemyDisplay> Displays = new Dictionary<GridPosition, EnemyDisplay>();

        private Actor actor;

        public SegmentedBar CooldownBar, HealthBar;

        public Image EnemyImage, Targeting, TargetingAffected;

        private GridButtonBehaviour gridbutton;

        public Text Name, Health, Cooldown, Attack, Defense, Rank;

        // Used to detect changes
        private int OldCooldown, OldMaximumCooldown;

        void Awake()
        {
            this.HealthBar.NumSegments = 2;
            this.HealthBar.SegmentSpacing = 1;

            this.gridbutton = this.GetComponent<GridButtonBehaviour>();

            // Register a callback to be called after a game was loaded, to correctly update display
            OnGameDeserialized callback = this.GameWasLoaded;
            EventManager.Register(Events.GameDeserialized, callback);

            Displays[new GridPosition(this.gridbutton.x, this.gridbutton.y)] = this;
        }

        void Start()
        {
        }

        void Update()
        {
            this.actor = GridManager.TileMap.GetAt(this.gridbutton.x, this.gridbutton.y);

            this.Rank.text = this.actor.Rank.ToString();

            string actualName = this.actor.name;
            // Strip "(Clone)"
            while (actualName.EndsWith("(Clone)"))
            {
                actualName = actualName.Substring(0, actualName.Length - 7);
            }
            this.Name.text = actualName;

            //
            // Countdown text
            //

            if (this.actor.countdown.CurrentCountdown != this.OldCooldown
                || this.actor.countdown.MaxCountdown != this.OldMaximumCooldown)
            {
                // Make bar fill update
                float targetFill = (float)this.actor.countdown.CurrentCountdown / this.actor.countdown.MaxCountdown;
                float duration = 0.2f;

                // Tween the update using DOTween generic tween
                DOTween.To(
                    new DOGetter<float>(() => { return this.CooldownBar.Fill; }),
                    new DOSetter<float>((value => { this.CooldownBar.Fill = value; })),
                    targetFill,
                    duration);

                this.CooldownBar.NumSegments = this.actor.countdown.MaxCountdown;
                this.CooldownBar.SegmentSpacing = 1 / (this.actor.countdown.MaxCountdown * 0.88f);

                this.OldCooldown = this.actor.countdown.CurrentCountdown;
                this.OldMaximumCooldown = this.actor.countdown.MaxCountdown;
            }
            string countdownText = this.actor.countdown.CurrentCountdown.ToString();

            // If the enemy is stunned, changed font color
            if (this.actor.status.Stunned)
            {
                countdownText = TextUtilities.FontColor("#4040FF", countdownText);
            }
            this.Cooldown.text = countdownText;

            //
            // Sprite
            //

            this.EnemyImage.sprite = this.actor.GetComponent<DisplayData>().Image;

            float hpFillTarget;

            // Special display for corpses
            if (actualName == "Corpse")
            {
                this.Attack.text = "-";
                this.Health.text = "-";
                hpFillTarget = 0f;
            }

            else
            {
                this.Health.text = this.actor.damagable.CurrentHealth.ToString();
                hpFillTarget = (float)this.actor.damagable.CurrentHealth / this.actor.damagable.MaxHealth;

                // Make attack text TODO: Should only be updated when attack is updated
                string attackText = this.actor.attack.CalculateAttack().ToString();
                //If enemy has bonus attack, display it in green. If negative, red
                if (this.actor.attack.BonusAttack > 0)
                {
                    attackText = TextUtilities.FontColor("#20DD20FF", attackText);
                }
                else if (this.actor.attack.BonusAttack < 0)
                {
                    attackText = TextUtilities.FontColor("#DD2020FF", attackText);
                }

                this.Attack.text = attackText;
            }

            // Tween health bar
            UpdateHealth(hpFillTarget);
        }

        private void UpdateHealth(float targetFill)
        {
            float duration = 0.4f;
            // Tweens the hpbar 
            DOTween.To(
                new DOGetter<float>(() => { return this.HealthBar.Fill; }),
                new DOSetter<float>((value => { this.HealthBar.Fill = value; })),
                targetFill,
                duration).SetEase(Ease.OutQuad);
        }

        // This is called after loadign a save, correts its image and name display
        private void GameWasLoaded()
        {
            this.actor = GridManager.TileMap.GetAt(this.gridbutton.x, this.gridbutton.y);

            GameObject prefab = GameResources.GetEnemyByID(this.actor.enemyTypeID);

            // A pretty silly way, but it works. Basically, since name and image is usually only set when the enemy is spawned, we force it to change.
            this.actor.GetComponent<DisplayData>().Image = prefab.GetComponent<DisplayData>().Image;
            this.actor.name = prefab.name;
        }
    }
}