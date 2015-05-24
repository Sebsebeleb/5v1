
// A simple generic attack buff
using BaseClasses;

class AttackBuff : Effect
{
    private int bonus;

    public AttackBuff(int attackBonus)
    {
        bonus = attackBonus;
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
