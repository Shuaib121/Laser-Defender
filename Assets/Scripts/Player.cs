using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Player : MonoBehaviour
{
    //config params
    [Header ("Player Details")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;

    [Header ("Projectiles")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header("Sounds")]
    [SerializeField] AudioClip[] playerSounds;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());

        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            //Quarternion.identity means to add no rotation, use existing rotation
            GameObject laser = Instantiate(laserPrefab,
                               transform.position,
                               Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            AudioSource.PlayClipAtPoint(playerSounds[0],Camera.main.transform.position,0.15f);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }  
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding ;
    }

    private void Move()
    {
        //gets player x,y values and adds to it to move player
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime *moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        //new position variables
        var newXPos = transform.position.x + deltaX;
        var newYPos = transform.position.y + deltaY;

        //moves the players
        transform.position = new Vector2(Mathf.Clamp(newXPos,xMin,xMax),Mathf.Clamp(newYPos,yMin,yMax));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
        Destroy(other.gameObject);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(playerSounds[1],Camera.main.transform.position,0.2f);
            FindObjectOfType<SceneLoader>().LoadGameOver();
            Cursor.visible = true;
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
