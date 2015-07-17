using UnityEngine;

public static class Utils
{
    private static Actor _player = null;
    private static Actor Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindWithTag("Player").GetComponent<Actor>();
            }
            return _player;
        }
    }
    public static int ActorToID(Actor act)
    {
        if (act.gameObject.tag == "Player")
        {
            return 0;
        }
        return act.y * 3 + act.x + 1;
    }

    public static Actor IDToActor(int i)
    {
        if (i == 0)
        {
            return Player;
        }
		return GridManager.TileMap.GetFromIndex(i-1);
    }
}