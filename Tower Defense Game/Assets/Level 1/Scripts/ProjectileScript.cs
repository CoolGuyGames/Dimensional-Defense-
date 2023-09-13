using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public int damage;
    public string dimension;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            if(dimension == collision.GetComponent<EnemyMovement>().dimension)
                collision.GetComponent<EnemyMovement>().health -= damage * 2;    
            else
                collision.GetComponent<EnemyMovement>().health -= damage;
            Destroy(gameObject);
        }
    }
}
