using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateTarget : MonoBehaviour
{
    [SerializeField] Vector3 moveVector;
    float startSpeed = 1;
    private IEnumerator MoveRoutone()
    {
        float lifeTime = 0;
        while (true)
        {
            transform.Translate(moveVector * Time.deltaTime * startSpeed);
            Debug.DrawRay(this.transform.position, moveVector);
            lifeTime += Time.deltaTime;
        
            yield return null;
        }
    }
    void Start()
    {
        StartCoroutine(MoveRoutone());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
