using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject levelContainer;
    private Level level;

    void Start() {
        level = levelContainer.GetComponent<Level>();
    }
    void Update()
    {
        // if (Input.touchCount > 0) 
        // Input.GetTouch(0).position
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit)) {
                Cluster cluster = level.GetCluster(hit.transform.gameObject.GetComponent<Tile>());
                
                foreach(List<Tile> wave in cluster.waves) {
                    foreach (Tile tile in wave) {
                        //tile.Swap();
                    }
                }
            }
        }
        
    }
}
