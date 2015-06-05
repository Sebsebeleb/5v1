using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    private int _xp;

    public void GiveXp(int xp)
    {
        _xp += xp;
    }
}