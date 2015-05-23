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
        CurrentCountdown--;

        if (CurrentCountdown <= 0)
        {
            CurrentCountdown = MaxCountdown;
            DoAction();
        }
    }

    private void DoAction()
    {
        _brain.Think();
    }
}