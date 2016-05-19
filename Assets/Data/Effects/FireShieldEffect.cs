namespace BBG.Data.Effects
{
    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects.ThemeDebuffs;

    [System.Serializable]
    internal class FireShieldEffect : Effect
    {
        // Damage dealt when attacked
        private int damage;
        // Bonus damage if already burning
        private int bonusDamage;

        public FireShieldEffect(int duration, int _damage, int _bonusDamage)
            : base(duration)
        {
            this.IsBuff = true;
            this.Description = new EffectDescription(
                "Fire Shielded",
                this.describe
                );

            this.damage = _damage;
            this.bonusDamage = _bonusDamage;
        }

        private string describe()
        {
            string retalitateProp = TextUtilities.FontColor(Colors.DamageValue, this.damage);
            string bonusProp = TextUtilities.FontColor(Colors.DamageValue, this.bonusDamage);

            return string.Format("Whenever an enemy attacks you, you deal {0} damage to them and apply burning(3), and deal an extra {1} damage if they are already burning",
                retalitateProp,
                bonusProp);
        }

        public override void OnAdded()
        {
            base.OnAdded();

            EventManager.Register(Events.EnemyAttack, (OnEnemyAttack)this.Retalitate);
        }

        public override void OnRemoved()
        {
            base.OnRemoved();

            EventManager.UnRegister(Events.EnemyAttack, (OnEnemyAttack)this.Retalitate);
        }

        private void Retalitate(EnemyAttackArgs args)
        {
            Actor attacker = args.who;

            int finalDamage = this.damage;

            if (attacker.effects.HasEffect<Burning>()){
                finalDamage += this.bonusDamage;
            }

            attacker.damagable.TakeDamage(finalDamage);
            attacker.effects.AddEffect(new Burning(3));
        }
    }
}