using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;
using UnityEngine.Assertions;

// Middle-man class for loading various game assets
namespace BBG.ResourceManagement
{
    using BBG.Actor;
    using BBG.BaseClasses;

    public static class GameResources
    {

        // The folder where all the enemy types are stored
        private const string SearchPath = "Enemies";

        private const string ItemModifiersPath = "ItemModifiers";

        private static Assembly assembly = Assembly.GetCallingAssembly();


        // Cache for GetEnemyByID. Possible TODO: Have this filled out at build-time by the editor
        private static Dictionary<int, GameObject> _enemyByIDCache = new Dictionary<int, GameObject>();
        public static GameObject GetEnemyByID(int id)
        {
            if (_enemyByIDCache.ContainsKey(id))
            {
                return _enemyByIDCache[id];
            }

            // Try to find the enemytype using Resources.Load
            GameObject enemy = _findEnemy(id);

            if (enemy == null)
            {
                return null;
            }

            // Store it in the cache
            _enemyByIDCache[id] = enemy;

            return enemy;
        }

        /// <summary>
        /// Temp function for getting the item modifications
        /// </summary>
        /// <returns></returns>
        public static TextAsset[] GetItemModifiers()
        {
            return Resources.LoadAll<TextAsset>(ItemModifiersPath);
        }

        // Looks through all gameobjects untill it finds one that has the Actor component and the correct ID
        private static GameObject _findEnemy(int id)
        {
            foreach (GameObject go in Resources.LoadAll<GameObject>(SearchPath))
            {
                Actor act = go.GetComponent<Actor>();

                // If it doesn't have an actor component, it isnt an enemy UseType
                if (act == null)
                {
                    continue;
                }

                if (act.enemyTypeID == id)
                {
                    return go;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds an effect by name and returns a new instance
        /// </summary>
        /// <param name="name">Name of effect to find</param>
        /// <returns></returns>
        public static Effect LoadEffect(string name)
        {

            // Look inside both these assemblies
            var typ = (assembly.GetType("BBG.Data.Effects." + name) ?? assembly.GetType("BBG.Data.Traits." + name))
                      ?? assembly.GetType("BBG.Data.Effects.Specializations." + name);
            Assert.IsNotNull(typ, "Invalid effect load attempt: " + name);
            var effInstance = Activator.CreateInstance(typ);
            var test = effInstance as Effect;

            return test;

        }

        public static Sprite LoadSprite(string name)
        {
            return Resources.Load<Sprite>(name);
        }
    }
}