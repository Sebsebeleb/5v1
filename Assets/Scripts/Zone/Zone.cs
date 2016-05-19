namespace BBG.Zone
{
    using System;
    using System.Collections.Generic;

    using BBG.Actor;
    using BBG.Data;

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

        public int ZoneLength = 60; // The bosscounter will start at this number
        public string Denizens;

        // Modifies the diffuculty of enemies spawned.
        public float EnemyDifficultyMod = 1.0f;

        // Common modifications to the zone
        #region CommonMods
        private float _enemyDamageModifier = 1.0f;
        public float EnemyDamageModifier
        {
            get { return this._enemyDamageModifier; }
            set { this._enemyDamageModifier = value; }
        }

        private float _enemyHealthModifier = 1.0f;

        public float EnemyHealthModifier
        {
            get { return this._enemyHealthModifier; }
            set { this._enemyHealthModifier = value; }
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
            // Make sure there are no old enemeis alive (like due to the debug menu generating a new zone before finish)
            foreach (Actor enemy in GridManager.TileMap.GetAll())
            {
                EnemyManager.KillEnemy(enemy);
            }

            OnPostEnemySpawned callback = this.HandleEnemySpawned;
            // Initialize a generic callback for modifying creatures as they spawn based on common zone stats
            EventManager.Register(Events.PostEnemySpawned, callback);
            foreach (ZoneModifier mod in this.modifiers)
            {
                mod.ApplyCallbacks();
            }

            // Kinda temp, spawn some enemies at the beginning of a new zone so it isnt completely barren
            EnemyManager.SpawnRandomEnemy(0, 0);
            EnemyManager.SpawnRandomEnemy(1, 0);
            EnemyManager.SpawnRandomEnemy(2, 0);
        }

        // Apply modifications to enemies when they spawned based on zone effects
        private void HandleEnemySpawned(Actor who)
        {
            who.damagable.BonusMaxHealthPercent = this.EnemyHealthModifier - 1.0f;
        }
    }
}
