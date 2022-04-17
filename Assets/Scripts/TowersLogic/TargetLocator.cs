using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    //[SerializeField] Transform target;
    [SerializeField] ParticleSystem shotParticle;
    ITargetable target;
    private List<ITargetable> enemyWithInRange = new List<ITargetable>();

    float radius = 15f;

    private void Update()
    {
        //  FindClosestTarget();
        AimWeapon();
    }

    private void OnTriggerEnter(Collider other)
    {
        ITargetable target = other.GetComponent<ITargetable>();
        if (target != null)
        {
            enemyWithInRange.Add(target);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ITargetable target = other.GetComponent<ITargetable>();
        if (target != null)
        {
            enemyWithInRange.Remove(target);
        }
    }
    /* private void FindClosestTarget()
     {
         ITargetable[] enemies = FindObjectsOfType<ITargetable>();
         Transform closestTarget = null;
         float maxDistance = Mathf.Infinity;
         foreach (ITargetable enemy in enemies)
         {
             float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
             if (targetDistance < maxDistance)
             {
                 closestTarget = enemy.transform;
                 maxDistance = targetDistance;
             }
         }
         target = closestTarget;
     }*/
    private void AimWeapon()
    {
        if (enemyWithInRange.Count > 0)
        {
            target = enemyWithInRange[0];
            float targetDistance = Vector3.Distance(transform.position, target.GetTargetPosition());
            Attack(targetDistance < radius);

            weapon.transform.LookAt(target.GetTargetPosition());
        }
    }
    private void Attack(bool isActive)
    {
        var emmision = shotParticle.emission;
        emmision.enabled = isActive;
    }
}
