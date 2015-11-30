namespace Assets.Scripts.Input
{
    using UnityEngine;

    // Temp? class for handling some keyboard shortcuts
    public class ShortcutsManager : MonoBehaviour
    {
        public GameObject InventoryScreen;

        public void Update()
        {
            if (Input.GetKey(KeyCode.I))
            {
                InventoryScreen.SetActive(true);
            }
        }
    }
}