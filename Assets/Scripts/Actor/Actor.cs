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

    private AttackBehaviour _attack;
    public AttackBehaviour attack
    {
        get { return _attack; }
    }

    private AI _ai;
    public AI ai
    {
        get { return _ai; }
    }

    #endregion

    [HideInInspector]
    public int x;
    [HideInInspector]
    public int y;

    void Awake()
    {
        _damagable = GetComponent<Damagable>();
        _countdown = GetComponent<CountdownBehaviour>();
        _effects = GetComponent<EffectHolder>();
        _attack = GetComponent<AttackBehaviour>();
        _ai = GetComponent<AI>();
    }

    void Update()
    {

    }
}