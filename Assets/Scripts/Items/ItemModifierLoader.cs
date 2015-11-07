using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Items
{
    using System.Xml;

    using JetBrains.Annotations;

    using UnityEngine;


    /// <summary>
    /// Manages loading and storing all the item modifiers that exist
    /// </summary>
    public static class ItemModifierLoader
    {
        public struct ItemModifierEntry
        {
            public string Name;

            public BaseItem.ItemRarity[] Rarities; // This modifier can be added to these rarities

            public XmlNodeList ItemEffects; // The actual effects that should be applied
        }

        private static Dictionary<string, ItemModifierEntry> modifiers = new Dictionary<string, ItemModifierEntry>();

        private static bool loaded = false;

        /// <summary>
        /// Returns entries for all the loaded item modifiers
        /// </summary>
        /// <returns>A list of all the item modifiers that have been loaded</returns>
        public static ItemModifierEntry[] GetItemModifiers()
        {
            if (!loaded)
            {
                LoadAllModifiers();
                loaded = true;
            }

            return modifiers.Values.ToArray();

        }

        private static void LoadAllModifiers()
        {
            TextAsset[] assets = GameResources.GetItemModifiers();

            Debug.Log("Found " + assets.Count() + " assets for modifiers");

            foreach (TextAsset asset in assets)
            {
                ItemModifierEntry[] loaded = loadItemModifierEntries(asset.text);

                foreach (var mod in loaded)
                {
                    Debug.Log("Found mod: " + mod.Name);
                    modifiers[mod.Name] = mod;
                }
            }
        }

        /// <summary>
        /// Loads every single item modifier in a data asset
        /// </summary>
        /// <param name="text">The XML to load from</param>
        /// <returns>A list of ItemModifierEntry of all the item modifiers found</returns>
        private static ItemModifierEntry[] loadItemModifierEntries(string text)
        {
            Debug.Log("- Looking for mods in an asset -");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(text);

            var elements = doc.GetElementsByTagName("item");


            List<ItemModifierEntry> entires = new List<ItemModifierEntry>();

            foreach (XmlElement element in elements)
            {

                // Figure out what rarities this modifier can be applied to
                List<BaseItem.ItemRarity> rarities = new List<BaseItem.ItemRarity>();

                if (element.HasAttribute("common") && (element.Attributes["common"].Value == "true"))
                {
                    rarities.Add(BaseItem.ItemRarity.Common);
                }
                if (element.HasAttribute("uncommon") && (element.Attributes["uncommon"].Value == "true"))
                {
                    rarities.Add(BaseItem.ItemRarity.Uncommon);
                }
                if (element.HasAttribute("rare") && (element.Attributes["rare"].Value == "true"))
                {
                    rarities.Add(BaseItem.ItemRarity.Rare);
                }
                if (element.HasAttribute("legendary") && (element.Attributes["legendary"].Value == "true"))
                {
                    rarities.Add(BaseItem.ItemRarity.Legendary);
                }

                ItemModifierEntry entry = new ItemModifierEntry();

                entry.Name = element.FirstChild.InnerText;
                entry.Rarities = rarities.ToArray();
                entry.ItemEffects = element.GetElementsByTagName("effect");

                entires.Add(entry);
            }

            return entires.ToArray();
        }
    }
}
