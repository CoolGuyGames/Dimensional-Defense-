using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string[] dimensions = { "Air", "Fire", "Earth", "Water" };
    private CameraControls cameraControls;

    GameObject[] enemies;
    GameObject closestEnemy = null;

    public GameObject projectile;
    public GameObject radiusObject;

    public float startingShootDelay;
    public float shootDelay;

    public float startingProjectileSpeed;
    public float projectileSpeed;

    public string dimension;

    public int startingDamage;
    public int damage;

    public int startingCost;
    public int cost;

    public string towerName;

    public float startingRadius;
    public float radius;

    private bool canShoot = true;
    public bool isHeld;
    private bool canPlace = true;

    public CircleCollider2D towerCollider;
    private GameManager gameManager;

    private int inContactWith = 0;

    // Update is called once per frame
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cameraControls = GameObject.Find("Main Camera").GetComponent<CameraControls>();
        
    }
    void Update()
    {
        Upgrades();

        goToCursor();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            if (closestEnemy != null)
            {
                if(Vector2.Distance(enemies[i].transform.position, this.transform.position) < Vector2.Distance(closestEnemy.transform.position, this.transform.position) && Vector2.Distance(enemies[i].transform.position, this.transform.position) <= radius)
                    closestEnemy = enemies[i];
            }
            else if (enemies[0] != null)
            {
                closestEnemy = enemies[0];
            }
        }

        if(closestEnemy != null && Vector2.Distance(closestEnemy.transform.position, this.transform.position) <= radius)
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
            towerCollider.enabled = true;
        }
        else
        {
            towerCollider.enabled = false;
        }

        radiusObject.transform.localScale = new Vector3(2 * radius, 2 * radius, 2 * radius);
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
                PlaceParticles();
                PlayerPrefs.SetInt(towerName + "totalPlaces", PlayerPrefs.GetInt(towerName + "totalPlaces") + 1);
            }
            else if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                gameManager.money += cost;
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("noplace") || collision.CompareTag("Tower"))
        {
            canPlace = false;
            inContactWith++;
            radiusObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 76);
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("noplace") || collision.CompareTag("Tower"))
        {
            canPlace = false;
            radiusObject.GetComponent<SpriteRenderer>().color = new Color32 (255, 0, 0, 76);
        }
        if (collision.CompareTag("placeable") && inContactWith == 0)
        {
            canPlace = true;
            radiusObject.GetComponent<SpriteRenderer>().color = new Color32(149, 149, 149, 76);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("noplace") || collision.CompareTag("Tower"))
        {
            inContactWith--;
            if(dimension == dimensions[cameraControls.currentLocation])
            {
                canPlace = true;
                radiusObject.GetComponent<SpriteRenderer>().color = new Color32(149, 149, 149, 76);
            }
        }
        if (collision.CompareTag("placeable") && dimension != dimensions[cameraControls.currentLocation])
        {
            canPlace = false;
            radiusObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 76);
        }
    }

    public ParticleSystem partSystem;
    private void PlaceParticles()
    {
        partSystem.Play();
    }

    public void Upgrades()
    {
        cost = startingCost * (int) (Mathf.Pow(1.5f, PlayerPrefs.GetInt(towerName + "totalPlaces") + 1));
        damage = startingDamage * (int) (Mathf.Pow(1.5f, PlayerPrefs.GetInt(towerName + "Damage" + "Level"))) + PlayerPrefs.GetInt(towerName + "Damage" + "Level");
        shootDelay = startingShootDelay * Mathf.Pow(0.5f, PlayerPrefs.GetInt(towerName + "ShootDelay" + "Level") / 4.0f);
        projectileSpeed = startingProjectileSpeed + PlayerPrefs.GetInt(towerName + "ProjectileSpeed" + "Level");
        radius = startingRadius +  0.1f * PlayerPrefs.GetInt(towerName + "Radius" + "Level");

    }

}
