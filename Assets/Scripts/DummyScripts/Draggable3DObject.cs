using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable3DObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    Coroutine dragRoutine;
  
    public void StartDrag()
    {
        mZCoord = Camera.main.WorldToScreenPoint(
            gameObject.transform.position).z;
        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        dragRoutine = StartCoroutine(DragRountine());
    }
    private IEnumerator DragRountine()
    {
        while (true)
        {
            transform.position = GetMouseAsWorldPoint() + mOffset;
            yield return null;
        }
    }
    private Vector3 GetMouseAsWorldPoint()
    { 
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(dragRoutine);
        }
    }
}
