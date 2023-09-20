using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    GameObject[] enemies;
    GameObject closestEnemy = null;

    public GameObject projectile;
    public GameObject radiusObject;
    public float shootDelay;
    public float projectileSpeed;
    public string dimension;
    public int damage;


    public int radius;
    private bool canShoot = true;
    public bool isHeld;
    private bool canPlace = true;

    public CircleCollider2D towerCollider;

    // Update is called once per frame
    void Update()
    {
        goToCursor();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            if (closestEnemy != null)
            {
                if(Vector2.Distance(enemies[i].transform.position, this.transform.position) < Vector2.Distance(closestEnemy.transform.position, this.transform.position) && Vector2.Distance(enemies[i].transform.position, this.transform.position) <= radius)
                    closestEnemy = enemies[i];
            }
            else if (enemies[0] != null && Vector2.Distance(enemies[0].transform.position, this.transform.position) <= radius)
            {
                closestEnemy = enemies[0];
            }
        }

        if(closestEnemy != null)
        {
            Vector3 offset = closestEnemy.transform.position - transform.position;

            transform.rotation = Quaternion.LookRotation(
                                   Vector3.forward, // Keep z+ pointing straight into the screen.
                                   offset           // Point y+ toward the target.
                                 );
        }

        if(canShoot && closestEnemy != null && !isHeld && Vector2.Distance(closestEnemy.transform.position, this.transform.position) <= radius)
        {
            StartCoroutine("Shoot");
        }

        if(isHeld == false)
        {

        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject shotProjectile = Instantiate(projectile, this.transform.position, transform.rotation);
        shotProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2((closestEnemy.transform.position - transform.position).normalized.x, (closestEnemy.transform.position - transform.position).normalized.y) * projectileSpeed;
        shotProjectile.GetComponent<ProjectileScript>().dimension = dimension;
        shotProjectile.GetComponent<ProjectileScript>().damage = damage;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    private void OnMouseEnter()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            isHeld = true;
        }
    }

    private void OnMouseExit()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void goToCursor()
    {
        if(isHeld)
        {
            Vector3 mousPos = Input.mousePosition;
            mousPos.z = -1 * Camera.main.transform.position.z - 1;
            transform.position = Camera.main.ScreenToWorldPoint(mousPos);
            if(Input.GetKeyDown(KeyCode.Mouse0) && canPlace)
            {
                isHeld = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("noplace") || collision.CompareTag("Tower"))
        {
            canPlace = false;
            radiusObject.GetComponent<SpriteRenderer>().color = new Color32 (255, 0, 0, 76);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("noplace"))
        {
            canPlace = true;
            radiusObject.GetComponent<SpriteRenderer>().color = new Color32(149, 149, 149, 76);
        }
    }


}
