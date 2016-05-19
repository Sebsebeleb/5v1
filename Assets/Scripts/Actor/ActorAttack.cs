using System;

using UnityEngine;

namespace BBG.Actor
{
    using BBG.DataHolders;

    public class ActorAttack : MonoBehaviour
    {

        // The actual stats relating to attacking. This can be saved/loaded etc.
        private AttackData data = new AttackData(); 

        // For inspector
        public int StartingBaseAttack;

        [SerializeField]
        private int attackPerRank;

        private Actor actor;


        // The regions are for public read/write of data
        #region properties
        public int BonusAttack
        {
            get { return this.data.BonusAttack; }
            set { this.data.BonusAttack = value; }
        }

        #endregion

        public int Attack
        {
            get { return this.CalculateAttack(); }
        }


        private void Awake()
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

        void OnSpawn()
        {
            this.data.BaseAttack = this.StartingBaseAttack + this.attackPerRank * this.actor.Rank;

            if (this.gameObject.tag != "Player")
            {
                // Add zone bonus
                int bonus = (int)Math.Round(this.StartingBaseAttack * (1.0f - Zone.Zone.current.EnemyDamageModifier));
            }

        }

        public void DoAttack(Actor target)
        {
            int damage = this.Attack;

            target.damagable.TakeDamage(damage);

            //if target.tag != player: EventManager.Broadcast("EnemyTookDamage")
        }

        public bool CanAttack(int x, int y)
        {
            return true;
        }

        /// <summary>
        /// Returns final standard attack
        /// </summary>
        /// <returns></returns>
        public int CalculateAttack()
        {
            return this.data.BaseAttack + this.data.BonusAttack;
        }

        // Return the data object used. Should only be used by serialization classes
        public AttackData _GetRawData()
        {
            return this.data;
        }

        public void _SetRawData(AttackData _data)
        {
            this.data = _data;
        }
    }
}