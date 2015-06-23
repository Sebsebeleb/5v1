using UnityEngine;

public class CountdownBehaviour : MonoBehaviour
{
    public int MaxCountdown;
    public int CurrentCountdown;

    private Actor _actor;
    private AI _brain;

    void Awake()
    {
        _actor = GetComponent<Actor>();
        _brain = GetComponent<AI>();

    }

    public void Countdown()
    {
        BroadcastMessage("OnTurn");
        
        // If we are stunned, we do not cooldown
        if (_actor.status.Stunned){
            return;
        }
        

        CurrentCountdown--;
        if (CurrentCountdown <= 0)
        {
            CurrentCountdown = MaxCountdown;
            DoAction();
            BroadcastMessage("OnAct", SendMessageOptions.DontRequireReceiver);
            Event.EventManager.Notify(Event.Events.ActorActed, _actor);
        }
    }

    private void DoAction()
    {
        _brain.Think();
    }
}