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
        while (actualName.EndsWith("(Clone)")) {
            actualName = actualName.Substring(0, actualName.Length - 7);
        }
        Name.text = actualName;
        Health.text = actor.damagable.CurrentHealth.ToString();
        Cooldown.text = actor.countdown.CurrentCountdown.ToString();

        EnemyImage.sprite = actor.GetComponent<DisplayData>().Image;

    }
}