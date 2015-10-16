using System.Net.Mime;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;


using DG.Tweening;


/// <summary>
/// Displays general info about the game, like player HP, boss coutner etc.
/// </summary>
public class InfoDisplay : MonoBehaviour
{
    public Text PlayerHealthText;
    public Text BossCounterText;
    public Text PlayerXPText;

    public Color HealthColour;
    public Color XPColour;

    private Damagable _playerHealth;
    private TurnManager _turnManager;
    private PlayerExperience _experience;

    private int oldCurrentHealth;
    private int oldMaxHealth;
    private int oldExperience;

    private

    void Awake()
    {
        _playerHealth = GameObject.FindWithTag("Player").GetComponent<Damagable>();
        _turnManager = GameObject.FindWithTag("GM").GetComponent<TurnManager>();
        _experience = GameObject.FindWithTag("Player").GetComponent<PlayerExperience>();
    }

    void Update()
    {
        int newCurrentHealth = _playerHealth.CurrentHealth;
        int newMaxHealth = _playerHealth.MaxHealth;
        int newExperience = _experience.GetCurrentXP();

        // Update Health
        if (newCurrentHealth != oldCurrentHealth || newMaxHealth != oldMaxHealth){
            PlayerHealthText.text = _playerHealth.CurrentHealth.ToString() + "/" + _playerHealth.MaxHealth.ToString();
            UpdateProp(PlayerHealthText, HealthColour);
        }
        // Update Experience
        if (newExperience != oldExperience){
            PlayerXPText.text = _experience.GetCurrentXP() + "/" + _experience.GetNeededXP();
            UpdateProp(PlayerXPText, XPColour);
        }

        // Update Boss counter
        BossCounterText.text = TurnManager.BossCounter.ToString();

        oldCurrentHealth = newCurrentHealth;
        oldMaxHealth = newMaxHealth;
        oldExperience = newExperience;
    }

    // Triggers an animation for the text component
    private void UpdateProp(Text component, Color color){
        color.a = 1.0f; // TODO: This is just a stupid temp solution because of linux editor
        component.DOColor(Color.white, 0.3f).OnComplete(() => component.DOColor(color, 0.3f));
        component.transform.DOShakeScale(0.3f, 0.88f, 8, 0);
        //component.transform.DOShakeRotation(0.2f, 30f, 3);
    }

}