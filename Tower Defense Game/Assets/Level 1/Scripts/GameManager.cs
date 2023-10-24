using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UIElements;

public class GameManager : MonoBehaviour
{
    public TMP_Text pointsRoundText;
    public GameObject gameOver;

    public int money;

    public GameObject[] rounds;
    public GameObject[] enemies;
    public float[] roundDelay;
    public int round = 1;
    public int lives = 100;

    private bool roundInProgress = false;

    public TMP_Text roundChangeText;

    private void Start()
    {
        Time.timeScale = 2f;
    }

    private void Update()
    {
        roundChangeText.text = "Round " + round;
        if (!roundInProgress)
        {
            StartCoroutine("RoundSpawner");
        }

        pointsRoundText.text = "Points: " + money + "\n" + "Round: " + round + "\n" + "Lives: " + lives;

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
                yield return new WaitForSeconds(2 * Mathf.Pow(1.05f, -1 * round));
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
}
