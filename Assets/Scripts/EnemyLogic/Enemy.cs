using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy : MonoBehaviour,ITargetable
{
    [SerializeField] int goldReward=25;
    [SerializeField] int goldPenalty = 25;
    [SerializeField] EnemyHealth enemyHealth;
    public int TargetReward => goldReward;
    public int TargetPenalty => goldPenalty;
    public event Action OnEnemyKilled;
    public event Action OnEnemyFinished;
    private void Awake()
    {
        enemyHealth.OnReachedZero+=TargetDied;
    }

    public Vector3 GetTargetPosition() => this.transform.position;

    public void TargetDied()
    {
        OnEnemyKilled?.Invoke();
    }
    public void TargetReachedEnd()
    {
        OnEnemyFinished?.Invoke();
    }

}
