using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward=25;
    [SerializeField] int goldPenalty = 25;
    public int TargetReward => goldReward;
    public int TargetPenalty => goldPenalty;
    public event Action OnEnemyKilled;
    public event Action OnEnemyFinished;
   
    public void TargetDied()
    {
        OnEnemyKilled?.Invoke();
    }
    public void TargetReachedEnd()
    {
        OnEnemyFinished?.Invoke();
    }

}
