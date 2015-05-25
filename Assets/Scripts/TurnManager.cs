using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public int BossCounter = 80;

    private GameObject player;
    private SkillBehaviour playerSkills;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerSkills = player.GetComponent<SkillBehaviour>();
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
    }

    private void UpdatePlayer()
    {
        playerSkills.CountDown();
    }

    private void InitBoss()
    {

    }
}
