using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 10f;
    public float padding = 0.8f;
    public GameObject explosion;
    public PlayerHealthBarScript healthBarScript;
    public GameObject damageEffect;
    public CoinCount coinCount;
    public GameCotroller gameCotroller;

    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip explosionSound;
    public AudioClip coinSound;

    float minX;
    float maxX;
    float minY;
    float maxY;

    public float health = 20f;
    float barFillAmount = 1f;
    float damage = 0;
    // Start is called before the first frame update
    void Start()
    {
        FindBoundaries();
        damage = barFillAmount / health;
    }

    void FindBoundaries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }    

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        float newXpos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        float newYpos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        transform.position= new Vector2 (newXpos, newYpos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            audioSource.PlayOneShot(damageSound, 0.5f);
            DamagePlayerHealthBar();
            Destroy(collision.gameObject);
            GameObject damageVFx = Instantiate(damageEffect, collision.transform.position, Quaternion.identity);
            Destroy(damageVFx,0.05f);
            if (health <= 0)
            {
                AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 0.5f);
                gameCotroller.GameOver();
                Destroy(gameObject);
                GameObject playerExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(playerExplosion, 0.8f);
            }


        
        }

        if (collision.gameObject.tag == "Coin")
        {
            audioSource.PlayOneShot(coinSound, 0.5f);
            coinCount.AddCount();
            Destroy(collision.gameObject);
        }    
    }

    void DamagePlayerHealthBar()
    {
        if (health > 0)
        {
            health -= 1;
            barFillAmount = barFillAmount - damage;
            healthBarScript.SetAmount(barFillAmount);
        }
    }
}
