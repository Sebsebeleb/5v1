using System;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    public delegate int GetPriority();

    private List<AiAction> _actions = new List<AiAction>();
    private List<AiAction> _freeActions = new List<AiAction>();

    // Register an action the ai can do with 0 arguments, returning an ACtionPriorty describing the priority of it and the action to take if it is chosen
    /// <summary>
    /// Register an action the ai can take on its turn.
    /// </summary>
    /// <param name="action">A function that proveds a PossibleActions giving the priority of the action and a reference to the method to call if it is chosen</param>
    /// <param name="freeAction">If this action is free, the AI will use it each turn in addition to whatever main action it chooses</param>
    public void AddAction(AiAction action)
    {
        if (action.IsFreeAction)
        {
            _freeActions.Add(action);
            
        }
        else
        {
            _actions.Add(action);
        }
    }
    
    public List<AiAction> GetFreeActions(){
        return _freeActions;
    }
    
    public List<AiAction> GetStandardActions(){
        return _actions;
    }

    public void Think()
    {
        // Free actions are used first
        foreach (AiAction freeAction in _freeActions) {
            freeAction.Callback();
        }

        
        _actions.Sort((a, b) => a.CalcPriority().CompareTo(b.CalcPriority()));

        if (_actions.Count >= 1){
            _actions[0].Callback();
        }

    }
    
    // The class used to describe an action
    public class AiAction {
        public string Name;
        public Func<string> Description; // A method that describes this action. Can be rich text
        public GetPriority CalcPriority; // Method to call to calculate priority
        public bool IsFreeAction; // If it is free it will be performed in addition to the chose standard action
        public Action Callback; // The method to call if we perform this action
    }
}