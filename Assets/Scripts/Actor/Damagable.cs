using Event;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;

    public float DamageReductionPercent { get; set; }

    public int IsInvulnerable = 0; // If this is higher than 0, you can evaluate it as true, otherwise false.

    private Actor actor;

    void Awake()
    {
        actor = GetComponent<Actor>();
    }

    public void TakeDamage(int damage)
    {

        int finalDamage = damage;

        //If invulnerable, negate all damage
        if (IsInvulnerable > 0)
        {
            finalDamage = 0;
        }

        // Flat percent reduction
        // Limit between 0% and 80%
        float actualReduction = Mathf.Min(DamageReductionPercent, 0.8f);
        actualReduction = Mathf.Max(actualReduction, 0.0f);

        finalDamage = Mathf.CeilToInt(finalDamage * (1.0f - actualReduction));

        CurrentHealth -= finalDamage;

        if (CurrentHealth <= 0)
        {
            if (gameObject.tag == "Player")
            {
                Lose();
            }
            else
            {
                Die();
            }
        }


        EventManager.Notify(Events.OnActorTookDamage, new TookDamageArgs(actor, finalDamage));
    }

    /// <summary>
    /// Heal current health. Returns total amount healed
    /// </summary>
    /// <param name="amount">Amount to heal</param>
    /// <returns>The actual amount that was healed</returns>
    public int Heal(int amount)
    {
        int oldHP = CurrentHealth;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);

        return CurrentHealth - oldHP;
    }

    /// <summary>
    /// Called when the player loses
    /// </summary>
    private void Lose()
    {
        Debug.Log("You lost!");
    }

    public void Die(bool givexp = true)
    {
        if (givexp)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerExperience>().GiveXp(3);
        }

        //TODO: Consider; Should this object have this responsibility?
        EventManager.Notify(Events.ActorDied, actor);
        EnemyManager.KillEnemy(actor);
    }
}