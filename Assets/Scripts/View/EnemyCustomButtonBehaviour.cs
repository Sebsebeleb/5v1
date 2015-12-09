using UnityEngine;
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
        _gridButton = GetComponent<View.GridButtonBehaviour>();
        _inspector = InspectorPanel.GetComponent<InspectorPanelBehaviour>();
        _playerTargeting = GameObject.FindWithTag("Player").GetComponent<PlayerTargeting>();
    }

    public void OnPointerClick(PointerEventData eventData){

        // Rightclick -> Inspect
        if (eventData.button == PointerEventData.InputButton.Right){
            InspectEnemy();
        }


        /*if (eventData.clickCount > 1){
            InspectEnemy();
        }*/

        // Otherwise, target it
        else{
            TargetEnemy();
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
        Actor enemy = GridManager.TileMap.GetAt(_gridButton.x, _gridButton.y);
        InspectorPanel.SetActive (true);

        _inspector.InspectActor (enemy);
    }
    private void TargetEnemy(){
        _playerTargeting.TargetGrid(_gridButton.x, _gridButton.y);
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
