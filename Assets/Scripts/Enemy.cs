using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("Enemy Details")]
    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemylaserPrefab;
    [SerializeField] GameObject VFX;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] float dieVFXDuration = 1f;

    [Header("Sounds")]
    [SerializeField] AudioClip[] enemySounds;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CounterDownAndShoot();
    }

    private void CounterDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(enemylaserPrefab,
                           transform.position,
                           Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);

        AudioSource.PlayClipAtPoint(enemySounds[0],Camera.main.transform.position,0.20f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
        if(other.tag == "PlayerLaser")
        {
            Destroy(other.gameObject);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        StartCoroutine(ShowHit());
        health -= damageDealer.GetDamage();
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameDetails>().AddToScore();
        GameObject dieVFX = Instantiate(VFX,
                            transform.position,
                            Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(dieVFX, dieVFXDuration);
        AudioSource.PlayClipAtPoint(enemySounds[1], Camera.main.transform.position,0.3f);
    }

    private IEnumerator ShowHit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.3349057f, 0.3349057f, 1f);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
