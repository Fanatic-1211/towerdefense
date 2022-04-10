using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer addRenderer;

    private GameObject occupation;
    public void SetOccupation(GameObject occupation)
    {
        this.occupation = occupation;
    }
    public bool IsCellAwalable() => occupation == null;
    public void HighlightCell(bool highLight)
    {
        addRenderer.gameObject.SetActive(highLight);
    }

}
