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
        attackAction.AnimationName = "Attack";
        attackAction.Description = GetDescription;
        attackAction.Callback = AttackPlayer;
        attackAction.CalcPriority = CalculatePriority;
        attackAction.IsFreeAction = false;

        _brain.AddAction(attackAction);
    }

    private string GetDescription(){
        int damage = actor.attack.Attack;
        string damageText = TextUtilities.Bold(TextUtilities.FontColor("#FF2222", damage.ToString()));
        return "Attacks you for " + TextUtilities.FontColor("#FF2222", damageText) + " damage";
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
