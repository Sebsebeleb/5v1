using UnityEngine;

namespace BBG
{
    public static class Utils
    {
        private static Actor.Actor _player = null;
        private static Actor.Actor Player
        {
            get
            {
                if (_player == null)
                {
                    _player = GameObject.FindWithTag("Player").GetComponent<Actor.Actor>();
                }
                return _player;
            }
        }

        // These are used to get a reference to map position/player of enemies. The mapping is as following for enemies (0 is player) on the map
        // _______
        // |1|2|3|
        // |4|5|6|
        //
        public static int ActorToID(Actor.Actor act)
        {
            if (act.gameObject.tag == "Player")
            {
                return 0;
            }
            return act.y * 3 + act.x + 1;
        }
        public static Actor.Actor IDToActor(int i)
        {
            if (i == 0)
            {
                return Player;
            }
            return GridManager.TileMap.GetFromIndex(i-1);
        }
    }
}