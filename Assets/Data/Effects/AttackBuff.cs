
// A simple generic attack buff
using BaseClasses;

class AttackBuff : Effect
{
    
    
    private int bonus;

    public AttackBuff(int attackBonus) : base()
    {
        bonus = attackBonus;
    }
    
    // description: Pass it to describe the way it was buffed
    public AttackBuff(int attackBonus, EffectDescription description) : base(){
        bonus = attackBonus;
        Description = description;
    }

    public override void OnAdded()
    {
        base.OnAdded();

        owner.attack.BonusAttack += bonus;
    }

    public override void OnRemoved()
    {
        base.OnRemoved();

        owner.attack.BonusAttack -= bonus;
    }
}
