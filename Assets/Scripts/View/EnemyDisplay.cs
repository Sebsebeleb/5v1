using UnityEngine;
using UnityEngine.UI;
using View;

public class EnemyDisplay : MonoBehaviour
{
    public Text Name;
    public Text Health;
    public Text Cooldown;
    public Text Attack;
    public Text Defense;

    public Image EnemyImage;

    private GridButtonBehaviour gridbutton;

    private Actor actor;

    void Awake()
    {
        gridbutton = GetComponent<GridButtonBehaviour>();
    }

    void Start()
    {

    }

    void Update()
    {
        actor = GridManager.TileMap.GetAt(gridbutton.x, gridbutton.y);

        string actualName = actor.name;
        // Strip "(Clone)"
        while (actualName.EndsWith("(Clone)"))
        {
            actualName = actualName.Substring(0, actualName.Length - 7);
        }
        Name.text = actualName;
        Health.text = actor.damagable.CurrentHealth.ToString();

        //If the enemy is stunned, changed font color
        string countdownText = actor.countdown.CurrentCountdown.ToString();
        if (actor.status.Stunned){
            countdownText = RichTextUtilities.FontColor("#4040FF", countdownText);
        }
        Cooldown.text = countdownText;

        EnemyImage.sprite = actor.GetComponent<DisplayData>().Image;

        // Make attack text TODO: Should only be updated when attack is updated
        string attackText = actor.attack.CalculateAttack().ToString();
        //If enemy has bonus attack, display it in green. If negative, red
        if (actor.attack.BonusAttack > 0)
        {
            attackText = RichTextUtilities.FontColor("#20DD20FF", attackText);
        }
        else if (actor.attack.BonusAttack < 0)
        {
            attackText = RichTextUtilities.FontColor("#DD2020FF", attackText);
        }

        Attack.text = attackText;

    }
}