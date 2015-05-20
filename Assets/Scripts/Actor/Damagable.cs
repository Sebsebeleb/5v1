using System;
using System.Collections;
using Event;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0) {
            Die();
        }


        EventManager.Notify(Events.OnActorTookDamage, new TookDamageArgs(enemy, damage));
    }

    public void Die(bool givexp = true)
    {
        if (givexp) {
            GameObject.FindWithTag("Player").GetComponent<PlayerExperience>().GiveXp(2);
        }
    }
}