using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public int damage;
    public string dimension;

    private ParticleSystem explosionParticles;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        explosionParticles = this.GetComponentInChildren<ParticleSystem>();

        StartCoroutine(Explode(collision));
    }

    IEnumerator Explode(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            this.transform.Find("Trail").gameObject.SetActive(false);
            this.GetComponent<SpriteRenderer>().enabled = false;
            if (dimension == collision.GetComponent<EnemyMovement>().dimension)
                collision.GetComponent<EnemyMovement>().health -= damage * 2;
            else
                collision.GetComponent<EnemyMovement>().health -= damage;
            explosionParticles.Play();
            yield return new WaitForSeconds(explosionParticles.main.duration);
            Destroy(gameObject);
        }
    }
}
