using DefaultNamespace;

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Keeps track of many important game variables
/// </summary>
public static class GameStateManager
{
    public static GameFunctions funcs;

    static GameStateManager()
    {
        ResetGame();
    }

    private static bool tgHasDied = false;

    /// <summary>
    /// Has the player lost at the TG promotional event yet? if the player is able to complete the game without this happening, they may get a nice prize
    /// </summary>
    public static bool Tg_HasDied
    {
        get
        {
            return tgHasDied;
        }
        set
        {
            tgHasDied = value;
        }
    }

    // Called when the game is being "restarted"
    public static void ResetGame()
    {
        tgHasDied = false;
        TurnManager.BossCounter = 60;

    }



    public static void RestartGame(bool restartTutorial)
    {
        PlayerPrefs.DeleteAll();
        ResetGame();
        SceneManager.LoadScene(0);
    }

    public static void OnDeath()
    {
        funcs.ShowFirstDeath();
    }

    public static void WinCandy()
    {
        funcs.Win();
    }

}