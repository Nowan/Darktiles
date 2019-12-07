using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileSide side = TileSide.Light;

    public void Swap() {
        this.gameObject.transform.Rotate(new Vector3(0, 0, 180));
    }
}
