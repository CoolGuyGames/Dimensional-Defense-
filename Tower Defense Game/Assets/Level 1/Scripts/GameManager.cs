using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text roundText;
    public TMP_Text pointsText;
    public TMP_Text livesText;

    public GameObject gameOver;

    public int money;

    public GameObject[] rounds;
    public GameObject[] enemies;
    public float[] roundDelay;
    public int round = 1;
    public int lives = 100;

    private bool roundInProgress = false;

    public float timeWarp;
    public Text timeWarpText;

    public TMP_Text roundChangeText;

    private float spawnDelay = 3;

    private void Start()
    {
        
    }

    private void Update()
    {
        timeWarpText.text = timeWarp.ToString() + "x";

        if(spawnDelay > 0.5f)
        {
            spawnDelay = 3 - 0.1f * (round - 4);
        }
        else
        {
            spawnDelay = 0.1f;
        }

        Time.timeScale = timeWarp;

        roundChangeText.text = "Round " + round;
        if (!roundInProgress)
        {
            StartCoroutine("RoundSpawner");
        }

        pointsText.text = "Points: " + money;
        roundText.text = "Round: " + round;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }
    }

    IEnumerator RoundSpawner()
    {
        roundInProgress = true;
        if (rounds.Length >= round)
        {
            for (int i = 0; i < rounds[round - 1].transform.childCount; i++)
            {
                Instantiate(rounds[round - 1].transform.GetChild(i));
                yield return new WaitForSeconds(roundDelay[round - 1]);
            }
        }
        else
        {
            for(int i = 0; i < round * 2; i++) 
            {
                Instantiate(enemies[Random.Range(0, enemies.Length)]);
                yield return new WaitForSeconds(spawnDelay);
            }
        }
        
        StartCoroutine("ChangeRounds");
        yield return new WaitForSeconds(4);
        roundInProgress = false;
    }

    IEnumerator ChangeRounds()
    {
        yield return new WaitForSeconds(1f);
        roundChangeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        round++;
        yield return new WaitForSeconds(1f);
        roundChangeText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
    }

    public void IncreaseSpeed()
    {
        if(timeWarp < 3.5f)
        {
            timeWarp += 0.5f;
        }
    }

    public void DecraseSpeed()
    {
        if(timeWarp > 0f)
        {
            timeWarp -= 0.5f;
        }
    }
}
