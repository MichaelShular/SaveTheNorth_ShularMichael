using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public GameObject enemy;
    public int maxNumberOfEnemys;
    public int currentNumberOfEnemys;
    public float respawnTimer;
    private BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider>();
        SpawnEnemy();
        SpawnEnemy();
        currentNumberOfEnemys = 2;
        StartCoroutine(enemySpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    public void SpawnEnemy()
    {
        var temp = Instantiate(enemy, transform);
        temp.transform.position = new Vector3(Random.Range(box.bounds.min.x, box.bounds.max.x), transform.position.y, Random.Range(box.bounds.min.z, box.bounds.max.z));
    }

    IEnumerator enemySpawnTimer()
    {
        yield return new WaitForSeconds(respawnTimer);
        int chance = Random.Range(0, 100);
        if (currentNumberOfEnemys < maxNumberOfEnemys)
        {
            if (chance < 50)
            {
                SpawnEnemy();
                SpawnEnemy();
                currentNumberOfEnemys += 2;
            }
            else
            {
                SpawnEnemy();
                currentNumberOfEnemys++;
            }
        }
        StartCoroutine(enemySpawnTimer());
    }
}
