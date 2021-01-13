using UnityEngine;
using System.Collections;

public class SaveManager : MonoBehaviour
{
    public GameObject basicTilePrefab;

    private void Start()
    {
        SaveSystem.LoadFile();
        LoadLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveSystem.SaveFile();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveSystem.LoadFile();
            LoadLevel();
        }
    }

    void LoadLevel()
    {
        int count = SaveSystem.saveGameData.currentTiles.Count;
        for (int i = 0; i < count; i++)
        {
            TileSave tile = SaveSystem.saveGameData.currentTiles[i];

            GameObject spawnedPrefab = Instantiate(basicTilePrefab);

            spawnedPrefab.transform.position = tile.pos;
            spawnedPrefab.transform.rotation = tile.rotation;
            spawnedPrefab.GetComponent<Tile>().currentData = tile.data;

            spawnedPrefab.GetComponent<Tile>().UpdateVisuals();
        }
    }
}
