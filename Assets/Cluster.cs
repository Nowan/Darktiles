using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluster
{
    public List<List<Tile>> waves = new List<List<Tile>>();

    public Cluster(Tile initialTile) {
        List<Tile> initialWave = new List<Tile>();
        initialWave.Add(initialTile);
        this.waves.Add(initialWave);
    }

    public Cluster(Tile initialTile, List<Tile> clusterTiles){
        List<Tile> initialWave = new List<Tile>();
        initialWave.Add(initialTile);
        this.waves.Add(initialWave);
        
        this.waves.Add(clusterTiles);
    }
}
