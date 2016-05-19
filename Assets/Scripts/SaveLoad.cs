using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

namespace BBG
{
    using BBG.Actor;
    using BBG.DataHolders;
    using BBG.ResourceManagement;
    using BBG.Serialization;

    public class SaveLoad : MonoBehaviour {

        // A class for saving all the game data
        [System.Serializable]
        public struct SaveState{
            public int BossCounter;

            public ActorState[] Enemies;

            public ActorState Player;
            public int PlayerLevel;
            public int PlayerExp;
        }

        [System.Serializable]
        // All save data related to a single enemy
        public struct ActorState{
            public int EnemyTypeID;
            public int GridX;
            public int GridY;
            public AttackData Attack;
            public HealthData Health;
            public CountdownData Countdown;
            public List<Dictionary<string, object>> EffectHolderEffects;
        }

        private GameObject managerObject;
        private GameObject Player;

        private SaveState data; // The one currently being saved to/loaded from

        void Awake(){
            this.managerObject = GameObject.FindWithTag("GM");
            this.Player = GameObject.FindWithTag("Player");
        }


        ///
        /// Entry points to save/load functionality.
        ///

        // Saves the game under /savedGames.sav on persistentDataPath under a binary format
        public void Save() {
            this.MakeSaveGameData();
            this.SaveToFile();
        }

        // Loads the game from /savedGames.sav on persistantDataPath from a binary format
        public void Load() {
            if(File.Exists(Application.persistentDataPath + "/savedGames.sav")) {
                // First open file and get the binary data;
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/savedGames.sav", FileMode.Open);
                this.data = (SaveState)bf.Deserialize(file);
                file.Close();

                // Now use this data to initalize the new game state
                this.InitializeSave();
            }
        }

        // Saves the currently constructed data to a file in persistent data path
        private void SaveToFile(){
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Create (Application.persistentDataPath + "/savedGames.sav");
            bf.Serialize(file, this.data);
            file.Close();
        }

        /// Constructs the data to be saved
        private SaveState MakeSaveGameData(){
            this.data = new SaveState();

            this.SavePlayer();
            this.SaveEnemies();

            this.data.BossCounter = TurnManager.BossCounter;

            return this.data;
        }

        // Initializes all the data loaded from the save
        private void InitializeSave(){

            this.LoadPlayer();
            this.LoadEnemies();

            TurnManager.BossCounter = this.data.BossCounter;

            EventManager.Notify(Events.GameDeserialized, null);
        }

        ///
        /// Various functions for saving/loading different parts of the game
        ///

        private void SavePlayer(){
            Actor.Actor act = this.Player.GetComponent<Actor.Actor>();

            this.data.Player.Attack = act.attack._GetRawData();
            this.data.Player.Health = act.damagable._GetRawData();
            this.data.Player.EffectHolderEffects = EffectSerializer.Serialize(act.effects._GetRawData());

            PlayerExperience pe = act.GetComponent<PlayerExperience>();

            this.data.PlayerExp = pe.GetCurrentXP();
            this.data.PlayerLevel = pe.level;

        }
        private void LoadPlayer(){
            Actor.Actor act = this.Player.GetComponent<Actor.Actor>();

            act.attack._SetRawData(this.data.Player.Attack);
            act.damagable._SetRawData(this.data.Player.Health);
            act.effects._SetRawData(EffectSerializer.Deserialize(this.data.Player.EffectHolderEffects));

            PlayerExperience pe = act.GetComponent<PlayerExperience>();

            pe._SetRawExp(this.data.PlayerExp);
            pe.level = this.data.PlayerLevel;
        }

        private void SaveEnemies(){
            this.data.Enemies = new ActorState[6];

            int i = 0;
            foreach(Actor.Actor enemy in GridManager.TileMap.GetAll()){
                ActorState actData = this.SaveEnemy(enemy.gameObject);

                this.data.Enemies[i] = actData;

                i++;
            }
        }

        // Convert an enemy to an ActorState for serialization.
        private ActorState SaveEnemy(GameObject enemy){
            ActorState actData = new ActorState();

            Actor.Actor act = enemy.GetComponent<Actor.Actor>();

            actData.EnemyTypeID = act.enemyTypeID;

            actData.GridX = act.x;
            actData.GridY = act.y;

            actData.Attack = act.attack._GetRawData();
            actData.Health = act.damagable._GetRawData();
            actData.Countdown = act.countdown._GetRawData();
            actData.EffectHolderEffects =EffectSerializer.Serialize(act.effects._GetRawData());

            return actData;
        }

        private void LoadEnemies(){
            foreach(ActorState enemyData in this.data.Enemies){
                this.LoadEnemy(enemyData);
            }
        }

        // Initalizes enemies using data loaded as ActorState from a saved game
        private void LoadEnemy(ActorState enemyData){
            int x = enemyData.GridX;
            int y = enemyData.GridY;

            // First we destroy the old one
            Actor.Actor oldActor = GridManager.TileMap.GetAt(x, y);
            EnemyManager.KillEnemy(oldActor);

            // Now we create a new one from the template
            GameObject enemyPrefab = GameResources.GetEnemyByID(enemyData.EnemyTypeID);
            EnemyManager.SpawnEnemy(enemyPrefab, x, y, false);

            Actor.Actor enemyActor = GridManager.TileMap.GetAt(x, y);

            // Then populate it's data
            enemyActor.enemyTypeID = enemyData.EnemyTypeID;

            enemyActor.damagable._SetRawData(enemyData.Health);
            enemyActor.attack._SetRawData(enemyData.Attack);
            enemyActor.countdown._SetRawData(enemyData.Countdown);

            enemyActor.effects._SetRawData(EffectSerializer.Deserialize(enemyData.EffectHolderEffects));
        }
    }
}