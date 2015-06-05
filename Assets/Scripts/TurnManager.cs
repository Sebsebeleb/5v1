using Event;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public int BossCounter = 80;

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
        UpdatePlayer();
        //First we countdown the boss stuff
        BossCounter--;
        if (BossCounter <= 0)
        {
            InitBoss();
        }

        // TODO: Sort it properly (unless it already is)
        foreach (Actor enemy in GridManager.TileMap.GetAll())
        {
            if (!enemy) continue;

            enemy.countdown.Countdown();
        }

        EventManager.Notify(Events.OnTurn, null);
    }

    private void UpdatePlayer()
    {
        // Uugh this is kinda dirty...
        _playerSkills.CountDown();
        _playerEffects.OnTurn();

        
    }

    private void InitBoss()
    {

    }
}
