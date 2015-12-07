using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

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
         managerObject = GameObject.FindWithTag("GM");
         Player = GameObject.FindWithTag("Player");
    }


    ///
    /// Entry points to save/load functionality.
    ///

    // Saves the game under /savedGames.sav on persistentDataPath under a binary format
    public void Save() {
        MakeSaveGameData();
        SaveToFile();
    }

    // Loads the game from /savedGames.sav on persistantDataPath from a binary format
    public void Load() {
        if(File.Exists(Application.persistentDataPath + "/savedGames.sav")) {
            // First open file and get the binary data;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.sav", FileMode.Open);
            data = (SaveState)bf.Deserialize(file);
            file.Close();

            // Now use this data to initalize the new game state
            InitializeSave();
        }
    }

    // Saves the currently constructed data to a file in persistent data path
    private void SaveToFile(){
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create (Application.persistentDataPath + "/savedGames.sav");
        bf.Serialize(file, data);
        file.Close();
    }

    /// Constructs the data to be saved
    private SaveState MakeSaveGameData(){
        data = new SaveState();

        SavePlayer();
        SaveEnemies();

        data.BossCounter = TurnManager.BossCounter;

        return data;
    }

    // Initializes all the data loaded from the save
    private void InitializeSave(){

        LoadPlayer();
        LoadEnemies();

        TurnManager.BossCounter = data.BossCounter;

        Event.EventManager.Notify(Event.Events.GameDeserialized, null);
    }

    ///
    /// Various functions for saving/loading different parts of the game
    ///

    private void SavePlayer(){
        Actor act = Player.GetComponent<Actor>();

        data.Player.Attack = act.attack._GetRawData();
        data.Player.Health = act.damagable._GetRawData();
        data.Player.EffectHolderEffects = IntermediateSerializers.EffectSerializer.Serialize(act.effects._GetRawData());

        PlayerExperience pe = act.GetComponent<PlayerExperience>();

        data.PlayerExp = pe.GetCurrentXP();
        data.PlayerLevel = pe.level;

    }
    private void LoadPlayer(){
        Actor act = Player.GetComponent<Actor>();

        act.attack._SetRawData(data.Player.Attack);
        act.damagable._SetRawData(data.Player.Health);
        act.effects._SetRawData(IntermediateSerializers.EffectSerializer.Deserialize(data.Player.EffectHolderEffects));

        PlayerExperience pe = act.GetComponent<PlayerExperience>();

        pe._SetRawExp(data.PlayerExp);
        pe.level = data.PlayerLevel;
    }

    private void SaveEnemies(){
        data.Enemies = new ActorState[6];

        int i = 0;
        foreach(Actor enemy in GridManager.TileMap.GetAll()){
            ActorState actData = SaveEnemy(enemy.gameObject);

            data.Enemies[i] = actData;

            i++;
        }
    }

    // Convert an enemy to an ActorState for serialization.
    private ActorState SaveEnemy(GameObject enemy){
        ActorState actData = new ActorState();

        Actor act = enemy.GetComponent<Actor>();

        actData.EnemyTypeID = act.enemyTypeID;

        actData.GridX = act.x;
        actData.GridY = act.y;

        actData.Attack = act.attack._GetRawData();
        actData.Health = act.damagable._GetRawData();
        actData.Countdown = act.countdown._GetRawData();
        actData.EffectHolderEffects =IntermediateSerializers.EffectSerializer.Serialize(act.effects._GetRawData());

        return actData;
    }

    private void LoadEnemies(){
        foreach(ActorState enemyData in data.Enemies){
            LoadEnemy(enemyData);
        }
    }

    // Initalizes enemies using data loaded as ActorState from a saved game
    private void LoadEnemy(ActorState enemyData){
        int x = enemyData.GridX;
        int y = enemyData.GridY;

        // First we destroy the old one
        Actor oldActor = GridManager.TileMap.GetAt(x, y);
        EnemyManager.KillEnemy(oldActor);

        // Now we create a new one from the template
        GameObject enemyPrefab = GameResources.GetEnemyByID(enemyData.EnemyTypeID);
        EnemyManager.SpawnEnemy(enemyPrefab, x, y, false);

        Actor enemyActor = GridManager.TileMap.GetAt(x, y);

        // Then populate it's data
        enemyActor.enemyTypeID = enemyData.EnemyTypeID;

        enemyActor.damagable._SetRawData(enemyData.Health);
        enemyActor.attack._SetRawData(enemyData.Attack);
        enemyActor.countdown._SetRawData(enemyData.Countdown);

        enemyActor.effects._SetRawData(IntermediateSerializers.EffectSerializer.Deserialize(enemyData.EffectHolderEffects));
    }
}