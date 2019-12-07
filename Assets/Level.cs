using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq.Expressions;

public class Level : MonoBehaviour
{
    public TextAsset levelDataJSON;
    public GameObject regularTile;
    public Tile [,] tiles;
    private LevelData levelData;

    public Cluster GetCluster(Tile tile) {
        Cluster cluster = new Cluster(tile, GetClusterTiles(tile));
        
        return cluster;
    }

    private void Awake() {
        levelData = JsonUtility.FromJson<LevelData>(levelDataJSON.ToString());
        PlaneData planeData = levelData.planes[0];
        tiles = new Tile[planeData.height, planeData.width];

        foreach(TileData tileData in planeData.tiles) {
            GameObject obj = UnityEngine.Object.Instantiate(regularTile, this.gameObject.transform);
            Tile tile = obj.GetComponent<Tile>();
            tile.Init(tileData);
            obj.transform.position = new Vector3(tileData.column * 1.05f, 0, tileData.row * 1.05f);
            this.tiles[tileData.row, tileData.column] = tile;
        }
    }

    private List<Tile> GetClusterTiles(Tile initialTile) {
        List<Tile> clusterTiles = new List<Tile>();
        Func<Tile, bool> checkFunc = (tile) => {
            return tile.side == initialTile.side;
        };
        
        List <Vector2Int> checkQueue = new List<Vector2Int>();
        List <Vector2Int> checkedQueue = new List<Vector2Int>();

        checkQueue.AddRange(GetNeighboringPoints(new Vector2Int(initialTile.column, initialTile.row)));

        while (checkQueue.Count > 0) {
            Vector2Int point = checkQueue[0];
            
            if (IsInBounds(point)) {
                Tile tile = this.tiles[point.y, point.x];
                if (tile.side == initialTile.side) {
                    clusterTiles.Add(tile);

                    foreach(Vector2Int neighborPoint in GetNeighboringPoints(point)) {
                        Debug.Log(neighborPoint);
                        bool hasBeenChecked = checkedQueue.Exists((pt) => {
                            return pt.x == neighborPoint.x && pt.y == neighborPoint.y;
                        });

                        if (!hasBeenChecked) {
                            checkQueue.Add(neighborPoint);
                        }
                    }
                }
                
            }
            checkedQueue.Add(point);
            checkQueue.Remove(point);
        }

        /*

        tiles.AddRange(TraverseVerticalTiles(new Vector2Int(initialTile.column, initialTile.row), checkFunc));
        tiles.AddRange(TraverseHorizontalTiles(new Vector2Int(initialTile.column, initialTile.row), new Vector2Int(1, 0), checkFunc));
        tiles.AddRange(TraverseHorizontalTiles(new Vector2Int(initialTile.column, initialTile.row), new Vector2Int(-1, 0), checkFunc));
        */
        return clusterTiles;
    }

    private bool IsInBounds(Vector2Int point) {
        return point.y >= 0 && point.y < this.tiles.GetLength(0) && point.x >= 0 && point.x < this.tiles.GetLength(1);
    }

    private Vector2Int[] GetNeighboringPoints(Vector2Int point) {
        return new Vector2Int[4] {
            new Vector2Int(point.x - 1, point.y),
            new Vector2Int(point.x + 1, point.y),
            new Vector2Int(point.x, point.y + 1),
            new Vector2Int(point.x, point.y - 1)
        };
    }

    private List<Tile> TraverseHorizontalTiles(Vector2Int start, Vector2Int step, Func<Tile, bool> findFunc) {
        List<Tile> tiles = new List<Tile>();
        Vector2Int head = start + step;

        while (IsInBounds(head) && findFunc(this.tiles[head.y, head.x])) {
            tiles.Add(this.tiles[head.y, head.x]);
            tiles.AddRange(TraverseVerticalTiles(head, findFunc));

            head += step;
        }
        
        return tiles;
    }

    private List<Tile> TraverseVerticalTiles(Vector2Int start, Func<Tile, bool> findFunc) {
        List<Tile> tiles = new List<Tile>();
        tiles.AddRange(TraverseTiles(start, new Vector2Int(0, 1), findFunc));
        tiles.AddRange(TraverseTiles(start, new Vector2Int(0, -1), findFunc));
        return tiles;
    }

    private List<Tile> TraverseTiles(Vector2Int start, Vector2Int step, Func<Tile, bool> findFunc) {
        List<Tile> tiles = new List<Tile>();
        Vector2Int head = start + step;
        
        while (IsInBounds(head) && findFunc(this.tiles[head.y, head.x])) {
            Tile tile = this.tiles[head.y, head.x];
            if (findFunc(tile)) {
                tiles.Add(tile);
            }
            head += step;
        }
        return tiles;
    }
}