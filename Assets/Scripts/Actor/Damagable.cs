using System;

using UnityEngine;

using Random = UnityEngine.Random;

namespace BBG.Actor
{
    using BBG.Audio;
    using BBG.DataHolders;
    using BBG.Items.Generation;

    using Debug = UnityEngine.Debug;

    public class Damagable : MonoBehaviour
    {

        // Public fields for setting up inital stats in UNITY!
        #region basestats
        [SerializeField]
        private int BaseHealth;

        [SerializeField]
        private int healthPerRank; // Bonus health per rank

        #endregion
        private HealthData data;

        public int MaxHealth
        {
            get { return this.data.MaxHealth + this.data.BonusMaxHealth; }
            private set { this.data.MaxHealth = value; }
        }
        public int CurrentHealth
        {
            get { return this.data.CurrentHealth; }
            set { this.data.CurrentHealth = value; }
        }

        public int BonusMaxHealth
        {
            get { return this.data.BonusMaxHealth; }
            set { this.data.BonusMaxHealth = value; }
        }

        public float BonusMaxHealthPercent
        {
            get
            {
                return this.data.BonusMaxHealthPercent;
            }
            set
            {
                this.data.BonusMaxHealthPercent = value;
            }
        }



        public float DamageReductionPercent { get; set; }

        public bool Dead { get; private set; }

        public int IsInvulnerable = 0; // If this is higher than 0, you can evaluate it as true, otherwise false.

        private Actor actor;

        void Awake()
        {
            this.actor = this.GetComponent<Actor>();

        }

        void Start()
        {
            if (this.tag == "Player")
            {
                this.OnSpawn();
            }
        }

        public void OnSpawn()
        {
            this.MaxHealth = this.BaseHealth + this.healthPerRank * this.actor.Rank;
            int bonusHealth = this.BonusMaxHealth;
        
            if (this.gameObject.tag != "Player")
            {
                bonusHealth += (int)Math.Round(this.MaxHealth * this.BonusMaxHealthPercent);
            }
            this.CurrentHealth = this.MaxHealth + bonusHealth;
        }

        public void TakeDamage(int damage)
        {

            if (this.gameObject.tag != "Player")
            {
                AudioManager.Trigger("Enemy_TakeHit");
            }
            int finalDamage = damage;

            //If invulnerable, negate all damage
            if (this.IsInvulnerable > 0)
            {
                finalDamage = 0;
            }

            // Flat percent reduction
            // Limit between 0% and 80%
            float actualReduction = Mathf.Min(this.DamageReductionPercent, 0.8f);
            actualReduction = Mathf.Max(actualReduction, 0.0f);

            finalDamage = Mathf.CeilToInt(finalDamage * (1.0f - actualReduction));

            this.CurrentHealth -= finalDamage;

            if (this.CurrentHealth <= 0 && !this.Dead) // cannot die multiple times yo
            {
                if (this.gameObject.tag == "Player")
                {
                    this.Lose();
                }
                else
                {
                    this.Die();
                }
            }


            EventManager.Notify(Events.OnActorTookDamage, new TookDamageArgs(this.actor, finalDamage));
        }

        /// <summary>
        /// Heal current health. Returns total amount healed
        /// </summary>
        /// <param name="amount">Amount to heal</param>
        /// <returns>The actual amount that was healed</returns>
        public int Heal(int amount)
        {
            int oldHP = this.CurrentHealth;

            this.CurrentHealth = Mathf.Min(this.CurrentHealth + amount, this.MaxHealth);

            return this.CurrentHealth - oldHP;
        }

        /// <summary>
        /// Called when the player loses
        /// </summary>
        private void Lose()
        {
            GameStateManager.OnDeath();
            Debug.Log("You lost!");
            GameStateManager.Tg_HasDied = true;
        }

        public void Die(bool givexp = true)
        {
            if (this.gameObject.tag == "TG_Boss")
            {
                GameStateManager.WinCandy();
            }
            AudioManager.Trigger("Enemy_Death");
            //DropItem();
            this.Dead = true;

            // Allow effects that trigger on death
            EventManager.Notify(Events.ActorPreDied, this.actor);

            // If we started the boss, enemies dont grant xp anymore (besides defeating the whole boss wave (NYI))
            if (givexp && TurnManager.BossCounter > 0)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerExperience>().GiveXp(3);
            }

            this.actor.effects.Cleanup();

            //TODO: Consider; Should this object have this responsibility?
            EventManager.Notify(Events.ActorDied, this.actor);
            EnemyManager.KillEnemy(this.actor);


        }

        /// <summary>
        /// Temporary function, sometiems drops items on death
        /// </summary>
        private void DropItem()
        {
            if (Random.Range(0, 100) > 40)
            {
                var item = ItemGenerator.GenerateItem(GeneratedItemType.Equipment);

                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>().AddItem(item);
            }
        }

        public HealthData _GetRawData()
        {
            return this.data;
        }

        public void _SetRawData(HealthData _data)
        {
            this.data = _data;
        }
    }
}