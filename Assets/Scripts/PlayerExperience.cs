using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int level;
    private int xp;
    private int xpNeeded{
        get {return CalculateXpNeeded();}
    }
    
    void Start(){
        level = 1;
        
        foreach(int i in new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}){
            level = i;
            Debug.Log(xpNeeded);
        }
    }

    public void GiveXp(int xp)
    {
        xp += xp;
        
        CheckLevelup();
    }
    
    private void CheckLevelup(){
        if (xp >= xpNeeded){
            Levelup();
        }
    }
    
    public void Levelup(){
        
    }
    
    private int CalculateXpNeeded(){
        float xpNeed;
        xpNeed = Mathf.Pow(level, 2.6f) * 0.3f + level * 2 + 10;
        
        return Mathf.CeilToInt(xpNeed);
    }
    
}