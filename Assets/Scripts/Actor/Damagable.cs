﻿using Event;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;

    private Actor actor;

    void Awake()
    {
        actor = GetComponent<Actor>();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

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


        EventManager.Notify(Events.OnActorTookDamage, new TookDamageArgs(actor, damage));
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
            GameObject.FindWithTag("Player").GetComponent<PlayerExperience>().GiveXp(2);
        }

        //TODO: Consider; Should this object have this responsibility?
        EventManager.Notify(Events.ActorDied, actor);
        EnemyManager.KillEnemy(actor);
    }
}