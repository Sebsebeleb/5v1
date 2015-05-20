using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{

    public void DoAttack(Enemy target)
    {
        int damage = 1;

        target.damagable.TakeDamage(damage);

        //if target.tag != player: EventManager.Broadcast("EnemyTookDamage")
    }

    public bool CanAttack(int x, int y)
    {
        return true;
    }
}