using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject level;
    void Update()
    {
        // if (Input.touchCount > 0) 
        // Input.GetTouch(0).position
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit)) {
                Tile tile = hit.transform.gameObject.GetComponent<Tile>();
                tile.Swap();
                //Debug.Log(tile);
            }
        }
        
    }
}
