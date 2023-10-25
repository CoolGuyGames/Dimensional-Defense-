using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    public string[] Towers;
    public string[] Upgrades;

    private GameManager gameManager;
    private void Start()
    {
        gameManager = GetComponent<GameManager>();

        for(int i = 0; i < Towers.Length; i++)
        {
            for(int j = 0; j < Upgrades.Length; j++)
            {
                PlayerPrefs.SetInt(Towers[i] + Upgrades[j] + "Cost", 10);
                PlayerPrefs.SetInt(Towers[i] + Upgrades[j] + "Level", 0);
            }

        }
    }
    private void Update()
    {
        for (int i = 0; i < Towers.Length; i++)
        {
            for (int j = 0; j < Upgrades.Length; j++)
            {
                PlayerPrefs.SetInt(Towers[i] + Upgrades[j] + "Cost", (int) (PlayerPrefs.GetInt(Towers[i] + Upgrades[j] + "Level") * 1.2 + 10));
            }

        }
    }
    public void Upgrade(string tower, string upgrade)
    {
        if(PlayerPrefs.GetInt(tower + upgrade + "Cost") <= gameManager.money)
        {
            gameManager.money -= PlayerPrefs.GetInt(tower + upgrade + "Cost");
            PlayerPrefs.SetInt(tower + upgrade + "Level", PlayerPrefs.GetInt(tower + upgrade + "Level") + 1);
        }
    }

    public void Nothing()
    {

    }
}
