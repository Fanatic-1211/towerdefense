using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DraggableObject : MonoBehaviour
{
    GameObject draggableVisible;
    [SerializeField]float mZCoord = 18f;
    [SerializeField] float yOffset = 5f;
    string cameraRaycastIgnoreLayer = "TilePlaceable";
    string draggableRaycastIgnoreLayer = "CameraRaycast";
    ITowerPlaceable currentTile = null;
    public event Action<ITowerPlaceable> OnNewTileEntered;
    public event Action<ITowerPlaceable> OnTileExit;
    public event Action<ITowerPlaceable> OnDragEnded;
    int cameraIgnore=0;
    int draggableIgnore=0;

    private void Awake()
    {
        cameraIgnore = LayerMask.GetMask(draggableRaycastIgnoreLayer);
        draggableIgnore = LayerMask.GetMask(cameraRaycastIgnoreLayer);
    }
    RaycastHit hit;
    public void SetUp(GameObject draggableVisiblePrefab)
    {
        if (draggableVisible != null)
            Destroy(draggableVisible);
        draggableVisible = Instantiate(draggableVisiblePrefab,this.transform.position, Quaternion.identity, this.transform);
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down*100f, Color.red);
        ITowerPlaceable newTile = null;
        if (Physics.Raycast(this.transform.position, Vector3.down, out hit, 1000f, draggableIgnore)) 
            newTile = hit.collider.gameObject.GetComponent<ITowerPlaceable>();
        if(newTile!=currentTile)
        {
            if (currentTile != null) LeaveTile(currentTile);
            if (newTile != null) EnterTile(newTile);
            currentTile = newTile;
        }
        if (Input.GetMouseButtonUp(0)) OnDragEnded?.Invoke(currentTile);
        transform.position = FindPanelByRaycast();

    }
    private Vector3 FindPanelByRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000f, cameraIgnore))
        {
            return hit.point;
        }
        return transform.position;

    }
    private void LeaveTile(ITowerPlaceable tile)
    {
        OnTileExit?.Invoke(tile);
    }
    private void EnterTile(ITowerPlaceable tile)
    {
        OnNewTileEntered?.Invoke(tile);
    }
    private IEnumerator DragRountine()
    {
        while (true)
        {
          
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
        Vector3 world = Camera.main.ScreenToWorldPoint(mousePoint);
      
        return world;
    }
}
