using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] float speed = 5f;
    [SerializeField] Enemy enemy;
    private void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(PrintWaypointName());
    }
    private void FindPath()
    {
        path.Clear();
        GameObject pathParentObj = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in pathParentObj.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if (waypoint != null)
                path.Add(waypoint);
        }
    }
    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }
    
    private void FinishPath()
    {
        this.gameObject.SetActive(false);
    }
    IEnumerator PrintWaypointName()
    {
        Vector3 startPos;
        Vector3 endPos;
        float lerp = 0;
        foreach (Waypoint waypoint in path)
        {
            lerp = 0;
            startPos = this.transform.position;
            endPos = waypoint.transform.position;
            transform.LookAt(endPos);
            while (lerp < 1)
            {
                lerp += Time.deltaTime * speed;
                this.transform.position = Vector3.Lerp(startPos, endPos, lerp);
                yield return null;
            }
        }
        enemy.TargetReachedEnd();
        FinishPath();
        //  Destroy(gameObject);
    }
}
