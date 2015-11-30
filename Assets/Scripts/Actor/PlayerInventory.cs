using System.Collections;
using System.Collections.Generic;

using Generation;

using UnityEngine;

/// <summary>
/// The player inventory.
/// </summary>
internal class PlayerInventory : MonoBehaviour, IEnumerable<BaseItem>
{
    private List<BaseItem> backpack = new List<BaseItem>();

    /// <summary>
    /// The get enumerator.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerator"/>.
    /// </returns>
    public IEnumerator<BaseItem> GetEnumerator()
    {
        return this.backpack.GetEnumerator();
    }

    /// <summary>
    /// The get enumerator.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerator"/>.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    /// <summary>
    /// The add item.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    public void AddItem(BaseItem item)
    {
        this.backpack.Add(item);
    }

    /// <summary>
    /// Remove an item from the backpack.
    /// </summary>
    /// <param name="item">
    /// The item to remove
    /// </param>
    public void RemoveItem(BaseItem item)
    {

    }

    private void Start()
    {
        BaseItem item = ItemGenerator.GenerateItem(GeneratedItemType.Equipment);
        Debug.Log(item);
        Debug.Log(item.GetDescription(false));
    }
}