using RTT = BBG.TextUtilities;

namespace BBG.Zone.View
{
    using System;
    using System.Linq;

    using UnityEngine;
    using UnityEngine.UI;

    public class ZoneEntryBehaviour : MonoBehaviour
    {

        public Text ZoneName;
        public Image ZoneImage;
        public Text Description;

        public Zone zone;

        // Set the zone this represents and update view based on it
        public void SetZone(Zone _zone)
        {
            // Doesn't really belong here, but it has to be set after the parent is set.
            this.GetComponent<Toggle>().group = this.GetComponentInParent<ToggleGroup>();

            this.zone = _zone;
            this.InitDescription(); // creates the string for the description and updates the Text component
        }

        // Builds the final string to use on the text component and applies it
        private void InitDescription()
        {

            string finalString = String.Format(
                @"Enemies: {0}
				Length: {1}

				",
				RTT.Bold(RTT.FontColor("#FFFF00", this.zone.Denizens)),
                RTT.FontColor("#FF00FF", this.zone.ZoneLength.ToString())
				);

            // Add modifier descriptions

            // TO maybe DO: Sort it by 0 to last, then decending order (to get neutral zones after positive/negative ones
            // Current: sort it decending order
            var sortedModifiers = this.zone.modifiers.OrderBy((mod) => mod.Difficulty);

            foreach (ZoneModifier mod in sortedModifiers)
            {
                string color;

                if (mod.Difficulty == 0)
                {
                    color = "#FFFFFF";
                }
                else if (mod.Difficulty > 0)
                {
                    color = "#FF0000";
                }
                else
                {
                    color = "#00FF00";
                }

                finalString = String.Format("{0} {1}\n", finalString, TextUtilities.FontColor(color, mod.Description));
            }

            this.Description.text = finalString;
        }
    }
}