using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerSelectorIconScript : MonoBehaviour
{
    public GameObject tower;
    private int cost;

    private GameManager gameManager;
    private string[] strongDimensions = { "Water", "Fire", "Earth", "Air" };
    private string strongAgainst;

    public TMP_Text text;

    public Text[] upgradeCosts;

    Tower script;

    private int damage;
    private float shootDelay;
    private float projectileSpeed;
    private float radius;

    private void Start()
    {
        script = tower.GetComponent<Tower>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        for (int i = 0; i < strongDimensions.Length; i++)
        {
            if (strongDimensions[i] == script.dimension)
            {
                strongAgainst = strongDimensions[(i + 1) % 4];
            }
        }
    }

    private void Update()
    {
        text.text = "Damage: " + damage + "\nAttack Speed: " + shootDelay.ToString("0.00") + "\nRange: " + radius + "\nDimension: " + script.dimension + "\nStrong Against: " + strongAgainst + "\nCost: " + cost;

        cost = script.startingCost * (int)(Mathf.Pow(1.5f, PlayerPrefs.GetInt(script.towerName + "totalPlaces") + 1));
        damage = script.startingDamage * (int)(Mathf.Pow(1.2f, PlayerPrefs.GetInt(script.towerName + "Damage" + "Level"))) + PlayerPrefs.GetInt(script.towerName + "Damage" + "Level");
        shootDelay = script.startingShootDelay  * Mathf.Pow(0.5f, PlayerPrefs.GetInt(script.towerName + "ShootDelay" + "Level") / 4.0f);
        projectileSpeed = script.startingProjectileSpeed + PlayerPrefs.GetInt(script.towerName + "ProjectileSpeed" + "Level");
        radius = script.startingRadius + 0.1f * PlayerPrefs.GetInt(script.towerName + "Radius" + "Level");

        upgradeCosts[0].text = PlayerPrefs.GetInt(script.towerName + "Damage" + "Cost").ToString();
        upgradeCosts[1].text =  PlayerPrefs.GetInt(script.towerName + "ShootDelay" + "Cost").ToString();
        upgradeCosts[2].text = PlayerPrefs.GetInt(script.towerName + "Radius" + "Cost").ToString();
    }
    public void BuyTower()
    {
        if (gameManager.money >= cost && gameManager.timeWarp > 0)
        {
            gameManager.money -= cost;
            Instantiate(tower);
        }
    }
}
