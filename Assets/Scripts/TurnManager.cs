using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public int BossCounter = 80;


    public void UseTurn()
    {
        //First we countdown the boss stuff
        BossCounter--;
        if (BossCounter <= 0)
        {
            InitBoss();
        }
        else
        {
            // TODO: Sort it properly (unless it already is)
            foreach (Actor enemy in GridManager.TileMap.GetAll())
            {
                if (!enemy) continue;

                enemy.countdown.Countdown();
            }
        }
    }

    private void InitBoss()
    {

    }
}
