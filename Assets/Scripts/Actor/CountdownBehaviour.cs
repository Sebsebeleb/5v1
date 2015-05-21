using UnityEngine;

public class CountdownBehaviour : MonoBehaviour
{
    public int MaxCountdown;
    public int CurrentCountdown;

    private Enemy _enemy;
    private AI _brain;

    void Awake()
    {
        _enemy = GetComponent<Enemy>();
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