using UnityEngine;

namespace View
{

    public class GridButtonBehaviour : MonoBehaviour
    {

        public int x;
        public int y;

        private GameObject _player;
        private PlayerTargeting _playerTargeting;


        void Awake()
        {
            _player = GameObject.FindWithTag("Player");
            _playerTargeting = _player.GetComponent<PlayerTargeting>();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        public void Clicked()
        {
            _playerTargeting.TargetGrid(x, y);
        }
    }

}
