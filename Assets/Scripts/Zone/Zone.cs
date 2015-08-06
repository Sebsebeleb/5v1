using System;
using System.Collections.Generic;

namespace Zone
{
    // A zone is a collection of data, including spawn list for enemies, zone modifiers, and other zone specific stuff.
    // At any point of the game during the main screen, a single zone is active. After finishing a zone, a player has to
    // choose a new zone to enter.
    [Serializable]
    public class Zone
    {

        private static Zone _currentZone;

        // The currently active zone
        public static Zone current
        {
            get { return _currentZone; }
        }

        public List<ZoneModifier> modifiers = new List<ZoneModifier>();

        public List<EnemySpawnList> spawnLists;

        public int ZoneLength; // The bosscounter will start at this number
        public string Denizens;

        public static void SetZone(Zone nextZone)
        {
            _currentZone = nextZone;

            //Initalize the zone effects
            InitalizeZone();
        }

        private static void InitalizeZone(){
            
        }
    }

}
