using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{

    public delegate int PossibleActions();

    private PossibleActions possible;

    public void AddAction(PossibleActions action)
    {
        possible += action;
    }

    public void Think()
    {
        possible.Invoke();
    }

}
