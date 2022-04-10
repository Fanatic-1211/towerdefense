using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] List<TileObject> tilesOnScene;

    public void HighLightTile(TileObject tile,bool highlight)
    {
        tile.HighlightCell(highlight);
    }
}
