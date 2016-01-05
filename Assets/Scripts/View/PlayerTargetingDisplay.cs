namespace Scripts.View
{
    using Map;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Updates the UI to show targeting related information based on what the player has selected/could want to do
    /// </summary>
    public class PlayerTargetingDisplay : MonoBehaviour
    {
        /// <summary>
        /// The toggle group for the buttons for skills, used to figure out if any (and what) skils are selected for usage
        /// </summary>
        [SerializeField]
        private ToggleGroup skillToggles;

        // The skill we are currently displaying targeting related information for
        private BaseSkill displayForSkill;

        private bool wasOn;

        private GridPosition oldPosition;

        private GridPosition newPosition;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">see GridMap.GetFromIndex()</param>
        public void RegisterMouseEnter(int index)
        {
            Actor act = GridManager.TileMap.GetFromIndex(index);

            int x = act.x;
            int y = act.y;

            this.newPosition = new GridPosition(x, y);
        }

        public void RegisterMouseExit(int index)
        {
            this.newPosition = null;
        }

        private void Update()
        {
            // Check if dirty
            if (this.wasOn != this.skillToggles.AnyTogglesOn() || this.newPosition != this.oldPosition)
            {

                this.wasOn = this.skillToggles.AnyTogglesOn();
                this.oldPosition = this.newPosition;

                // Clear old info
                foreach (GridPosition position in GridManager.TileMap.GetAllPositions())
                {
                    SetDisplayAt(position, false);
                    SetAffectedAt(position, false);
                }


                this.UpdateDisplay();

            }
        }

        private void UpdateDisplay()
        {
            // Nothing to display
            if (!this.skillToggles.AnyTogglesOn())
            {
                this.displayForSkill = null;

                return;
            }

            // Something to display!
            Toggle activeToggle = this.skillToggles.ActiveToggles().ToArray()[0];


            SkillUseButton activeButton = activeToggle.GetComponent<SkillUseButton>();

            BaseSkill skill = activeButton.AssociatedSkill;
            this.displayForSkill = skill;

            List<GridPosition> targets = skill.GetValidTargets();

            foreach (GridPosition target in targets)
            {
                SetDisplayAt(target, true);
            }

            // Update "affected" display

            if (this.newPosition != null)
            {
                var affected = skill.GetAffectedTargets(this.newPosition);


                foreach (GridPosition pos in affected)
                {
                    SetAffectedAt(pos, true);
                }
            }
            else
            {
                foreach (GridPosition pos in GridManager.TileMap.GetAllPositions())
                {
                    SetAffectedAt(pos, false);
                }
            }
        }

        // Enables or disables the targeting display for the given position
        private void SetDisplayAt(GridPosition pos, bool on)
        {
            EnemyDisplay.Displays[pos].Targeting.gameObject.SetActive(on);
        }

        // Enables or disables the "affected by" targeting display for the given position
        private void SetAffectedAt(GridPosition pos, bool on)
        {
            
            EnemyDisplay.Displays[pos].TargetingAffected.gameObject.SetActive(on);
        }
    }
}