using System;

using UnityEngine;

public class CountdownBehaviour : MonoBehaviour
{
    //For the inspector
    #region Inspector Values
    [SerializeField]
    private int StartMaxCountdown;

    #endregion

    #region Public properties

    public int MaxCountdown{
        get {return data.MaxCountdown;}
        set {data.MaxCountdown = value;}
    }
    public int CurrentCountdown{
        get {return data.CurrentCountdown;}
        set {data.CurrentCountdown = value;}
    }

    #endregion

    // Our actual data

    private CountdownData data;

    // References
    private Actor actor;
    private AI _brain;

    void Awake()
    {
        this.actor = GetComponent<Actor>();
        _brain = GetComponent<AI>();

    }

    void OnSpawn(){
        data.MaxCountdown = StartMaxCountdown;
        data.CurrentCountdown = data.MaxCountdown;
    }

    public void Countdown()
    {
        BroadcastMessage("OnTurn");

        // If we are stunned, we do not cooldown
        if (this.actor.status.Stunned){
            return;
        }
        // If we are a corpse, and the boss wave has started, do not countdown
        if (TurnManager.BossCounter <= 0 && gameObject.tag == "Corpse"){
            return;
        }

        Event.EventManager.Notify(Event.Events.ActorCountedDown, this.actor);


        CurrentCountdown--;
        if (CurrentCountdown <= 0)
        {
            CurrentCountdown = MaxCountdown;
            DoAction();
            BroadcastMessage("OnAct", SendMessageOptions.DontRequireReceiver);
            Event.EventManager.Notify(Event.Events.ActorActed, this.actor);
        }
    }

    private void DoAction()
    {
        StartCoroutine(_brain.Think());
    }

    public CountdownData _GetRawData(){
        return data;
    }

    public void _SetRawData(CountdownData _data){
        data = _data;
    }
}