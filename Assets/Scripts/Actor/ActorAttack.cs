﻿using System;
using UnityEngine;

public class ActorAttack : MonoBehaviour
{

    // The actual stats relating to attacking. This can be saved/loaded etc.
    private AttackData data = new AttackData(); 

    // For inspector
    public int StartingBaseAttack;

    [SerializeField]
    private int attackPerRank;

    private Actor actor;


    // The regions are for public read/write of data
    #region properties
    public int BonusAttack
    {
        get { return data.BonusAttack; }
        set { data.BonusAttack = value; }
    }

    #endregion

    public int Attack
    {
        get { return CalculateAttack(); }
    }


    private void Awake()
    {
        this.actor = GetComponent<Actor>();
    }

    void Start()
    {
        if (tag == "Player")
        {
            OnSpawn();
        }
    }

    void OnSpawn()
    {
        data.BaseAttack = StartingBaseAttack + this.attackPerRank * this.actor.Rank;

        if (gameObject.tag != "Player")
        {
            // Add zone bonus
            int bonus = (int)Math.Round(StartingBaseAttack * (1.0f - Zone.Zone.current.EnemyDamageModifier));
        }

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

    /// <summary>
    /// Returns final standard attack
    /// </summary>
    /// <returns></returns>
    public int CalculateAttack()
    {
        return data.BaseAttack + data.BonusAttack;
    }

    // Return the data object used. Should only be used by serialization classes
    public AttackData _GetRawData()
    {
        return data;
    }

    public void _SetRawData(AttackData _data)
    {
        data = _data;
    }
}