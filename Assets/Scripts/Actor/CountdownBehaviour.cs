using Event;
using UnityEngine;

public class CountdownBehaviour : MonoBehaviour
{
    public readonly int MaxCountdown;
    public int CurrentCountdown;

    private Enemy _enemy;

    void Awake()
    {
        _enemy = GetComponent<Enemy>();

        OnCountedDown callback = OnCountedDown;
        EventManager.Register(Events.OnCountedDown, callback);
    }

    private void OnCountedDown(CountedDownArgs args)
    {

    }

    public void Countdown()
    {
        CurrentCountdown--;

        if (CurrentCountdown <= 0)
        {
            DoAction();
            CurrentCountdown = MaxCountdown;
        }
    }

    private void DoAction()
    {
        EventManager.Notify(Events.OnCountedDown, new CountedDownArgs(_enemy));
    }
}