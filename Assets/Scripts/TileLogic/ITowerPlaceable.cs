using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerPlaceable
{
    public bool IsCellAvalable();
    public T PlaceTower<T>(T towerPrefab) where T : MonoBehaviour;
    public void HighlightPlace(bool highLight);
    public void ClearCell();
}
