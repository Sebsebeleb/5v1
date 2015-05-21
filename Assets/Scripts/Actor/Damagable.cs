using System;
using System.Collections;
using Event;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;

    private Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0) {
            if (gameObject.tag == "Player") {
                Lose();
            }
            else {
                Die();
            }
        }


        EventManager.Notify(Events.OnActorTookDamage, new TookDamageArgs(enemy, damage));
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
        if (givexp) {
            GameObject.FindWithTag("Player").GetComponent<PlayerExperience>().GiveXp(2);
        }

        EnemyManager.KillEnemy(enemy);
    }
}