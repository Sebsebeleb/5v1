using Assets.Scripts.View.Inventory;

using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Display Entires for items in the backpack are added here
    [SerializeField]
    private Transform backpackObject;

    [SerializeField]
    private Transform equippedObject;

    private PlayerInventory inventory;

    [SerializeField]
    private GameObject itemDisplayEntryPrefab;

    public void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    public void Update()
    {
        // Check if user wants to exit this screen
        if (Input.GetButton("Cancel"))
        {
            gameObject.SetActive(false);
        }
    }

    public void OnEnable()
    {
        this.Clear();
        this.PopulateChildren();
    }

    private void PopulateChildren()
    {
        foreach (var item in inventory)
        {
            CreateItem(item, backpackObject);
        }
    }

    private void CreateItem(BaseItem item, Transform parent)
    {
        GameObject entry = Instantiate(itemDisplayEntryPrefab) as GameObject;

        InventoryItemEntry entryBehaviour = entry.GetComponent<InventoryItemEntry>();
        entryBehaviour.SetItem(item);
        entry.transform.SetParent(parent);
    }

    private void Clear()
    {
        foreach (Transform child in backpackObject)
        {
            Destroy(child.gameObject);
        }
    }
}