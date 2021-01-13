using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject testPrefab;

    public MergeData humanData;
    public MergeData natureData;
    public MergeData waterData;

    public void SpawnTile(MergeData data)
    {
        Tile spawnedTile = Instantiate(testPrefab, Vector3.zero, Quaternion.identity).GetComponent<Tile>();
        spawnedTile.currentData = data;
        spawnedTile.UpdateVisuals();
    }
}
