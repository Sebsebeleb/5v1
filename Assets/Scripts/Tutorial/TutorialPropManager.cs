namespace Tutorial
{
    using Assets.Scripts.View.CommonUI;

    using Event;

    using Map;

    using UnityEngine;

    /// <summary>
    /// Makes sure tutorial props show up at correct events
    /// </summary>
    public class TutorialPropManager : MonoBehaviour
    {

        public Transform CountdownTutorialTransform;
        public Transform InspectionTutorialTransform;

        [SerializeField]
        private Transform BossTutorialTransform;

        private void Start()
        {
            if (TutorialManager.tutorialDisabled)
            {
                return;
            }

            if (TutorialManager.ShouldDisplayTutorialProp("CountdownExplanation", false))
            {
                EventManager.Register(Events.ActorCountedDown, (ActorParameters) this.TutorialCountdown);
            }

            if (TutorialManager.ShouldDisplayTutorialProp("Inspection", false))
            {
                EventManager.Register(Events.PostEnemySpawned, (ActorParameters) this.TutorialInspection);
            }

            if (TutorialManager.ShouldDisplayTutorialProp("boss", false))
            {
                EventManager.Register(Events.OnTurn, (OnTurn)this.TutorialBoss);
            }
        }

        private void TutorialBoss()
        {
            if (TurnManager.BossCounter == 0)
            {
                
                TutorialManager.DisableTutorialProp("boss");
                this.BossTutorialTransform.gameObject.SetActive(true);
                
            }
        }

        /// <summary>
        /// Tutorial that teaches the game about inspecting
        /// </summary>
        /// <param name="who"></param>
        private void TutorialInspection(Actor who)
        {
            // TODO: FIXME : shouldnt use gameobject name

            if (who.name != "Skeleton Warrior(Clone)" && who.name != "Corpse(Clone)")
            {
                TutorialManager.DisableTutorialProp("Inspection");

                this.InspectionTutorialTransform.gameObject.SetActive(true);
                EventManager.UnRegister(Events.PostEnemySpawned, (ActorParameters) this.TutorialInspection);

                //
                // Update position and highlight the enemy
                //

                // Figure out the UI button that this enemy corresponds to
                GridPosition pos = new GridPosition(who.x, who.y);
                EnemyDisplay display = EnemyDisplay.Displays[pos];
                RectTransform enemyRect = display.GetComponent<RectTransform>();

                Vector2 offset;
                if (who.y == 0)
                {
                    offset = new Vector2(0, -255);
                }
                else
                {
                    offset = new Vector2(0, 255);
                }

                Debug.Log(enemyRect);
                // Move and highlight
                this.InspectionTutorialTransform.GetComponent<MoveToRectTransform>().Move(enemyRect, offset);
                this.InspectionTutorialTransform.GetComponent<Highlighter>().Highlight(enemyRect);
            }
        }

        /// <summary>
        /// Tutorial that explains countdown
        /// </summary>
        /// <param name="who"></param>
        private void TutorialCountdown(Actor who)
        {
            if (who.countdown.CurrentCountdown == 2)
            {
                TutorialManager.DisableTutorialProp("CountdownExplanation");

                EventManager.UnRegister(Events.ActorCountedDown, (ActorParameters) this.TutorialCountdown);

                //Find the UI button
                GridPosition pos = new GridPosition(who.x, who.y);
                EnemyDisplay display = EnemyDisplay.Displays[pos];
                RectTransform enemyRect = display.GetComponent<RectTransform>();

                // Move and highlight
                Debug.Log(enemyRect);

                Vector2 offset;
                if (who.y == 0)
                {
                    offset = new Vector2(0, -255);
                }
                else
                {
                    offset = new Vector2(0, 255);
                }

                this.CountdownTutorialTransform.gameObject.SetActive(true);
                this.CountdownTutorialTransform.GetComponent<MoveToRectTransform>().Move(enemyRect, offset);
                this.CountdownTutorialTransform.GetComponent<Highlighter>().Highlight(enemyRect);

            }
        }
    }
}