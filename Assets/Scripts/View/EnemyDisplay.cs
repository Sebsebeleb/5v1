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

    public Text Rank;

    public Image EnemyImage;

    private GridButtonBehaviour gridbutton;

    private Actor actor;

    void Awake()
    {
        gridbutton = GetComponent<GridButtonBehaviour>();


        // Register a callback to be called after a game was loaded, to correctly update display
        Event.OnGameDeserialized callback = GameWasLoaded;
        Event.EventManager.Register(Event.Events.GameDeserialized, callback);
    }

    void Start()
    {

    }

    void Update()
    {
        actor = GridManager.TileMap.GetAt(gridbutton.x, gridbutton.y);

        this.Rank.text = this.actor.Rank.ToString();

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
            countdownText = TextUtilities.FontColor("#4040FF", countdownText);
        }
        Cooldown.text = countdownText;

        EnemyImage.sprite = actor.GetComponent<DisplayData>().Image;

        // Make attack text TODO: Should only be updated when attack is updated
        string attackText = actor.attack.CalculateAttack().ToString();
        //If enemy has bonus attack, display it in green. If negative, red
        if (actor.attack.BonusAttack > 0)
        {
            attackText = TextUtilities.FontColor("#20DD20FF", attackText);
        }
        else if (actor.attack.BonusAttack < 0)
        {
            attackText = TextUtilities.FontColor("#DD2020FF", attackText);
        }

        Attack.text = attackText;

    }

    // This is called after loadign a save, correts its image and name display
    private void GameWasLoaded(){
        actor = GridManager.TileMap.GetAt(gridbutton.x, gridbutton.y);

        GameObject prefab = GameResources.GetEnemyByID(actor.enemyTypeID);

        // A pretty silly way, but it works. Basically, since name and image is usually only set when the enemy is spawned, we force it to change.
        actor.GetComponent<DisplayData>().Image = prefab.GetComponent<DisplayData>().Image;
        actor.name = prefab.name;

    }
}