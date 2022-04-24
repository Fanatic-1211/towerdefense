using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;
using Game.Environment.Tile;

    public class EnemyFactory : MonoBehaviour
{
    [SerializeField] TileManager tileManager;
    [SerializeField] Enemy enemyPrefab;
    [Range(0,50)][SerializeField] int poolSize = 5;
    [Range(0.1f,30f)][SerializeField] float spawnTimer = 1f;
    List<Enemy> activeEnemy = new List<Enemy>();
    List<Enemy> disabledEnemy = new List<Enemy>();
    IBillingSystem bank;
    [Inject]
    public void Construct(IBillingSystem bank)
    {
        this.bank = bank;
    }

    private void Awake()
    {
        PopulatePool();
    }
    private void PopulatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            disabledEnemy.Add(Instantiate(enemyPrefab, transform));
        }
        disabledEnemy.ForEach(e => e.OnEnemyFinished += () => OnEnemyFinished(e));
        disabledEnemy.ForEach(e => e.OnEnemyKilled += () => OnEnemyKilled(e));
        disabledEnemy.ForEach(e => e.gameObject.SetActive(false));
    }
    private void OnEnemyKilled(Enemy target)
    {
        bank.Deposit(target.TargetReward);
        ReturnToPool(target);
    }
    private void OnEnemyFinished(Enemy target)
    {
        bank.Withdraw(target.TargetPenalty);
        ReturnToPool(target);
    }
    private void ReturnToPool(Enemy enemy)
    {
        activeEnemy.Remove(enemy);
        enemy.gameObject.SetActive(false);
        disabledEnemy.Add(enemy);
    }
    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    private void EnableObjectInPool()
    {
        if (disabledEnemy.Count > 0)
        {
            Enemy disabledObj = disabledEnemy[0];
            disabledEnemy.Remove(disabledObj);
            disabledObj.gameObject.SetActive(true);
            activeEnemy.Add(disabledObj);
        }
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
