using BaseClasses;
using Data.Effects;

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
        Description = new EffectDescription(
            "Fire Shielded",
            describe
        );

        damage = _damage;
        bonusDamage = _bonusDamage;
    }

    private string describe()
    {
        string retalitateProp = TextUtilities.FontColor(Colors.DamageValue, damage);
        string bonusProp = TextUtilities.FontColor(Colors.DamageValue, bonusDamage);

        return string.Format("Whenever an enemy attacks you, you deal {0} damage to them and apply burning(3), and deal an extra {1} damage if they are already burning",
         retalitateProp,
         bonusProp);
    }

    public override void OnAdded()
    {
        base.OnAdded();

        Event.EventManager.Register(Event.Events.EnemyAttack, (Event.OnEnemyAttack)Retalitate);
    }

    public override void OnRemoved()
    {
        base.OnRemoved();

        Event.EventManager.UnRegister(Event.Events.EnemyAttack, (Event.OnEnemyAttack)Retalitate);
    }

    private void Retalitate(Event.EnemyAttackArgs args)
    {
        Actor attacker = args.who;

        int finalDamage = damage;

        if (attacker.effects.HasEffect<Burning>()){
            finalDamage += bonusDamage;
        }

        attacker.damagable.TakeDamage(finalDamage);
        attacker.effects.AddEffect(new Burning(3));
    }
}