using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public List<PlaneData> planes;
}

[System.Serializable]
public class PlaneData 
{
    public int id;
    public int width;
    public int height;
    public List<TileData> tiles;
}

[System.Serializable]
public class TileData
{
    public int id;
    public int row;
    public int column;
    public string type;
    public string side;
}