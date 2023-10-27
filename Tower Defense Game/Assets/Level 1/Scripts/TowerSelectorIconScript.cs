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
    }

    private void Update()
    {
       
        text.text = "Damage: " + damage + "\nAttack Speed: " + shootDelay + "\nRange: " + radius + "\nDimension: " + script.dimension + "\nCost: " + cost;

        cost = 10 * (int)(Mathf.Pow(1.5f, PlayerPrefs.GetInt(script.towerName + "totalPlaces") + 1));
        damage = script.startingDamage * (int)(Mathf.Pow(1.2f, PlayerPrefs.GetInt(script.towerName + "Damage" + "Level"))) + PlayerPrefs.GetInt(script.towerName + "Damage" + "Level");
        shootDelay = script.startingShootDelay * Mathf.Pow(0.9f, PlayerPrefs.GetInt(script.towerName + "ShootDelay" + "Level"));
        projectileSpeed = script.startingProjectileSpeed + PlayerPrefs.GetInt(script.towerName + "ProjectileSpeed" + "Level");
        radius = script.startingRadius + 0.1f * PlayerPrefs.GetInt(script.towerName + "Radius" + "Level");

        upgradeCosts[0].text = PlayerPrefs.GetInt(script.towerName + "Damage" + "Cost").ToString();
        upgradeCosts[1].text = PlayerPrefs.GetInt(script.towerName + "ShootDelay" + "Cost").ToString();
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
