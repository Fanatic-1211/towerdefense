using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 5;
    [SerializeField] float spawnTimer = 1f;
    GameObject[] pool;
    private void Awake()
    {
        PopulatePool();
    }
    private void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemyPrefab,transform);
            pool[i].SetActive(false);
        }
    }
    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    private void EnableObjectInPool()
    {
        GameObject disabledObj = pool.FirstOrDefault(o => !o.activeInHierarchy);
        if (disabledObj != null) disabledObj.SetActive(true);
    }
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
