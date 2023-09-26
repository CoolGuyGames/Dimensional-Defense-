using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectorIconScript : MonoBehaviour
{
    public GameObject tower;
    private int cost;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cost = tower.GetComponent<Tower>().cost;
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
