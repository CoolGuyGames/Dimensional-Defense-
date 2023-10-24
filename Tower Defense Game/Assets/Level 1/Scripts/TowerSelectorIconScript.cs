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
        text = this.transform.GetChild(0).GetComponent<TMP_Text>();
        Tower script = tower.GetComponent<Tower>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cost = tower.GetComponent<Tower>().cost;
        text.text = "Base Damage: " + script.damage + "\nAttack Speed: " + script.shootDelay + "\nRange: " + script.radius + "\nDimension: " + script.dimension;
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
