using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int level;
    private int _xp;
    private int xpNeeded{
        get {return CalculateXpNeeded();}
    }

    void Start(){
        level = 1;
    }

    public void GiveXp(int xp)
    {
        _xp += xp;

        CheckLevelup();
    }

    private void CheckLevelup(){
        if (_xp >= xpNeeded){
            Levelup();
        }
    }

    public void Levelup(){
        _xp = _xp - xpNeeded;
        level++;
        Event.EventManager.Notify(Event.Events.PlayerLeveledUp, level);
    }

    private int CalculateXpNeeded(){
        float xpNeed;
        xpNeed = Mathf.Pow(level, 2.6f) * 0.4f + level * 2 + 10;

        return Mathf.CeilToInt(xpNeed);
    }

    public int GetCurrentXP(){
        return _xp;
    }

    public int GetNeededXP(){
        return xpNeeded;
    }

    public void _SetRawExp(int xp){
        _xp = xp;
    }

}