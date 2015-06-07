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
        AI.AiAction attackAction = new AI.AiAction();
        attackAction.Name = "Attack";
        attackAction.Description = "Attacks you for x damage";
        attackAction.Callback = AttackPlayer;
        attackAction.CalcPriority = CalculatePriority;
        attackAction.IsFreeAction = false;
        
        _brain.AddAction(attackAction);
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
