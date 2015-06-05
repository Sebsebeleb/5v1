using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTargeting : MonoBehaviour
{

    public ToggleGroup SkillGroupRef;


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
        Actor target = GridManager.TileMap.GetAt(x, y);

        // Did we actually do soemthing that should take a turn?
        bool usedAction = false;

        // Target skill
        if (SkillGroupRef.AnyTogglesOn())
        {
            Toggle active = SkillGroupRef.ActiveToggles().ToList()[0];
            BaseSkill skill = active.GetComponent<SkillUseButton>().AssociatedSkill;

            if (skill.CanTargetGrid(x, y) && skill.CanUse(x, y))
            {
                usedAction = true;
                skill.UseOnTargetGrid(x, y);
            }
        }
        else // Else, target regular attack
        {
            if (_playerAttack.CanAttack(x, y))
            {
                usedAction = true;
                _playerAttack.DoAttack(target);
            }
        }

        if (usedAction)
        {
            turn.UseTurn();
        }
    }
}
