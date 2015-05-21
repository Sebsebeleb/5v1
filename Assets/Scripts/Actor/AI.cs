using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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

    // Register an action the ai can do with 0 arguments, returning an ACtionPriorty describing the priority of it and the action to take if it is chosen
    public void AddAction(PossibleActions action)
    {
        _actions.Add(action);
    }

    public void Think()
    {
        List<ActionPriority> priorities = new List<ActionPriority>();

        foreach (PossibleActions action in _actions) {
            priorities.Add(action());           
        }

        // Sort it by priority
        priorities.Sort((a, b) => a.priority.CompareTo(b.priority));

        // Callback the highest priority action
        if (priorities.Count >= 1) {
            priorities[0].callback();
        }
    }

}