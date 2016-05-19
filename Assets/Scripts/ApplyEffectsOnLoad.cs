//Instatiates and adds to EventHolder all specified (by name) of this creature

using UnityEngine;

namespace BBG
{
    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.ResourceManagement;

    public class ApplyEffectsOnLoad : MonoBehaviour
    {
        public string[] InitialEffects;
        // These are added even if the game is loaded
        public string[] Traits;

        private EffectHolder _effects;

        private void Awake()
        {
            this._effects = this.GetComponent<EffectHolder>();
        }

        private void Start()
        {
            this.AddTraits();
        }

        public void OnSpawn(){
            this.AddEffects();
        }

        // Call this to apply the effects.
        private void AddEffects(){
            foreach (string effectName in this.InitialEffects)
            {
                Effect eff = GameResources.LoadEffect(effectName);
                this._effects.AddEffect(eff);
            }
        }

        private void AddTraits(){
            foreach(string effectName in this.Traits){
                Effect eff = GameResources.LoadEffect(effectName);
                this._effects.AddEffect(eff);
            }
        }

    }
}