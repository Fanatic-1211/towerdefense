using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHp = 10;
    [SerializeField] int currentHp = 0;
    [SerializeField] Enemy enemy;
    // Start is called before the first frame update
    void OnEnable()
    {
        currentHp = maxHp;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }
    private void ProcessHit()
    {
        currentHp--;
        Debug.Log(currentHp);
        if (currentHp <= 0) 
        {
            enemy.TargetDied();
            this.gameObject.SetActive(false);
        }
    }
}
