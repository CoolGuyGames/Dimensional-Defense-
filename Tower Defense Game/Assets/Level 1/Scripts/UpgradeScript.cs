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
                PlayerPrefs.SetInt(Towers[i] + Upgrades[j] + "Level", 0);
                PlayerPrefs.SetInt(Towers[i] + "totalPlaces", 0);
            }

        }
    }
    private void Update()
    {
        for (int i = 0; i < Towers.Length; i++)
        {
            for (int j = 0; j < Upgrades.Length; j++)
            {
                PlayerPrefs.SetInt(Towers[i] + Upgrades[j] + "Cost", (int)(Mathf.Pow(1.5f, (PlayerPrefs.GetInt(Towers[i] + Upgrades[j] + "Level"))) * 10));
            }

        }

        Debug.Log((int) Mathf.Pow(1.5f, (PlayerPrefs.GetInt("FloraDamageLevel"))));
        Debug.Log(PlayerPrefs.GetInt("FloraDamageCost"));
    }
    public void Upgrade(string towerThenUpgrade)
    {
        string[] towerUpgrade = towerThenUpgrade.Split(' ');

        if(PlayerPrefs.GetInt(towerUpgrade[0] + towerUpgrade[1] + "Cost") <= gameManager.money)
        {
            gameManager.money -= PlayerPrefs.GetInt(towerUpgrade[0] + towerUpgrade[1] + "Cost");
            PlayerPrefs.SetInt(towerUpgrade[0] + towerUpgrade[1] + "Level", PlayerPrefs.GetInt(towerUpgrade[0] + towerUpgrade[1] + "Level") + 1);
        }
    }
}
