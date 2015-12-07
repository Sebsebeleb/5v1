namespace Assets.Scripts.Input
{
    using UnityEngine;

    // Temp? class for handling some keyboard shortcuts
    public class ShortcutsManager : MonoBehaviour
    {
        private PlayerTargeting playerTargeting;

        public GameObject InventoryScreen;

        private void Awake()
        {
            this.playerTargeting = GameObject.FindWithTag("Player").GetComponent<PlayerTargeting>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                InventoryScreen.SetActive(true);
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
        }
    }
}