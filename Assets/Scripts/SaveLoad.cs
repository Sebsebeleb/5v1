using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;

public class SaveLoad : MonoBehaviour {

	// A class for saving all the game data
    [System.Serializable]
	private struct SaveState{
		//public TurnManager turnData;
        //public Map.GridMap enemyMap; //For GridManager.TileMap

        public ActorState[] Enemies;

        public ActorState Player;
        public HealthData PlayerHealth;
	}

    [System.Serializable]
    // All save data related to a single enemy
    private struct ActorState{
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


    public void Save() {
        MakeSaveGameData();
        SaveToFile();
    }

    public void Load() {
        if(File.Exists(Application.persistentDataPath + "/savedGames.sav")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.sav", FileMode.Open);
            data = (SaveState)bf.Deserialize(file);
            file.Close();

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
        SaveState data = new SaveState();

        //data.turnData = managerObject.GetComponent<TurnManager>();
        //data.enemyMap = GridManager.TileMap;

        SavePlayer();
        SaveEnemies();

        return data;
    }

    private void InitializeSave(){

        //TurnManager lTurn = data.turnData;
        //GridManager.TileMap = data.enemyMap;

        LoadPlayer();
        LoadEnemies();

        Event.EventManager.Notify(Event.Events.GameDeserialized, null);


        //Debug.Log(data.enemyMap.GetAll());
    }

    /*private void Test(){
        Assembly ass = Assembly.GetCallingAssembly();
        var f = ass.GetType("Asd").GetFields();
        foreach(FieldInfo fi in f){
            var a = fi.GetCustomAttributes(typeof(System.SerializableAttribute), false);
        }
    }*/

    ///
    /// Various functions for saving/loading different parts of the game
    ///

    private void SavePlayer(){
        Actor act = Player.GetComponent<Actor>();

        data.Player.Attack = act.attack._GetRawData();
        data.Player.Health = act.damagable._GetRawData();
        data.Player.EffectHolderEffects = IntermediateSerializers.EffectSerializer.Serialize(act.effects._GetRawData());

    }
    private void LoadPlayer(){
        Actor act = Player.GetComponent<Actor>();

        act.attack._SetRawData(data.Player.Attack);
        act.damagable._SetRawData(data.Player.Health);
        act.effects._SetRawData(IntermediateSerializers.EffectSerializer.Deserialize(data.Player.EffectHolderEffects));
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

    private ActorState SaveEnemy(GameObject enemy){
        //Ideas for saving enemies: Move all stuff in Start() to a new method called "OnSpawn()" which is called by EnemyManager
        //'s SpawnEnemy(), and give SpawnEnemy a parameter "DoInit" defaulting to true. If "DoInit" is not true, it does not call OnSpawn()
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

    private void LoadEnemy(ActorState enemyData){
        int x = enemyData.GridX;
        int y = enemyData.GridY;

        Actor enemyActor = GridManager.TileMap.GetAt(x, y);

        enemyActor.enemyTypeID = enemyData.EnemyTypeID;

        Debug.Log("Deserializing damagable");
        enemyActor.damagable._SetRawData(enemyData.Health);
        Debug.Log("Deserializing attack");
        enemyActor.attack._SetRawData(enemyData.Attack);
        Debug.Log("Deserializing countdown");
        enemyActor.countdown._SetRawData(enemyData.Countdown);

        Debug.Log("Deserializing effects");
        enemyActor.effects._SetRawData(IntermediateSerializers.EffectSerializer.Deserialize(enemyData.EffectHolderEffects));

        //TODO attempt for effects: use reflection to parse a data field into a dict, serialize the dict, and on deserialize:
        //create a new instance and again use reflection to populate with info from dict
    }
}