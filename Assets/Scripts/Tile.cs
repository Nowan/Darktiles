using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{

    public TileType type;
    public TileSide side = TileSide.Dark;

    private void Start()
    {
        Invalidate();
    }

    private void OnValidate()
    {
        Invalidate();
    }

    private void Invalidate()
    {
        Transform darkSide = transform.Find("DarkSide");
        Transform lightSide = transform.Find("LightSide");

        darkSide.GetComponent<MeshRenderer>().material = type.darkMaterial;
        lightSide.GetComponent<MeshRenderer>().material = type.lightMaterial;

        darkSide.localEulerAngles = new Vector3(side == TileSide.Dark ? 180 : 0, 0, 0);
        lightSide.localEulerAngles = new Vector3(side == TileSide.Light ? 180 : 0, 0, 0);
    }
}
