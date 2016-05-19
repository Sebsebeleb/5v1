namespace BBG.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    using BBG.ResourceManagement;

    using UnityEngine;

    /// <summary>
    /// Manages loading and storing all the item modifiers that exist
    /// </summary>
    public static class ItemModifierLoader
    {

        public struct ItemModifierEntry
        {
            public string Id; // TODO: Rename "Name" in xml documents to ID

            public string DisplayName; // Currently the prefix.

            public BaseItem.ItemRarity[] Rarities; // This modifier can be added to these rarities

            public XmlNodeList ItemEffects; // The actual effects that should be applied
        }

        /// Modifiers for the different equipment types
        private static Dictionary<string, ItemModifierEntry> weaponModifiers = new Dictionary<string, ItemModifierEntry>();

        private static Dictionary<string, ItemModifierEntry> miscModifiers = new Dictionary<string, ItemModifierEntry>();

        private static Dictionary<string, ItemModifierEntry> consumableModifiers = new Dictionary<string, ItemModifierEntry>();

        private static bool loaded = false;

        /// <summary>
        /// Returns entries for all the loaded item modifiers for the selected item type
        /// </summary>
        /// <returns>A list of all the item modifiers that have been loaded</returns>
        public static ItemModifierEntry[] GetItemModifiers(BaseItem.ItemType modifierType)
        {
            if (!loaded)
            {
                LoadAllModifiers();
                loaded = true;
            }

            switch (modifierType)
            {
                    case BaseItem.ItemType.Weapon:
                        return weaponModifiers.Values.ToArray();
                    case BaseItem.ItemType.Misc:
                        return miscModifiers.Values.ToArray();
                    case BaseItem.ItemType.Consumable:
                        return consumableModifiers.Values.ToArray();
                default:
                    throw new NotImplementedException("Item modifiers not implemented for the given modifiertype");
            }
        }

        private static void LoadAllModifiers()
        {
            TextAsset[] assets = GameResources.GetItemModifiers();

            Debug.Log("Found " + assets.Count() + " assets for modifiers");

            foreach (TextAsset asset in assets)
            {
                // Open the document
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(asset.text);

                // Figure out what item type it is for
                BaseItem.ItemType type;

                switch ((doc.LastChild.FirstChild.InnerText))
                {
                    case "Weapon":
                        type = BaseItem.ItemType.Weapon;
                        break;

                    case "Misc":
                        type = BaseItem.ItemType.Misc;
                        break;

                    case "Consumable":
                        type = BaseItem.ItemType.Consumable;
                        break;

                    default:
                        throw new Exception("Error, non-existing item type in data: " + doc.FirstChild.NextSibling.InnerText + " (" + asset +")");
                }

                // Store the loaded modifiers in appropriate list
                ItemModifierEntry[] loaded = loadItemModifierEntries(doc);

                Dictionary<string, ItemModifierEntry> storage;

                switch (type)
                {
                    case BaseItem.ItemType.Weapon:
                        storage = weaponModifiers;
                        break;
                    case BaseItem.ItemType.Misc:
                        storage = miscModifiers;
                        break;
                    case BaseItem.ItemType.Consumable:
                        storage = consumableModifiers;
                        break;
                    default:
                        throw new Exception("wtf");
                }


                foreach (var mod in loaded)
                {
                    Debug.Log("Found mod: " + mod.Id);
                    storage[mod.Id] = mod;
                }
            }
        }

        /// <summary>
        /// Loads every single item modifier in a data asset
        /// </summary>
        /// <param name="text">The XML to load from</param>
        /// <returns>A list of ItemModifierEntry of all the item modifiers found</returns>
        private static ItemModifierEntry[] loadItemModifierEntries(XmlDocument doc)
        {
            Debug.Log("- Looking for mods in an asset -");

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

                var child = element.FirstChild;

                //First child should be name
                entry.Id = child.InnerText;

                //Next node is prefix (currently)
                child = child.NextSibling;
                entry.DisplayName = child.InnerText;

                entry.Rarities = rarities.ToArray();
                entry.ItemEffects = element.GetElementsByTagName("effect");

                entires.Add(entry);
            }

            return entires.ToArray();
        }
    }
}
