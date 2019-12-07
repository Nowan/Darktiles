using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileSide side = TileSide.Light;
    public int row;
    public int column;

    public void Init(TileData tileData) {
        this.row = tileData.row;
        this.column = tileData.column;

        switch(tileData.side) {
            case "LIGHT": 
                this.side = TileSide.Light;
                break;
            case "DARK":
                this.side = TileSide.Dark;
                this.gameObject.transform.Rotate(new Vector3(0, 0, 180));
                break;
        }
    }

    public void Swap() {
        this.gameObject.transform.Rotate(new Vector3(0, 0, 180));
    }
}
