using UnityEngine;

public class EnemyAttackAI : MonoBehaviour
{

    private AI _brain;
    private Actor actor;

    private GameObject player;
    private Damagable playerDamage;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerDamage = player.GetComponent<Damagable>();

        _brain = GetComponent<AI>();
        actor = GetComponent<Actor>();
    }

    void Start()
    {
        _brain.AddAction(AttackAction);
    }

    private AI.ActionPriority AttackAction()
    {
        return new AI.ActionPriority(CalculatePriority(), AttackPlayer);
    }

    private int CalculatePriority()
    {
        return 50;
    }

    private void AttackPlayer()
    {
        playerDamage.TakeDamage(actor.attack.Attack);
    }
}
