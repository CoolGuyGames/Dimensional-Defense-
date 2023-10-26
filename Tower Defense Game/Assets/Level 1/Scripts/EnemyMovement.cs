using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public string dimension;

    Transform[] waypoints;
    public float startSpeed = 5f;
    public float speed;
    private int currentWaypointIndex = 0;

    public int startingHealth;
    public int health;

    private GameManager manager;
    public int moneyOnDeath = 1;

    private void Start()
    {

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        speed = startSpeed + 0.05f * manager.round;
        health = (int) (startingHealth * Mathf.Pow(2, manager.round / 10.0f));

        Transform[] waypointsTemp = new Transform[GameObject.Find(dimension).transform.GetChild(0).childCount];
        for (int i = 0; i < GameObject.Find(dimension).transform.GetChild(0).childCount; i++)
        {
            waypointsTemp[i] = GameObject.Find(dimension).transform.GetChild(0).transform.GetChild(i).transform;
        }

        transform.position = waypointsTemp[currentWaypointIndex].position;

        waypoints = waypointsTemp;
    }
    bool exploding = false;

    private void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, (targetPosition - transform.position).normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, 100  * speed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                currentWaypointIndex++;
            }
        }
        else if (exploding == false)
        {
            manager.lives--;
            StartCoroutine(Explosion());
        }

        if(health <= 0 && !exploding)
        {
            manager.money += moneyOnDeath;
            StartCoroutine(Explosion());
        }
    }

    IEnumerator Explosion()
    {
        exploding = true;
        this.transform.GetComponentInChildren<ParticleSystem>().Play();
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        this.transform.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
        exploding = false;
    }
}
