using UnityEditor;
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
            foreach (Enemy enemy in GridManager.TileMap.GetAll()) {

                if (!enemy) continue;

                enemy.countdown.Countdown();
            }
        }
    }

    private void InitBoss()
    {

    }
}
