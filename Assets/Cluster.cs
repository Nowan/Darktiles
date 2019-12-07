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
}
