using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public string dimension;

    Transform[] waypoints;
    public float speed = 5f;
    private int currentWaypointIndex = 0;

    public int health;

    private GameManager manager;
    public int moneyOnDeath = 1;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();


        Transform[] waypointsTemp = new Transform[GameObject.Find(dimension).transform.GetChild(0).childCount];
        for (int i = 0; i < GameObject.Find(dimension).transform.GetChild(0).childCount; i++)
        {
            waypointsTemp[i] = GameObject.Find(dimension).transform.GetChild(0).transform.GetChild(i).transform;
        }

        transform.position = waypointsTemp[currentWaypointIndex].position;

        waypoints = waypointsTemp;
    }

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
        else
        {
            manager.lives--;
            Destroy(gameObject);
        }

        if(health <= 0)
        {
            manager.money += moneyOnDeath;
            Destroy(gameObject);
        }
    }
}
