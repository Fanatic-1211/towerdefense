using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyHealth : MonoBehaviour,IDamageable
{
    [SerializeField] int startHp = 10;
    public event Action<int> OnDamageTaken;
    public event Action OnReachedZero;
    private int currentHp = 0;
    public int CurentHealth => currentHp;
    // Start is called before the first frame update
    void OnEnable()
    {
        currentHp = startHp;
    }

    public void DealDamage(int damagePoints)
    {
        int damageAbs = Math.Abs(damagePoints);
        currentHp -= Math.Abs(damageAbs);
        OnDamageTaken?.Invoke(damageAbs);
        if (currentHp <= 0)
        {
            OnReachedZero?.Invoke();
        }
    }
}
