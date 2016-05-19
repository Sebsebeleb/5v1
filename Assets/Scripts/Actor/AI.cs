using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BBG.Actor
{
    using BBG.Interfaces;
    using BBG.View;

    public class AI : MonoBehaviour
    {
        private Actor owner;

        public delegate int GetPriority();

        private List<AiAction> _actions = new List<AiAction>();
        private List<AiAction> _freeActions = new List<AiAction>();

        void Awake(){
            this.owner = this.GetComponent<Actor>();
        }

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
                this._freeActions.Add(action);

            }
            else
            {
                this._actions.Add(action);
            }
        }

        public List<AiAction> GetFreeActions(){
            return this._freeActions;
        }

        public List<AiAction> GetStandardActions(){
            return this._actions;
        }

        public IEnumerator Think()
        {
            // Free actions are used first
            foreach (AiAction freeAction in this._freeActions) {
                while(AnimationManager.IsAnimating()){
                    yield return 0;
                }
                EventManager.Notify(Events.PreEnemyAction, new OnPreEnemyActionArgs(this.owner, freeAction));
                freeAction.Callback();
                AnimationManager.RegisterAnimation();

            }


            this._actions.Sort((a, b) => a.CalcPriority().CompareTo(b.CalcPriority()));

            if (this._actions.Count >= 1){
                while(AnimationManager.IsAnimating()){
                    yield return 0;
                }
                EventManager.Notify(Events.PreEnemyAction, new OnPreEnemyActionArgs(this.owner, this._actions[0]));
                this._actions[0].Callback();
                AnimationManager.RegisterAnimation();
            }

        }

        // The class used to describe an action
        [System.Serializable]
        public class AiAction : ITooltip, IAnimatableChange {
            public string Name;
            public string AnimationName; //Name of animation trigger to launch when this action is taken.
            public Func<string> Description; // A method that describes this action. Can be rich text
            public GetPriority CalcPriority; // Method to call to calculate priority
            public bool IsFreeAction; // If it is free it will be performed in addition to the chose standard action
            public Action Callback; // The method to call if we perform this action
            // IanimatableChange related stuff
            public bool animateThis;
            public ChangeAnimation AnimationInfo;


            // ITooltip methods
            public string GetTooltip(){
                return this.Description();
            }

            public string GetName(){
                return this.Name;
            }

            // IAnimatableChange methods

            public bool ShouldAnimate(){
                return this.animateThis;
            }
            public ChangeAnimation GetAnimationInfo(){
                return this.AnimationInfo; // A default aniamtion has no info anyway
            }
        }
    }
}