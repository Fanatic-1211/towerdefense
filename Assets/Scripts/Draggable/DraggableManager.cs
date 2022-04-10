using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DraggableManager : MonoBehaviour
{
    [SerializeField] DraggableObject draggablePrefab;
    [SerializeField] TileManager tileManager;
    private event Action<TileObject> OnDragEnded;
    DraggableObject currentDragable;
    private bool isDragging = false;
    private void Awake()
    {
        currentDragable = Instantiate(draggablePrefab);
        currentDragable.gameObject.SetActive(false);
        currentDragable.OnDragEnded += CurrentDragable_OnDragEnded;
        currentDragable.OnNewTileEntered += CurrentDragable_OnNewTileEntered;
        currentDragable.OnTileExit += CurrentDragable_OnTileExit;
        isDragging = false;
    }
    

    private void CurrentDragable_OnTileExit(TileObject obj)
    {
        tileManager.HighLightTile(obj, false);
    }

    private void CurrentDragable_OnNewTileEntered(TileObject obj)
    {
        tileManager.HighLightTile(obj, true);
    }

    private void CurrentDragable_OnDragEnded(TileObject obj)
    {
        OnDragEnded?.Invoke(obj);
        currentDragable.gameObject.SetActive(false);
    }

    public Action<TileObject> StartDragging(GameObject draggableViewPrefab)
    {
        OnDragEnded = null;
        currentDragable.gameObject.SetActive(true);
        currentDragable.SetUp(draggableViewPrefab);
        return OnDragEnded;
    }

}
