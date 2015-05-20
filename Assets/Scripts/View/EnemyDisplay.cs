using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using View;

public class EnemyDisplay : MonoBehaviour
{
    public Text Name;
    public Text Health;
    public Text Cooldown;
    public Text Attack;
    public Text Defense;

    private GridButtonBehaviour gridbutton;

    private Enemy enemy;

    void Awake()
    {
        gridbutton = GetComponent<GridButtonBehaviour>();
    }

    void Start()
    {

    }

    void Update()
    {
        enemy = GridManager.TileMap.GetAt(gridbutton.x, gridbutton.y);

        Name.text = enemy.name;
        Health.text = enemy.damagable.CurrentHealth.ToString();
        Cooldown.text = enemy.countdown.CurrentCountdown.ToString();

    }
}