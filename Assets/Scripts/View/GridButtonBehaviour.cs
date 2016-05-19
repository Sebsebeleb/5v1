namespace BBG.View
{
    using UnityEngine;

    public class GridButtonBehaviour : MonoBehaviour
    {

        public int x;
        public int y;

        private GameObject _player;
        private PlayerTargeting _playerTargeting;


        void Awake()
        {
            this._player = GameObject.FindWithTag("Player");
            this._playerTargeting = this._player.GetComponent<PlayerTargeting>();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        public void Clicked()
        {
            this._playerTargeting.TargetGrid(this.x, this.y);
        }
    }

}
