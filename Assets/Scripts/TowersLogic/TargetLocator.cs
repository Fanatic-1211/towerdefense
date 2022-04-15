using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform target;
    [SerializeField] ParticleSystem shotParticle;
    [SerializeField] Enemy debugTarget;
    private List<Enemy> enemyWithInRange = new List<Enemy>();

    float radius = 15f;
    
    private void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }
   
    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }
    private void AimWeapon()
    {
        if (target != null)
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);
            Attack(targetDistance < radius);

                weapon.transform.LookAt(target);
        }
    }
    private void Attack(bool isActive)
    {
        var emmision = shotParticle.emission;
        emmision.enabled = isActive;
    }
}
