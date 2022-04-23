using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Environment.Tile;
using System.Linq;

namespace Game.Environment.Tile
{
    public enum NeighborCellPosition
    {
        north,
        west,
        south,
        east
    }
    public class PathTileData
    {
        public NeighborCellPosition PreviousNeighborSide { get; private set; }
        public NeighborCellPosition NextNeighborSide { get; private set; }
        public TileObject ThisTileObject { get; private set; }

        public PathTileData( TileObject thisTileObject,NeighborCellPosition previousNeighborSide, NeighborCellPosition nextNeighborSide)
        {
            PreviousNeighborSide = previousNeighborSide;
            NextNeighborSide = nextNeighborSide;
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
            TileObject nextObject= Extensions.GetClosestOnbject(start, removePathList);
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
            if(offsetPositon.x> offsetPositon.y)
            {
                if (offsetPositon.x > 0)
                    return NeighborCellPosition.east;
                else
                    return NeighborCellPosition.south;
            }
            else
            {
                if (offsetPositon.y > 0)
                    return NeighborCellPosition.north;
                else
                    return NeighborCellPosition.west;
            }
        }

    }
}