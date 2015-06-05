using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{

    // For inspector
    public int StartingBaseAttack;

    private int _baseAttack;
    [HideInInspector]
    public int BonusAttack;

    public int Attack
    {
        get { return CalculateAttack(); }
    }


    void Start()
    {
        _baseAttack = StartingBaseAttack;
    }

    public void DoAttack(Actor target)
    {
        int damage = Attack;

        target.damagable.TakeDamage(damage);

        //if target.tag != player: EventManager.Broadcast("EnemyTookDamage")
    }

    public bool CanAttack(int x, int y)
    {
        return true;
    }

    public int CalculateAttack()
    {
        return _baseAttack + BonusAttack;
    }
}