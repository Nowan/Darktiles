using System.IO;
using SimpleJSON;
using UnityEngine;

public class LevelImporter : MonoBehaviour
{
    public static void Import(GameObject level, string path)
    {
        foreach (Transform child in level.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        LevelGenerator generator = level.GetComponent<LevelGenerator>();
        JSONObject importData = (JSONObject)JSON.Parse(File.ReadAllText(path));

        generator.Run(importData);
    }
}
