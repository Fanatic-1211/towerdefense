using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Environment.Tile
{

    public struct TileSkinData
    {
        public TileSkinData(Mesh mesh, float rotation = 0)
        {
            Mesh = mesh;
            Rotation = rotation;
        }

        public Mesh Mesh { get; private set; }
        public float Rotation { get; private set; }

    }
    public class TileSkinDataManager : MonoBehaviour
    {

        [SerializeField] Mesh straightRoad;
        [SerializeField] Mesh angledRoad;
        [SerializeField] Mesh placeableTile;
        [SerializeField] Mesh lockedTile;

        public TileSkinData GetTileMesh(TileObject tile)
        {
            switch (tile.TileType)
            {
                case TileType.finish:
                    {
                        return new TileSkinData(straightRoad);
                    }
                case TileType.path:
                    {
                        return new TileSkinData(straightRoad);
                    }
                case TileType.spawn:
                    {
                        return new TileSkinData(straightRoad);
                    }
                case TileType.placeable:
                    {
                        return new TileSkinData(placeableTile);
                    }
                case TileType.locked:
                    {
                        return new TileSkinData(lockedTile);
                    }
            }
            Debug.LogWarning($"Can't find sutable tile");
            return new TileSkinData(lockedTile);
        }
        public TileSkinData GetTileMesh(PathTileData pathTileData)
        {
            if (pathTileData.NeighborCellPosition== (NeighborCellPosition.north | NeighborCellPosition.south) )
            {
                return new TileSkinData(straightRoad);
            }
            if (pathTileData.NeighborCellPosition == (NeighborCellPosition.east | NeighborCellPosition.west))
            {
                return new TileSkinData(straightRoad,90);
            }
            if (pathTileData.NeighborCellPosition == (NeighborCellPosition.north | NeighborCellPosition.east))
            {
                return new TileSkinData(angledRoad, 0);
            }
            if (pathTileData.NeighborCellPosition == (NeighborCellPosition.east | NeighborCellPosition.south))
            {
                return new TileSkinData(angledRoad, 90);
            }
            if (pathTileData.NeighborCellPosition == (NeighborCellPosition.south | NeighborCellPosition.west))
            {
                return new TileSkinData(angledRoad, 180);
            }
            if (pathTileData.NeighborCellPosition == (NeighborCellPosition.north | NeighborCellPosition.west))
            {
                return new TileSkinData(angledRoad, 270);
            }
            Debug.LogWarning($"Can't find sutable road");
            return new TileSkinData(lockedTile, 0);
        }
    }
}