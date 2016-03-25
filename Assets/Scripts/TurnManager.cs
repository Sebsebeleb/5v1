using Event;
using System;
using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public static int BossCounter = 60;
    public GameObject ZoneSelectionMenu;

    private GameObject _player;
    private SkillBehaviour _playerSkills;
    private EffectHolder _playerEffects;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _playerSkills = _player.GetComponent<SkillBehaviour>();
        _playerEffects = _player.GetComponent<EffectHolder>();
    }


    public void UseTurn()
    {
        StartCoroutine(processTurn());
    }

    private IEnumerator processTurn(){
        UpdatePlayer();

        while(AnimationManager.IsAnimating()){
            yield return 0;
        }

        //First we countdown the boss stuff
        if (BossCounter > 0){
            BossCounter--;
            if (BossCounter <= 0)
            {
                InitBoss();
            }
        }


        // TODO: Sort it properly (unless it already is)
        foreach (Actor enemy in GridManager.TileMap.GetAll())
        {
            while(AnimationManager.IsAnimating()){
                yield return 0;
            }

            if (!enemy) continue;

            enemy.countdown.Countdown();

        }

        EventManager.Notify(Events.OnTurn, null);

        // Check if we defeated the zone
        if (ZoneWon()){
            //TG



            //Non-TG
            /* 
            ZoneSelectionMenu.SetActive(true);
            ZoneSelectionMenu.GetComponent<Zone.ZoneSelectionScreen>().PopulateZones();
            */
        }
    }

    private void UpdatePlayer()
    {

        // Uugh this is kinda dirty...
        _playerSkills.CountDown();
        _playerEffects.OnTurn();

        //Update player's resources
        RegenResources();


    }


    /// <summary>
    /// Regenerates the characters resources based on regeneration rates
    /// </summary>
    private void RegenResources()
    {
        Actor.Player.CurrentMana += Actor.Player.ManaRegen.Value;
        Actor.Player.CurrentMana = Math.Min(Actor.Player.CurrentMana, Actor.Player.MaxMana.Value);

        if (Actor.Player.damagable.CurrentHealth <=  0)
        {
            
        }
    }


    private void InitBoss()
    {
        foreach(Actor enemy in GridManager.TileMap.GetAll()){
            EnemyManager.KillEnemy(enemy);
        }

        EnemyManager.SpawnBoss();
    }

    // The zone has been won if 1) the boss has been reached and 2) all enemies are dead
    private bool ZoneWon(){
        
        if (BossCounter > 0){
            return false;
        }

        foreach(var enemy in GridManager.TileMap.GetAll()){
            if (enemy.tag != "Corpse"){
                return false;
            }
        }

        return true;
    }
}
