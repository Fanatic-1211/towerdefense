using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectileArrow : MonoBehaviour, ITowerProjectile
{
    [SerializeField] int damage=1;
    [SerializeField] float startSpeed = 1;
    [SerializeField] float maxLifeTime = 5f;
    [SerializeField] int durability = 2;
    public Action<IDamageable> onCollision;
    public Action onDestroyed;
    Vector3 moveVector = Vector3.forward;
    private IEnumerator MoveRoutone()
    {
        float lifeTime=0;
        while (true)
        {
            transform.Translate(moveVector * Time.deltaTime*startSpeed,Space.World);
            Debug.DrawRay(this.transform.position, moveVector);
            lifeTime += Time.deltaTime;
            if (lifeTime > maxLifeTime)
                DestroyProjectile();
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.DealDamage(damage);
            onCollision?.Invoke(damageable);
            durability--;
            if (durability <= 0)
                DestroyProjectile();
        }
    }
 
    private void Awake()
    {
        StartCoroutine(MoveRoutone());
    }
    public ITowerProjectile CreateProjectile(Vector3 from,ITargetable targetable)
    {
   
        SimpleProjectileArrow arrow = Instantiate(this,from,Quaternion.identity, null);
        Vector3 moveVector =  targetable.GetTargetPosition() - arrow.transform.position;
        arrow.transform.LookAt(targetable.GetTargetPosition());
        
        arrow.moveVector = moveVector;
      
        return arrow;
    }

    public void DestroyProjectile(bool silent = false)
    {
        if (!silent) onDestroyed?.Invoke();
        Destroy(this.gameObject);
    }
}
