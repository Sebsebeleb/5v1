namespace BBG.Zone.View
{
    using System.Collections.Generic;
    using System.Linq;

    using UnityEngine;
    using UnityEngine.UI;

    public class ZoneSelectionScreen : MonoBehaviour
    {
        public const int NumZoneChoices = 4;

        public GameObject SelectionScreenRoot;
        public Transform ZoneList;

        public ToggleGroup EntryGroup;

        public GameObject ZoneEntryPrefab;

        public void Awake()
        {

        }

        //This function basically sets the zone and then restarts the game.
        public void ChooseSelectedZone()
        {


            // Setup the game
            Zone nextZone = this.EntryGroup.ActiveToggles().ToList()[0].GetComponent<ZoneEntryBehaviour>().zone;

            // Disable the screen
            this.gameObject.SetActive(false);

            TurnManager.BossCounter = nextZone.ZoneLength;

            Zone.SetZone(nextZone);

        }

        public void PopulateZones()
        {
            foreach (Transform child in this.ZoneList)
            {
                Destroy(child.gameObject);
            }

            List<Zone> zones = this.MakeZones();

            foreach (Zone zone in zones)
            {
                GameObject child = Instantiate(this.ZoneEntryPrefab) as GameObject;

                child.transform.SetParent(this.ZoneList, false);

                ZoneEntryBehaviour entryBehaviour = child.GetComponent<ZoneEntryBehaviour>();

                entryBehaviour.SetZone(zone);
            }
        }

        private List<Zone> MakeZones()
        {
            List<Zone> result = new List<Zone>();
            for (int i = 0; i < NumZoneChoices; i++)
            {
                result.Add(ZoneGenerator.Generate(20));
            }

            return result;
        }
    }
}