using UnityEngine;
using UnityEditor;
using System.IO;
using SimpleJSON;

public class LevelEditor : EditorWindow
{

    [MenuItem("Window/Level Editor")]
    static void Init()
    {
        LevelEditor window = GetWindow<LevelEditor>();
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        if (GUILayout.Button("Import"))
        {
            ImportData(GameObject.Find("Level"), RequestImportPath());
        }

        if (GUILayout.Button("Export")) {
            ExportData(GameObject.Find("Level"), RequestExportPath());
        }

        EditorGUILayout.EndVertical();
    }

    private string RequestImportPath() {
        return EditorUtility.OpenFilePanel("Import level from JSON", "Assets/Levels/", "json");
    }

    private string RequestExportPath() {
        return EditorUtility.SaveFilePanel("Save level as JSON", "Assets/Levels/", "000.json", "json");
    }

    private void ImportData(GameObject level, string path)
    {
        if (path == "") return;

        foreach (Transform child in level.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        LevelGenerator generator = level.GetComponent<LevelGenerator>();
        JSONObject importData = (JSONObject)JSON.Parse(File.ReadAllText(path));

        generator.Run(importData);
    }

    private void ExportData(GameObject level, string path) {
        if (path == "") return;

        JSONObject exportData = new JSONObject();
        JSONArray batchesData = new JSONArray();

        for (int i = 0; i < level.transform.childCount; i++)
        {
            Transform batch = level.transform.GetChild(i);
            JSONArray batchData = new JSONArray();

            foreach (Transform tile in batch)
            {
                JSONObject tileData = new JSONObject();
                Tile tileComponent = tile.GetComponent<Tile>();

                tileData.Add("side", new JSONNumber((int)tileComponent.side));
                tileData.Add("type", new JSONNumber(tileComponent.type.GetInstanceID()));

                JSONObject positionData = new JSONObject();
                positionData.Add("x", new JSONNumber(tile.position.x));
                positionData.Add("y", new JSONNumber(tile.position.y));
                positionData.Add("z", new JSONNumber(tile.position.z));
                tileData.Add("position", positionData);

                JSONObject rotationData = new JSONObject();
                rotationData.Add("x", new JSONNumber(tile.eulerAngles.x));
                rotationData.Add("y", new JSONNumber(tile.eulerAngles.y));
                rotationData.Add("z", new JSONNumber(tile.eulerAngles.z));
                tileData.Add("rotation", rotationData);

                batchData.Add(tileData);
            }

            batchesData.Add(batchData);
        }

        exportData.Add("batches", batchesData);

        File.WriteAllText(path, exportData.ToString());
    }
}