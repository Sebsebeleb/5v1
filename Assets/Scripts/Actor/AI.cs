using System;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    // Used for 
    public struct ActionPriority
    {
        public Action callback;

        public int priority;

        public ActionPriority(int pri, Action cb)
        {
            priority = pri;
            callback = cb;
        }
    }

    public delegate ActionPriority PossibleActions();

    private List<Delegate> _actions = new List<Delegate>();
    private List<Delegate> _freeActions = new List<Delegate>();

    // Register an action the ai can do with 0 arguments, returning an ACtionPriorty describing the priority of it and the action to take if it is chosen
    /// <summary>
    /// Register an action the ai can take on its turn.
    /// </summary>
    /// <param name="action">A function that proveds a PossibleActions giving the priority of the action and a reference to the method to call if it is chosen</param>
    /// <param name="freeAction">If this action is free, the AI will use it each turn in addition to whatever main action it chooses</param>
    public void AddAction(PossibleActions action, bool freeAction = false)
    {
        if (freeAction)
        {
            _freeActions.Add(action);
        }
        else
        {
            _actions.Add(action);

        }
    }


    public void Think()
    {
        // Free actions are used first
        foreach (PossibleActions freeAction in _freeActions) {
            freeAction().callback();
        }

        // Now do the main action
        List<ActionPriority> priorities = new List<ActionPriority>();

        foreach (PossibleActions action in _actions)
        {
            priorities.Add(action());
        }

        // Sort it by priority
        priorities.Sort((a, b) => a.priority.CompareTo(b.priority));

        // Callback the highest priority action
        if (priorities.Count >= 1)
        {
            priorities[0].callback();
        }
    }

}