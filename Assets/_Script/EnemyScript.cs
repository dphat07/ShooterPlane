using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class EnemyScript : MonoBehaviour
{
    public Transform[] gunPoint;
   
    public GameObject enemyFlash;
    public GameObject enemyBullet;
    public GameObject explosionPrefab;
    public HealthBar healthBar;
    public GameObject damageEffect;
    public float enemyBulletSpawnTime = 0.5f;

    public float health = 10f;
    float damage = 0f;
    float barSize = 1f;

    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        enemyFlash.SetActive(false);
        StartCoroutine(EnemyShooting());
        damage = barSize / health;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            DamageHealthBar();
            Destroy(collision.gameObject);
            GameObject damageVFx =  Instantiate(damageEffect, collision.transform.position, Quaternion.identity);
            Destroy(damageVFx, 0.05f);
            if (health <=0)
            {
                Destroy(gameObject);
                GameObject enemyExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(enemyExplosion, 0.6f);

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void DamageHealthBar ()
    {
        if (health > 0)
        {
            health -= 1;
            barSize = barSize - damage;
            healthBar.SetSize(barSize);
        }
    }

    void EnemyFire()
    {
        //Instantiate(enemyBullet, gunPoint1.position, Quaternion.identity);
        //Instantiate(enemyBullet, gunPoint2.position, Quaternion.identity);

        for (int i=0; i< gunPoint.Length; i++)
        {
            Instantiate(enemyBullet, gunPoint[i].position, Quaternion.identity);
        }
    }    

    IEnumerator EnemyShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyBulletSpawnTime);
            EnemyFire();
            enemyFlash.SetActive(true);
            yield return new WaitForSeconds(0.04f);
            enemyFlash.SetActive(false);
        }
      
    }
}
