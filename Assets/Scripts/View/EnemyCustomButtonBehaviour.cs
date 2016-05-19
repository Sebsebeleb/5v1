using UnityEngine;

namespace BBG.View
{
    using BBG.Actor;

    using UnityEngine.EventSystems;

    public class EnemyCustomButtonBehaviour : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        public GameObject InspectorPanel;
        private InspectorPanelBehaviour _inspector;

        private View.GridButtonBehaviour _gridButton;

        // Used to tell the player we want to target us
        private PlayerTargeting _playerTargeting;

        private bool isHeld;

        private float holdTime;

        void Awake(){
            this._gridButton = this.GetComponent<View.GridButtonBehaviour>();
            this._inspector = this.InspectorPanel.GetComponent<InspectorPanelBehaviour>();
            this._playerTargeting = GameObject.FindWithTag("Player").GetComponent<PlayerTargeting>();
        }

        public void OnPointerClick(PointerEventData eventData){

            // Rightclick -> Inspect
            if (eventData.button == PointerEventData.InputButton.Right){
                this.InspectEnemy();
            }


            /*if (eventData.clickCount > 1){
            InspectEnemy();
        }*/

            // Otherwise, target it
            else{
                this.TargetEnemy();
            }
        }

        void Update()
        {
            if (this.isHeld)
            {
                this.holdTime += Time.deltaTime;
            }
            else
            {
                this.holdTime = 0f;
            }

            if (this.holdTime > 0.4f)
            {
                this.InspectEnemy();
            }

        }

        private void InspectEnemy(){
            Actor enemy = GridManager.TileMap.GetAt(this._gridButton.x, this._gridButton.y);
            this.InspectorPanel.SetActive (true);

            this._inspector.InspectActor (enemy);
        }
        private void TargetEnemy(){
            this._playerTargeting.TargetGrid(this._gridButton.x, this._gridButton.y);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            this.isHeld = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            this.isHeld = false;
        }
    }
}
