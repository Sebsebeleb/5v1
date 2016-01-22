
using UnityEngine;

// Main access point for accessing the enemy
public class Actor : MonoBehaviour
{
    #region Cached components

    private Damagable _damagable;
    public Damagable damagable
    {
        get { return _damagable; }
    }

    private CountdownBehaviour _countdown;
    public CountdownBehaviour countdown
    {
        get { return _countdown; }
    }

    private EffectHolder _effects;
    public EffectHolder effects
    {
        get { return _effects; }
    }

    private ActorAttack _attack;
    public ActorAttack attack
    {
        get { return _attack; }
    }

    private AI _ai;
    public AI ai
    {
        get { return _ai; }
        set { _ai = value; } // For serialization
    }

    private Status _status;
    public Status status{
        get { return _status; }
    }

    private PlayerEquipment _equipment;

    public PlayerEquipment equipment
    {
        get
        {
            return this._equipment;
        }
    }

    #endregion

    #region Stats
    
    // Rank of the actor, higher ranks mean more difficult enemy
    public int Rank = 0;

    private int currentMana = 40;

    public readonly ActorAttribute MaxMana = new ActorAttribute("Max Mana", 40);

    public readonly ActorAttribute ManaRegen = new ActorAttribute("Mana Regen", 5);

    #endregion

    [HideInInspector]
    public int x;
    [HideInInspector]
    public int y;

    // Display name of the actor
    public readonly string ActorName;

    // The ID of an enemy, used to find the correct data (from serialization for instance) using GameResources.
    [SerializeField]
    public int enemyTypeID;

    private static Actor player;

    public static Actor Player
    {
        get
        {
            return player ?? (player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>());
        }
    }

    public int CurrentMana
    {
        get
        {
            return currentMana;
        }

        set
        {
            currentMana = value;
        }
    }

    private void Awake()
    {
        this._damagable = GetComponent<Damagable>();
        this._countdown = GetComponent<CountdownBehaviour>();
        this._effects = GetComponent<EffectHolder>();
        this._attack = GetComponent<ActorAttack>();
        this._ai = GetComponent<AI>();
        this._equipment = GetComponent<PlayerEquipment>();
        _status = GetComponent<Status>();
    }

    #region StatCalculations
    // Calculations based on stats etc.

    /// <summary>
    /// Calculates an attack that is based on weapon damage
    /// </summary>
    /// <returns></returns>
    public int GetWeaponDamage(float weaponMultiplier, int bonusDamage)
    {

        int baseDamage = (int) (attack.CalculateAttack() * weaponMultiplier);

        return baseDamage + bonusDamage;
    }

    // Bonus damage to spells could be calculated like this: final bonus = bonus / number of max target hits (or a coefficient)
    /*public int GetSpellDamage(int numberOfTargets, )
    {
        return 
    }*/

    #endregion
}