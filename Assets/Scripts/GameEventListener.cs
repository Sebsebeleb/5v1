
using Event;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    private Events triggerEvent;

    [SerializeField]
    private UnityEvent callback;

    void OnEnable()
    {
        
        // We use the OnTurn callback because we dont care about the parameters
        EventManager.Register(this.triggerEvent, (OnTurn)this.Trigger);
    }

    void OnDisable()
    {
        EventManager.UnRegister(this.triggerEvent, (OnTurn)this.Trigger);
    }

    private void Trigger()
    {
        Debug.Log("YEEEEEEEEEEEES");
        this.callback.Invoke();
    }

}