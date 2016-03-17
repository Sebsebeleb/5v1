using DG.Tweening;
using DG.Tweening.Core;
using Map;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View;

public class EnemyDisplay : MonoBehaviour
{
                
    // Public access to all the buttons
    public static Dictionary<GridPosition, EnemyDisplay> Displays = new Dictionary<GridPosition, EnemyDisplay>();

    public Text Name, Health, Cooldown, Attack, Defense, Rank;

    public SegmentedBar CooldownBar;

    // Used to detect changes
    private int OldCooldown, OldMaximumCooldown;

    public Image EnemyImage, Targeting, TargetingAffected;

    private GridButtonBehaviour gridbutton;

    private Actor actor;

    void Awake()
    {
        gridbutton = GetComponent<GridButtonBehaviour>();


        // Register a callback to be called after a game was loaded, to correctly update display
        Event.OnGameDeserialized callback = GameWasLoaded;
        Event.EventManager.Register(Event.Events.GameDeserialized, callback);

        Displays[new GridPosition(this.gridbutton.x, this.gridbutton.y)] = this;
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

        //
        // Countdown text
        //

        if (this.actor.countdown.CurrentCountdown != this.OldCooldown
            || this.actor.countdown.MaxCountdown != this.OldMaximumCooldown)
        {
            // Make bar fill update
            float targetFill = (float)this.actor.countdown.CurrentCountdown / this.actor.countdown.MaxCountdown;
            float duration = 0.2f;

            // Tween the update using DOTween generic tween
            DOTween.To(new DOGetter<float>(() => { return this.CooldownBar.Fill; }),new DOSetter<float>((value =>
                {
                    this.CooldownBar.Fill = value;
                })), targetFill, duration);

            this.CooldownBar.NumSegments = this.actor.countdown.MaxCountdown;
            this.CooldownBar.SegmentSpacing = 1 / (this.actor.countdown.MaxCountdown * 0.88f);   

            this.OldCooldown = this.actor.countdown.CurrentCountdown;
            this.OldMaximumCooldown = this.actor.countdown.MaxCountdown;
        }
        string countdownText = actor.countdown.CurrentCountdown.ToString();

        // If the enemy is stunned, changed font color
        if (actor.status.Stunned)
        {
            countdownText = TextUtilities.FontColor("#4040FF", countdownText);
        }
        Cooldown.text = countdownText;

        //
        // Sprite
        //

        EnemyImage.sprite = actor.GetComponent<DisplayData>().Image;


        // Special display for corpses
        if (actualName == "Corpse")
        {
            this.Attack.text = "-";
            this.Health.text = "-";
        }

        else
        {
            Health.text = actor.damagable.CurrentHealth.ToString();


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