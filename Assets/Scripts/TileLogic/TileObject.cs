using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour, ITowerPlaceable
{
    [SerializeField] SpriteRenderer addRenderer;
    private GameObject occupation;
    public void SetOccupation(GameObject occupation)
    {
        this.occupation = occupation;
    }
    public bool IsCellAvalable() => occupation == null;
    public T PlaceTower<T>(T towerPrefab) where T : MonoBehaviour
    { 
        if (!IsCellAvalable())
            return null;
        T obj = Instantiate(towerPrefab,this.transform.position, Quaternion.identity, this.transform);
        occupation = obj.gameObject;
        return obj;
    }

    public void HighlightPlace(bool highLight)
    {
        addRenderer.color = IsCellAvalable() ? Color.white : Color.red;
        addRenderer.gameObject.SetActive(highLight);
    }

    public void ClearCell()
    {
        if (occupation != null)
            Destroy(occupation);
    }
}
