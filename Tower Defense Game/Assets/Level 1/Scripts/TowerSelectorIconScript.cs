using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerSelectorIconScript : MonoBehaviour
{
    public GameObject tower;
    private int cost;

    private GameManager gameManager;

    public TMP_Text text;

    private void Start()
    {
        Tower script = tower.GetComponent<Tower>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cost = tower.GetComponent<Tower>().cost;
        text.text = "Damage: " + script.damage + "\nAttack Speed: " + script.shootDelay + "\nRange: " + script.radius + "\nDimension: " + script.dimension + "\nCost: " + script.cost;
    }
    public void BuyTower()
    {
        if (gameManager.money >= cost)
        {
            gameManager.money -= cost;
            Instantiate(tower);
        }
    }
}
