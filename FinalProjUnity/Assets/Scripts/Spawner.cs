using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;
    public float minBombSpawnDelay = 1f;
    public float maxBombSpawnDelay = 2f;

    public float minDrag = 0;
    public float maxDrag = 3;
    
    public float maxLifeTime = 5f;


    void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnFruit());
        StartCoroutine(SpawnBomb());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnFruit()
    {
        // initial delay
        yield return new WaitForSeconds(2f);

        // spawning routine
        while (enabled)
        {
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
            Vector3 pos = new Vector3();
            pos.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            pos.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            pos.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            GameObject fruit = Instantiate(prefab, pos, Quaternion.identity);
            fruit.GetComponent<Rigidbody>().drag = Random.Range(minDrag, maxDrag);
            Destroy(fruit, maxLifeTime);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    private IEnumerator SpawnBomb()
    {
        while (enabled)
        {
            Vector3 pos = new Vector3();
            pos.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            pos.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            pos.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            GameObject bomb = Instantiate(bombPrefab, pos, Quaternion.identity);
            bomb.GetComponent<Rigidbody>().drag = 4;

            yield return new WaitForSeconds(Random.Range(minBombSpawnDelay, maxBombSpawnDelay));
        }
    }

}
