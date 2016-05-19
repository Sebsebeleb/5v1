using System.Linq;

using UnityEngine;

namespace BBG
{
    using BBG.Actor;
    using BBG.Audio;
    using BBG.BaseClasses;
    using BBG.Tutorial;
    using BBG.View.Actions;

    using UnityEngine.UI;

    public class PlayerTargeting : MonoBehaviour
    {

        public ToggleGroup SkillGroupRef;


        private GameObject _player;
        private ActorAttack _playerAttack;
        private Actor.Actor _playerActor;

        private TurnManager turn;

        private void Awake()
        {

            this._player = GameObject.FindWithTag("Player");
            this._playerAttack = this._player.GetComponent<ActorAttack>();
            this._playerActor = this._player.GetComponent<Actor.Actor>();

            this.turn = GameObject.FindWithTag("GM").GetComponent<TurnManager>();
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

            Actor.Actor target = GridManager.TileMap.GetAt(x, y);

            // Did we actually do soemthing that should take a turn?
            bool usedAction = false;

            // Target skill
            if (this.SkillGroupRef.AnyTogglesOn())
            {
                Toggle active = this.SkillGroupRef.ActiveToggles().ToList()[0];
                BaseSkill skill = active.GetComponent<SkillUseButton>().AssociatedSkill;

                //TODO: Error log telling the reason
                if (skill.CanTargetGrid(x, y) && skill.CanUse(x, y) && !this._playerActor.status.Silenced)
                {
                    usedAction = true;
                    skill.UseOnTargetGrid(x, y);
                    this.SkillGroupRef.SetAllTogglesOff();
                }
            }
            else // Else, target regular attack
            {
                if (this._playerAttack.CanAttack(x, y))
                {
                    usedAction = true;
                    this._playerAttack.DoAttack(target);
                    EventManager.Notify(Events.PlayerAttackCommand, target);

                    AudioManager.Trigger("Player_AttackHit");
                }
            }

            if (usedAction)
            {
                this.turn.UseTurn();
            }
        }
    }
}
