using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;

    public float minSpawnDelay;
    public float maxSpawnDelay;
    public float minBombSpawnDelay;
    public float maxBombSpawnDelay;

    public float minDrag;
    public float maxDrag;


    // ui 설명 생략, 주여 오브젝트 위주 


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
            // Fruit Prefab 중 하나 랜덤으로 선택
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            // 생성될 과일의 위치를 spawn 영역 내에서 랜덤하게 설정
            Vector3 pos = new Vector3();
            pos.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            pos.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            pos.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            // 과일 생성
            GameObject fruit = Instantiate(prefab, pos, Quaternion.identity);

            // 과일이 떨어지는 속도 조정
            fruit.GetComponent<Rigidbody>().drag = Random.Range(minDrag, maxDrag);

            // 딜레이 최소 시간과 최대 시간 사이에서 랜덤으로 spawn 함
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    private IEnumerator SpawnBomb()
    {
        // initial delay 
        yield return new WaitForSeconds(minBombSpawnDelay);

        while (enabled)
        {
            // 생성될 폭탄의 위치를 spawn 영역 내에서 랜덤하게 설정
            Vector3 pos = new Vector3();
            pos.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            pos.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            pos.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            // 폭탄 생성
            GameObject bomb = Instantiate(bombPrefab, pos, Quaternion.identity);

            // 폭탄이 떨어지는 속도
            bomb.GetComponent<Rigidbody>().drag = 4;

            // 딜레이 최소 시간과 최대 시간 사이에서 랜덤으로 spawn 함
            yield return new WaitForSeconds(Random.Range(minBombSpawnDelay, maxBombSpawnDelay));
        }
    }

}
