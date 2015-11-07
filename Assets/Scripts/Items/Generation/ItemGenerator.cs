using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine.Assertions;

namespace Generation
{
    using System;
    using System.Xml;

    using Assets.Scripts.Items;

    using UnityEngine;

    using Random = System.Random;

    public enum GeneratedItemType
    {
        Equipment,
        Consumable,
        Both,
    }

    public static class ItemGenerator
    {

        private static Random random = new Random();

        /// <summary>
        /// Generates a new completely random item
        /// </summary>
        /// <param name="type">The type of item to generate</param>
        /// <returns>The new finalized item</returns>
        public static BaseItem GenerateItem(GeneratedItemType type)
        {
            BaseItem generatedItem;

            Debug.Log("---- Generating base item ----");
            generatedItem = MakeBaseItem(type);

            Debug.Log("-- Rolling Rarity --");
            generatedItem.Rarity = RollRarity();
            Debug.Log(generatedItem.Rarity);

            Debug.Log("-- Adding modifiers-- ");
            AddModifiers(generatedItem);

            Debug.Log("---- Finished creating item ----");
            return generatedItem;
        }

        /// <summary>
        /// Adds one random modifier to the item, based on the item type and rarity
        /// TODO: Add ways to affect what is selected like based on tags or whatever
        /// </summary>
        /// <param name="generatedItem">The item to generate modifiers for</param>
        private static void AddModifiers(BaseItem generatedItem)
        {
            ItemModifierLoader.ItemModifierEntry entry = new ItemModifierLoader.ItemModifierEntry();
            bool found = false;

            foreach (ItemModifierLoader.ItemModifierEntry itemModifier in ItemModifierLoader.GetItemModifiers())
            {
                Debug.Log("Checking: " + itemModifier.Name);
                Debug.Log("Looking for rarity: " + generatedItem.Rarity);
                Debug.Log("Mod contains rarities: ");
                Debug.Log(itemModifier.Rarities.Length);
                foreach (var itemRarity in itemModifier.Rarities)
                {
                    Debug.Log(itemRarity.ToString());
                }

                // Make sure the modifier has a suitable rartiy effect
                if (!itemModifier.Rarities.Contains(generatedItem.Rarity))
                {
                    continue;
                }
                Debug.Log("Found a modifier");
                entry = itemModifier;
                found = true;
                break;
            }

            if (!found)
            {
                Debug.Log("No modifiers were found");
                return;
            }

            Debug.Log("Adding modifiers");
            foreach (XmlElement itemEffectData in entry.ItemEffects)
            {
                ItemEffect eff = ItemEffect.LoadFromXML(itemEffectData, generatedItem.Rarity);       
                generatedItem.AddEffect(eff);
            }
        }

        /// <summary>
        /// Roll the rarity for the item
        /// </summary>
        /// <returns>The rarity for the new item</returns>
        private static BaseItem.ItemRarity RollRarity()
        {
            float roll = random.Next(100);

            if (roll > 90)
            {
                return BaseItem.ItemRarity.Legendary;
            }
            if (roll > 70)
            {
                return BaseItem.ItemRarity.Rare;
            }
            if (roll > 40)
            {
                return BaseItem.ItemRarity.Uncommon;
            }

            return BaseItem.ItemRarity.Common;
        }

        /// <summary>
        /// Initialize the item based on types that are possible to get
        /// </summary>
        /// <param name="type">What types should be possible</param>
        /// <returns>The initialized base item</returns>
        private static BaseItem MakeBaseItem(GeneratedItemType type)
        {
            BaseItem generatedItem;
            switch (type)
            {
                case GeneratedItemType.Both:
                    if (random.Next(1) > 0.5f)
                    {
                        generatedItem = new ConsumableItem();
                    }
                    else
                    {
                        generatedItem = new EquippableItem();
                    }
                    break;
                case GeneratedItemType.Consumable:
                    generatedItem = new ConsumableItem();
                    break;
                case GeneratedItemType.Equipment:
                    generatedItem = new EquippableItem();
                    break;
                default:
                    generatedItem = new EquippableItem();
                    Debug.LogException(new Exception("Missing item type?"));
                    break;
            }
            return generatedItem;
        }
    }
    
}
