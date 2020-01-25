using UnityEngine;
using UnityEditor;

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
        return "";//EditorUtility.SaveFilePanel("Save level as JSON", "Assets/Levels/", "000.json", "json");
    }

    private void ImportData(GameObject level, string path)
    {
        if (path == "") return;
        LevelImporter.Import(level, path);
    }

    private void ExportData(GameObject level, string path) {
        //if (path == "") return;
        LevelExporter.instance.Export(level, path);
    }
}