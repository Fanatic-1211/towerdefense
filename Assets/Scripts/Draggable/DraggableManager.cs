using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Environment.Tile;

public class DraggableManager : MonoBehaviour
{
    [SerializeField] DraggableObject draggablePrefab;
    [SerializeField] TileManager tileManager;
    private event Action<ITowerPlaceable> OnDragEnded;
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
    private void CurrentDragable_OnTileExit(ITowerPlaceable obj)
    {
        tileManager.HighLightTile(obj, false);
    }

    private void CurrentDragable_OnNewTileEntered(ITowerPlaceable obj)
    {
        tileManager.HighLightTile(obj, true);
    }

    private void CurrentDragable_OnDragEnded(ITowerPlaceable obj)
    {
        if (obj != null) tileManager.HighLightTile(obj, false);
        currentDragable.gameObject.SetActive(false);
        OnDragEnded?.Invoke(obj);
    }

    public void StartDragging(GameObject draggableViewPrefab, Action<ITowerPlaceable> onTowerDroppedCallback)
    {
        OnDragEnded = onTowerDroppedCallback;
        currentDragable.gameObject.SetActive(true);
        currentDragable.SetUp(draggableViewPrefab);
    }
}
