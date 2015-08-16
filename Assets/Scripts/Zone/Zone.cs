using System;
using System.Collections.Generic;

using UnityEngine;

namespace Zone
{
    // A zone is a collection of data, including spawn list for enemies, zone modifiers, and other zone specific stuff.
    // At any point of the game during the main screen, a single zone is active. After finishing a zone, a player has to
    // choose a new zone to enter.
    [Serializable]
    public class Zone
    {

        private static Zone _currentZone = new Zone(); //Default zone

        // The currently active zone
        public static Zone current
        {
            get { return _currentZone; }
        }


        public List<ZoneModifier> modifiers = new List<ZoneModifier>();
        public List<EnemySpawnList> spawnLists;

        public int ZoneLength; // The bosscounter will start at this number
        public string Denizens;

        // Modifies the diffuculty of enemies spawned. 
        public float EnemyDifficultyMod = 1.0f;

        // Common modifications to the zone
        #region CommonMods
        private float _enemyDamageModifier = 1.0f;
        public float EnemyDamageModifier
        {
            get { return _enemyDamageModifier; }
            set { _enemyDamageModifier = value; }
        }

        private float _enemyHealthModifier = 1.0f;

        public float EnemyHealthModifier
        {
            get { return _enemyHealthModifier; }
            set { _enemyHealthModifier = value; }
        }

        #endregion


        public static void SetZone(Zone nextZone)
        {
            _currentZone = nextZone;

            //Initalize the zone effects
            current.Initalize();
        }

        // Called when the zone is chosen to be the next active one. Also should be called when deserialized to setup events etc.
        private void Initalize()
        {

            Event.OnPostEnemySpawned callback = HandleEnemySpawned;
            // Initialize a generic callback for modifying creatures as they spawn based on common zone stats
            Event.EventManager.Register(Event.Events.PostEnemySpawned, callback);
            foreach (ZoneModifier mod in modifiers)
            {
                mod.ApplyCallbacks();
            }
        }

        // Apply modifications to enemies when they spawned based on zone effects
        private void HandleEnemySpawned(Actor who){
            who.damagable.BonusMaxHealth = EnemyHealthModifier - 1.0f;
        }
    }
}
