using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Linq;

namespace Zone
{

    enum ZoneTypes
    {
        CRYPT,
        FOREST,
        CRYPT_FOREST,
    }

    // Functional methods for generating random, difficulty aware zones
    public static class ZoneGenerator
    {
        // If a generated zone's difficulty is more than this different than the target difficulty, it isnt valid.
        private const int MAX_DIFFICULT_DEVIATION = 15;

        public static Zone Generate(int difficulty)
        {
            Zone zone = ChooseZoneType();

            List<ZoneModifier> modifiers = GenerateModifiers(zone, difficulty);

            zone.modifiers = modifiers;

            foreach (ZoneModifier mod in zone.modifiers)
            {
                mod.ApplyStatModifications(zone);
            }

            return zone;
        }

        // Chooses between the base zone types (which decides which denizens will be there etc.)
        private static Zone ChooseZoneType()
        {
            // Silly but works
            int i = Random.Range(1, 7);

            Zone choice;

            if (i <= 3)
            {
                choice = BuildZone(ZoneTypes.CRYPT);
            }
            else if (i <= 5)
            {
                choice = BuildZone(ZoneTypes.FOREST);
            }
            else
            {
                choice = BuildZone(ZoneTypes.CRYPT_FOREST);
            }

            return choice;
        }

        // targetDifficulty is the total difficulty we aim to match
        private static List<ZoneModifier> GenerateModifiers(Zone zone, int targetDifficulty)
        {
            List<ZoneModifier> result = new List<ZoneModifier>();

            int tries = 1000;
            bool success = false;

            // Attempt 1000 times to generate a valid zone
            while (tries > 0 && success == false)
            {
                result.Clear();


                int numMods = Random.Range(1, 5);

                int currentMods = 0;
                int modTries = 100;

                // Generate mods untill one that is valid is found. Valid means it isnt exclusive with other exisitng mods
                while (currentMods < numMods && modTries > 0)
                {
                    var mod = GenerateModifier();

                    if (result.TrueForAll((x) => x.Typ != mod.Typ))
                    {
                        result.Add(mod);
                        currentMods++;
                    }
                    modTries--;
                }

                //Check if the difficulty is good.
                int finalDifficulty = result.Sum((mod) => mod.Difficulty);
                if (Math.Abs(finalDifficulty - targetDifficulty) < MAX_DIFFICULT_DEVIATION)
                {
                    success = true;
                }

                //TODO: Check if the zone is identical to already generated zones. Thats not a choice!
            }

            return result;
        }

        // Chooses a random modifiers among all existing ones and returns it
        private static ZoneModifier GenerateModifier()
        {
            var definedModifiers = ZoneModifier.GetAllModifiers();

            int choice = Random.Range(0, definedModifiers.Count);

            return definedModifiers[choice];
        }

        // Defines the base stats for the different zone types
        private static Zone BuildZone(ZoneTypes type)
        {
            Zone zone = new Zone();

            switch (type)
            {
                case ZoneTypes.CRYPT:
                    zone.Denizens = "Crypt";
                    zone.ZoneLength = 60;
                    break;
                case ZoneTypes.CRYPT_FOREST:
                    zone.Denizens = "Crypt and Forest";
                    zone.ZoneLength = 65;
                    break;
                case ZoneTypes.FOREST:
                    zone.Denizens = "Forest";
                    zone.ZoneLength = 70;
                    break;
            }

            return zone;
        }
    }

}