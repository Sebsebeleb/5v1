namespace Assets.Scripts.Input
{
    using UnityEngine;

    // Temp? class for handling some keyboard shortcuts
    public class ShortcutsManager : MonoBehaviour
    {
        private PlayerTargeting playerTargeting;

        public GameObject InventoryScreen;

        public GameObject DebugScreen;

        [SerializeField]
        private InspectorPanelBehaviour inspector;

        private void Awake()
        {
            this.playerTargeting = GameObject.FindWithTag("Player").GetComponent<PlayerTargeting>();
        }

        public void Update()
        {
            // Show inventory
            if (Input.GetKeyDown(KeyCode.I))
            {
                // Put the window on top of other windows
                this.InventoryScreen.transform.SetSiblingIndex(this.InventoryScreen.transform.parent.childCount-1);
                InventoryScreen.SetActive(!this.InventoryScreen.activeInHierarchy);
            }

            // Show player info
            else if (Input.GetKeyDown(KeyCode.C))
            {
                this.inspector.gameObject.SetActive(!this.inspector.gameObject.activeInHierarchy);
                this.inspector.transform.SetSiblingIndex(this.inspector.transform.parent.childCount-1);
                this.inspector.InspectActor(Actor.Player);
            }

            else if (Input.GetKeyDown(KeyCode.Q))
            {
                
                this.playerTargeting.TargetGrid(0, 0);
            }

            else if (Input.GetKeyDown(KeyCode.W))
            {
                
                this.playerTargeting.TargetGrid(1, 0);
            }

            else if (Input.GetKeyDown(KeyCode.E))
            {
                
                this.playerTargeting.TargetGrid(2, 0);
            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                
                this.playerTargeting.TargetGrid(0, 1);
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                
                this.playerTargeting.TargetGrid(1, 1);
            }

            else if (Input.GetKeyDown(KeyCode.D))
            {
                
                this.playerTargeting.TargetGrid(2, 1);
            }

            if (Input.GetKeyDown(KeyCode.F8))
            {
                PlayerPrefs.DeleteAll();
            }

#if DEBUG
            else if (Input.GetKeyDown(KeyCode.O))
            {
                this.DebugScreen.SetActive(!this.DebugScreen.activeInHierarchy);
            }
#endif
        }
    }
}