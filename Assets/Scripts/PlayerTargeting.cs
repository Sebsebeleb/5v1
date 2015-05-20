using System;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{

    public Skill SelectedSkill;
    private GameObject _player;
    private AttackBehaviour _playerAttack;

    private TurnManager turn;

    private void Awake()
    {

        _player = GameObject.FindWithTag("Player");
        _playerAttack = _player.GetComponent<AttackBehaviour>();

        turn = GameObject.FindWithTag("GM").GetComponent<TurnManager>();
    }

    public void TargetGrid(int x, int y)
    {
        Enemy target = GridManager.TileMap.GetAt(x, y);
        if (SelectedSkill != null)
        {
            throw new NotImplementedException();
        }
        else
        {
            if (_playerAttack.CanAttack(x, y))
            {
                _playerAttack.DoAttack(target);
                turn.UseTurn();
            }
        }
    }
}
