using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Environment.Tile;
using System.Linq;

namespace Game.Environment.Tile
{
    [System.Flags]
    public enum NeighborCellPosition
    {
        none = 0,
        north = 1,
        west = 2,
        south = 4,
        east = 8
    }
    public class PathTileData
    {
        public NeighborCellPosition NeighborCellPosition { get; private set; }

        public TileObject ThisTileObject { get; private set; }

        public PathTileData(TileObject thisTileObject, NeighborCellPosition previousNeighborSide, NeighborCellPosition nextNeighborSide)
        {
            NeighborCellPosition |= previousNeighborSide;
            NeighborCellPosition |= nextNeighborSide;
            ThisTileObject = thisTileObject;
        }
    }
    public class PathBilder
    {
        public List<PathTileData> BuildPath(List<TileObject> pathlist)
        {
            List<PathTileData> output = new List<PathTileData>();
            List<TileObject> removePathList = new List<TileObject>();
            removePathList.AddRange(pathlist);
            TileObject start = removePathList.FirstOrDefault(p => p.TileType == TileType.spawn);
            TileObject end = removePathList.FirstOrDefault(p => p.TileType == TileType.finish);
            removePathList.Remove(start);
            removePathList.Remove(end);
            TileObject nextObject = Extensions.GetClosestOnbject(start, removePathList);
            output.Add(new PathTileData(start, NeighborCellPosition.west, GetSide(start, nextObject)));
            TileObject currentTile = nextObject;
            TileObject prevoious = start;
            removePathList.Remove(currentTile);
            while (removePathList.Count > 0)
            {
                nextObject = Extensions.GetClosestOnbject(currentTile, removePathList);
                output.Add(CreatePathData(prevoious, currentTile, nextObject));
                prevoious = currentTile;
                currentTile = nextObject;
                removePathList.Remove(currentTile);
            }
            output.Add(CreatePathData(prevoious, currentTile, end));
            output.Add(new PathTileData(end, GetSide(end, currentTile),NeighborCellPosition.east));
            return output;
        }
        private PathTileData CreatePathData(TileObject previousTile, TileObject currentTile, TileObject nextTile)
        {
            NeighborCellPosition toPrev = GetSide(currentTile, previousTile);
            NeighborCellPosition toNext = GetSide(currentTile, nextTile);
            return new PathTileData(currentTile, toPrev, toNext);
        }
        private NeighborCellPosition GetSide(TileObject fromTile, TileObject toTile)
        {
            Vector3 offsetPositon = toTile.transform.position - fromTile.transform.position;
            if (offsetPositon.x > offsetPositon.z)
            {
                if (offsetPositon.x > Mathf.Abs(offsetPositon.z/2))
                    return NeighborCellPosition.east;
                else
                    return NeighborCellPosition.south;
            }
            else
            {
                if (offsetPositon.z > Mathf.Abs(offsetPositon.x/2))
                    return NeighborCellPosition.north;
                else
                    return NeighborCellPosition.west;
            }
        }

    }
}