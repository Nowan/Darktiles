using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class LevelGenerator : MonoBehaviour
{
    public Object tilePrefab;

    void Start() {
        
    }

    public void Run(JSONNode levelData) {
        foreach (JSONArray batchData in levelData["batches"].AsArray)
        {
            GameObject batch = new GameObject("Batch");
            batch.transform.parent = transform;

            foreach (JSONObject tileData in batchData)
            {
                GameObject tile = (GameObject)Instantiate(tilePrefab, batch.transform);
                Tile tileComponent = tile.GetComponent<Tile>();

                JSONObject positionData = tileData["position"].AsObject;
                tile.transform.position = new Vector3(positionData["x"].AsFloat, positionData["y"].AsFloat, positionData["z"].AsFloat);

                JSONObject rotationData = tileData["rotation"].AsObject;
                tile.transform.eulerAngles = new Vector3(rotationData["x"].AsFloat, rotationData["y"].AsFloat, rotationData["z"].AsFloat);

                tileComponent.side = (TileSide)tileData["side"].AsInt;
            }
        }
    }
}
