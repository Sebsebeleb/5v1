namespace BBG.Zone
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    enum ModifierType{ // Modifiers with the same modifier UseType are exclusive
        ZONE_LENGTH,
        ENEMY_DAMAGE,
        ENEMY_HEALTH,
        LOOT_AMOUNT,
        ENEMY_DIFFICULTY,
    }

    public abstract class ZoneModifier
    {
        internal ModifierType Typ;
        public string Description; // The description will be shown on the zone view to tell the player the effect.

        public int Difficulty; // Positive difficulty means this makes a zone more difficult. A negative makes it easier (or more rewarding).

        // applied after generation to modify the zone's stats.
        public virtual void ApplyStatModifications(Zone zone)
        {

        }

        // called when the zone is actually started, used for initalizing callbacks to zone effects for instance
        public virtual void ApplyCallbacks()
        {

        }

        ///
        /// Static methods
        ///

         // A cache of all the standard modifiers
        private static List<ZoneModifier> _builtinModifiers = new List<ZoneModifier>();
        private static bool instantiated = false;

        //
        // Public functions
        //

        public static List<ZoneModifier> GetAllModifiers()
        {
            if (instantiated == false)
            {
                instantiated = true;
                _loadModifiers();
            }

            return _builtinModifiers;
        }

        //
        // Private functions
        //

        // Loads all the modifiers that exist and store them in _builtinModifiers
        private static void _loadModifiers()
        {
            Assembly asm = Assembly.GetCallingAssembly();
            foreach (var m in asm.GetTypes())
            {
                if (m.IsSubclassOf(typeof(ZoneModifier)))
                {
                    var instance = Activator.CreateInstance(m);
                    var modifier = instance as ZoneModifier;
                    _builtinModifiers.Add(modifier);
                }
            }
        }
    }

}