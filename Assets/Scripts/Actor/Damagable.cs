using Event;
using Generation;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Damagable : MonoBehaviour
{

    // Public fields for setting up inital stats in UNITY!
    #region basestats
    [SerializeField]
    private int BaseHealth;

    #endregion
    private HealthData data;

    private bool isDead;

    public int MaxHealth
    {
        get { return data.MaxHealth + data.BonusMaxHealth; }
        private set { data.MaxHealth = value; }
    }
    public int CurrentHealth
    {
        get { return data.CurrentHealth; }
        set { data.CurrentHealth = value; }
    }

    public int BonusMaxHealth
    {
        get { return data.BonusMaxHealth; }
        set { data.BonusMaxHealth = value; }
    }

    public float BonusMaxHealthPercent
    {
        get
        {
            return this.data.BonusMaxHealthPercent;
        }
        set
        {
            this.data.BonusMaxHealthPercent = value;
        }
    }



    public float DamageReductionPercent { get; set; }

    public int IsInvulnerable = 0; // If this is higher than 0, you can evaluate it as true, otherwise false.

    private Actor actor;

    void Awake()
    {
        actor = GetComponent<Actor>();

    }

    void Start()
    {
        if (tag == "Player")
        {
            OnSpawn();
        }
    }

    public void OnSpawn()
    {
        MaxHealth = BaseHealth;
        int bonusHealth = this.BonusMaxHealth;
        
        if (gameObject.tag != "Player")
        {
            bonusHealth += (int)Math.Round(MaxHealth * BonusMaxHealthPercent);
        }
        CurrentHealth = MaxHealth + bonusHealth;
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

        if (CurrentHealth <= 0 && !isDead) // cannot die multiple times yo
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
        DropItem();
        isDead = true;

        // Allow effects that trigger on death
        EventManager.Notify(Events.ActorPreDied, actor);

        // If we started the boss, enemies dont grant xp anymore (besides defeating the whole boss wave (NYI))
        if (givexp && TurnManager.BossCounter > 0)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerExperience>().GiveXp(3);
        }

        actor.effects.Cleanup();

        //TODO: Consider; Should this object have this responsibility?
        EventManager.Notify(Events.ActorDied, actor);
        EnemyManager.KillEnemy(actor);

    }

    /// <summary>
    /// Temporary function, sometiems drops items on death
    /// </summary>
    private void DropItem()
    {
        if (Random.Range(0, 100) > 40)
        {
            var item = ItemGenerator.GenerateItem(GeneratedItemType.Equipment);

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>().AddItem(item);
        }
    }

    public HealthData _GetRawData()
    {
        return data;
    }

    public void _SetRawData(HealthData _data)
    {
        data = _data;
    }
}