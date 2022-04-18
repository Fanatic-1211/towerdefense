using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeapon : MonoBehaviour, IWeaponSystem
{
    [SerializeField] float roundsPerSecond = 2f;
    [SerializeField] SimpleProjectileArrow projectileArrow;
    [SerializeField] Transform emitFrom;
    float cooldown = 0;//' 1 / roundsPerSecond;
    Coroutine targetRoutine;
    List<ITowerProjectile> projectiles = new List<ITowerProjectile>();

    private void Awake()
    {
        cooldown = 0;
    }

    public void SetNewTarget(ITargetable targetable)
    {
        StopRoutine();
        targetRoutine = StartCoroutine(ShootingRoutine(targetable));
    }
    private void StopRoutine()
    {
        if (targetRoutine != null)
            StopCoroutine(targetRoutine);
    }
    public void ClearTargets()
    {
        StopRoutine();
    }
    private IEnumerator ShootingRoutine(ITargetable target)
    {
        while (true)
        {
            if (cooldown <= 0) { 
                Shoot(target);
                cooldown = 1 / roundsPerSecond;
            }
            yield return null;
        }

    }
    private void Shoot(ITargetable target)
    {
        ITowerProjectile projectile = projectileArrow.CreateProjectile(emitFrom.position, target);
        //   projectiles.Add(projectile);

    }
    private void ProjectileDead(ITowerProjectile projectile)
    {
        //  projectiles.Remove(projectile);
    }
    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

}
