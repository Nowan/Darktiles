using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public TextAsset levelDataJSON;
    public GameObject regularTile;
    public GameObject [,] tiles;
    private LevelData levelData;

    public Cluster GetCluster(Tile tile) {
        Cluster cluster = new Cluster(tile);
        
        return cluster;
    }

    private void Awake() {
        levelData = JsonUtility.FromJson<LevelData>(levelDataJSON.ToString());
        PlaneData planeData = levelData.planes[0];
        tiles = new GameObject[planeData.height, planeData.width];

        foreach(TileData tileData in planeData.tiles) {
            GameObject tile = Object.Instantiate(regularTile, this.gameObject.transform);
            tile.transform.position = new Vector3(tileData.column * 1.05f, 0, tileData.row * 1.05f);
            this.tiles[tileData.row, tileData.column] = tile;
        }
    }
}