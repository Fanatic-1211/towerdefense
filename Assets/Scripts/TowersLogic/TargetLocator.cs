using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    ITargetable target;
    private List<ITargetable> enemyWithInRange = new List<ITargetable>();
    public event Action<ITargetable> OnNewTargetArrive;
    public event Action OnNoTargets;
    ITargetable currentTarget;
    float radius = 15f;

    private void Update()
    {
        AimWeapon();
    }

    private void OnTriggerEnter(Collider other)
    {
        ITargetable target = other.GetComponent<ITargetable>();
        if (target != null)
        {
            enemyWithInRange.Add(target);
            if (currentTarget == null)
            {
                currentTarget = enemyWithInRange[0];
                OnNewTargetArrive?.Invoke(currentTarget);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ITargetable target = other.GetComponent<ITargetable>();
        if (target != null)
        {
            enemyWithInRange.Remove(target);
            if (currentTarget == target)
            {
                if (enemyWithInRange.Count > 0) 
                { 
                    currentTarget = enemyWithInRange[0];
                    OnNewTargetArrive?.Invoke(currentTarget);
                }
                else
                {
                    currentTarget = null;
                    OnNoTargets?.Invoke();
                }
            }
        }
    }
    private void AimWeapon()
    {
        if (enemyWithInRange.Count > 0)
        {
            target = enemyWithInRange[0];
            weapon.transform.LookAt(target.GetTargetPosition());
        }
    }
}
