using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Game.Environment.Tile
{
    public enum TileType
    {
        placeable,
        path,
        locked,
        spawn,
        finish
    }
    public class TileManager : MonoBehaviour
    {

        [SerializeField] List<TileObject> tilesOnScene;

        PathBilder pathBuilder = new PathBilder();


        public void HighLightTile(ITowerPlaceable tile, bool highlight)
        {
            tile.HighlightPlace(highlight);
        }
        public void RebuildPath()
        {
            List<TileObject> pathTiles = tilesOnScene.Where(t => t.TileType == TileType.path || t.TileType == TileType.finish || t.TileType == TileType.spawn).ToList();
            List<PathTileData> pathTileDatas = pathBuilder.BuildPath(pathTiles);

            for (int i = 0; i < pathTileDatas.Count - 1; i++)
            {
                Debug.DrawLine(pathTileDatas[i].ThisTileObject.transform.position, pathTileDatas[i + 1].ThisTileObject.transform.position, Color.red, 5f);
            }
        }
    }
}