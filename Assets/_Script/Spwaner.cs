using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwaner : MonoBehaviour
{
    public GameObject[] enemy;
    public float respawnTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawner()); 
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemy();
        }
     
    }

    void SpawnEnemy()
    {
        int randomValue = Random.Range(0, enemy.Length);
        int randomXpos = Random.Range(-1, 1);
        Instantiate(enemy[randomValue], new Vector2(randomXpos,transform.position.y), Quaternion.identity);
    }
}
