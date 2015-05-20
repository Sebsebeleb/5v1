using UnityEngine;
using System.Collections;

// Main access point for accessing the enemy
public class Enemy : MonoBehaviour
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
    #endregion

    [HideInInspector] public int x;
    [HideInInspector] public int y;

    void Awake()
    {
        _damagable = GetComponent<Damagable>();
        _countdown = GetComponent<CountdownBehaviour>();
    }

    void Update()
    {

    }
}