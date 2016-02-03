using Event;
using System.Linq;

using Scripts.Audio;

using Tutorial;

using UnityEngine;
using UnityEngine.UI;

public class PlayerTargeting : MonoBehaviour
{

    public ToggleGroup SkillGroupRef;


    private GameObject _player;
    private ActorAttack _playerAttack;
    private Actor _playerActor;

    private TurnManager turn;

    private void Awake()
    {

        _player = GameObject.FindWithTag("Player");
        _playerAttack = _player.GetComponent<ActorAttack>();
        _playerActor = _player.GetComponent<Actor>();

        turn = GameObject.FindWithTag("GM").GetComponent<TurnManager>();
    }

    /// <summary>
    /// Takes the appropriate action when an enemy button is clicked, based on UI state.
    /// </summary>
    /// <param name="x">Actor's x position</param>
    /// <param name="y">Actor's y position</param>
    public void TargetGrid(int x, int y)
    {
        AudioManager.Trigger("Input_ButtonClickGeneric");
        // Check if nothing is hindering us from inputing stuff
        if (AnimationManager.IsAnimating()){
            return;
        }

        if (TutorialManager.IsInputPaused())
        {
            return;
        }

        Actor target = GridManager.TileMap.GetAt(x, y);

        // Did we actually do soemthing that should take a turn?
        bool usedAction = false;

        // Target skill
        if (SkillGroupRef.AnyTogglesOn())
        {
            Toggle active = SkillGroupRef.ActiveToggles().ToList()[0];
            BaseSkill skill = active.GetComponent<SkillUseButton>().AssociatedSkill;

            //TODO: Error log telling the reason
            if (skill.CanTargetGrid(x, y) && skill.CanUse(x, y) && !_playerActor.status.Silenced)
            {
                usedAction = true;
                skill.UseOnTargetGrid(x, y);
                SkillGroupRef.SetAllTogglesOff();
            }
        }
        else // Else, target regular attack
        {
            if (_playerAttack.CanAttack(x, y))
            {
                usedAction = true;
                _playerAttack.DoAttack(target);
                EventManager.Notify(Events.PlayerAttackCommand, target);

                AudioManager.Trigger("Player_AttackHit");
            }
        }

        if (usedAction)
        {
            turn.UseTurn();
        }
    }
}
