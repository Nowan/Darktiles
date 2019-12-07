using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public TextAsset levelDataJSON;
    public GameObject regularTile;
    private LevelData levelData;
    

    private void Awake() {
        levelData = JsonUtility.FromJson<LevelData>(levelDataJSON.ToString());

        foreach(TileData tileData in levelData.planes[0].tiles) {
            GameObject tile = Object.Instantiate(regularTile, this.gameObject.transform);
            tile.transform.position = new Vector3(tileData.column * 1.05f, 0, tileData.row * 1.05f);
        }
    }
}
