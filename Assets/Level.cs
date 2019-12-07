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
        List<Tile> tiles = new List<Tile>();
        Func<Tile, bool> checkFunc = (tile) => {
            return tile.side == initialTile.side;
        };

        tiles.AddRange(TraverseVerticalTiles(new Vector2Int(initialTile.column, initialTile.row), checkFunc));
        tiles.AddRange(TraverseHorizontalTiles(new Vector2Int(initialTile.column, initialTile.row), new Vector2Int(1, 0), checkFunc));
        tiles.AddRange(TraverseHorizontalTiles(new Vector2Int(initialTile.column, initialTile.row), new Vector2Int(-1, 0), checkFunc));

        return tiles;
    }

    private List<Tile> TraverseHorizontalTiles(Vector2Int start, Vector2Int step, Func<Tile, bool> findFunc) {
        List<Tile> tiles = new List<Tile>();
        Vector2Int head = start + step;

        while (head.y >= 0 && head.y < this.tiles.GetLength(0) && head.x >= 0 && head.x < this.tiles.GetLength(1)) {
            Tile tile = this.tiles[head.y, head.x];
            if (findFunc(tile)) {
                tiles.Add(tile);
            }
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
        
        while (head.y >= 0 && head.y < this.tiles.GetLength(0) && head.x >= 0 && head.x < this.tiles.GetLength(1)) {
            Tile tile = this.tiles[head.y, head.x];
            if (findFunc(tile)) {
                tiles.Add(tile);
            }
            head += step;
        }
        return tiles;
    }
}