using System.Net.Mime;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// Displays general info about the game, like player HP, boss coutner etc.
/// </summary>
public class InfoDisplay : MonoBehaviour
{
    public Text PlayerHealthText;
    public Text BossCounterText;
    public Text PlayerXPText;

    private Damagable _playerHealth;
    private TurnManager _turnManager;
    private PlayerExperience _experience;
    void Awake()
    {
        _playerHealth = GameObject.FindWithTag("Player").GetComponent<Damagable>();
        _turnManager = GameObject.FindWithTag("GM").GetComponent<TurnManager>();
        _experience = GameObject.FindWithTag("Player").GetComponent<PlayerExperience>();
    }

    void Update()
    {
        PlayerHealthText.text = _playerHealth.CurrentHealth.ToString() + "/" + _playerHealth.MaxHealth.ToString();
        BossCounterText.text = TurnManager.BossCounter.ToString();
        PlayerXPText.text = _experience.GetCurrentXP() + "/" + _experience.GetNeededXP();
    }
}