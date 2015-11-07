using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Generation;

using UnityEngine;

class PlayerInventory : MonoBehaviour
{
    private BaseItem items;

    void Start()
    {
        BaseItem item = ItemGenerator.GenerateItem(GeneratedItemType.Equipment);
        Debug.Log(item);
        Debug.Log(item.GetDescription(false));
    }
}
