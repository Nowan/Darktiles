using System.IO;
using SimpleJSON;
using UnityEngine;
using System.Collections.Generic;
using System;
public class LevelExporter
{
    public static LevelExporter instance = new LevelExporter();

    private LevelExporter()
    {

    }
    public void Export(GameObject level, string path)
    {

        JSONObject exportData = new JSONObject();
        JSONArray batchesData = new JSONArray();

        foreach (Transform batch in level.transform)
        {
            JSONArray batchData = new JSONArray();

            BatchPlaneList batchPlaneList = new BatchPlaneList(batch);

            foreach (BatchPlane batchPlane in batchPlaneList)
            {
                Debug.Log(batchPlane.GetHashCode());
                foreach (Transform tile in batch)
                {
                    if (BatchPlane.FromTile(tile) == batchPlane)
                    {
                        batchPlane.Add(tile);
                        Debug.Log(tile.gameObject.name);
                    }
                }
            }

            /*
           foreach (Transform tile in batch)
           {
               // TODO: convert to multidimensional array
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
           }*/

            batchesData.Add(batchData);
        }

        exportData.Add("batches", batchesData);

        //File.WriteAllText(path, exportData.ToString());
    }

    private bool LieInSamePlane(Transform tileA, Transform tileB)
    {
        return LookInSameDirection(tileA, tileB) && HaveSameAxisDepth(tileA, tileB);
    }

    private bool LookInSameDirection(Transform tileA, Transform tileB)
    {
        return tileA.forward == tileB.forward;
    }

    private bool HaveSameAxisDepth(Transform tileA, Transform tileB)
    {
        int axis = GetBaseAxis(tileA);
        return Mathf.Approximately(tileA.position[axis], tileB.position[axis]);
    }

    private int GetBaseAxis(Transform tile)
    {
        for (int axis = 0; axis < 3; axis++)
        {
            if (Mathf.Approximately(tile.forward[axis], 1.0f)) return axis;
        }

        throw new Exception("No base axis found");
    }

    private float GetAxisDepth(Transform tile, int axis)
    {
        return tile.position[axis];
    }
}

public class BatchPlaneList : List<BatchPlane>
{
    public BatchPlaneList(Transform batch)
    {
        foreach (Transform tile in batch)
        {
            AddUnique(BatchPlane.FromTile(tile));
        }
    }

    public void AddUnique(BatchPlane batchPlane)
    {
        if (!Contains(batchPlane)) Add(batchPlane);
    }

    public new bool Contains(BatchPlane batchPlane)
    {
        return base.Contains(batchPlane) || ContainsSimilar(batchPlane);
    }

    private bool ContainsSimilar(BatchPlane batchPlane)
    {
        foreach (BatchPlane comparand in this)
        {
            if (batchPlane == comparand) return true;
        }

        return false;
    }
}

public class BatchPlane : List<Transform>
{
    public Vector3 normal;
    public float baseAxisDepth;

    public BatchPlane(Vector3 normal, float baseAxisDepth)
    {
        this.normal = normal;
        this.baseAxisDepth = baseAxisDepth;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static BatchPlane FromTile(Transform tile)
    {
        int baseAxis = 0;
        for (int axis = 0; axis < 3; axis++)
        {
            if (Mathf.Approximately(tile.forward[axis], 1.0f)) { baseAxis = axis; break; }
        }

        return new BatchPlane(tile.forward, tile.position[baseAxis]);
    }

    public static bool operator ==(BatchPlane batchPlaneA, BatchPlane batchPlaneB)
    {
        return batchPlaneA.normal == batchPlaneB.normal && Mathf.Approximately(batchPlaneA.baseAxisDepth, batchPlaneB.baseAxisDepth);
    }

    public static bool operator !=(BatchPlane batchPlaneA, BatchPlane batchPlaneB)
    {
        return !(batchPlaneA.normal == batchPlaneB.normal && Mathf.Approximately(batchPlaneA.baseAxisDepth, batchPlaneB.baseAxisDepth));
    }
}