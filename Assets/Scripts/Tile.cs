using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public MergeData currentData;

    public GameObject visuals;

    public TileSave saveDataForTile;

    private void OnEnable()
    {
        UpdateVisuals();
    }

    public void LevelUpData()
    {
        MergeData newData =  MergeDataManager.instance.GetNextMerge(currentData);

        if (newData)
        {
            currentData = newData;
        }

        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        //If we have current data, Lets change the visuals
        if (currentData && currentData.mergeVisuals)
        {
            Destroy(visuals);
            visuals = Instantiate(currentData.mergeVisuals, transform);
        }
        SetSaveData();
    }

    public void SetSaveData()
    {
        if (SaveSystem.saveGameData.currentTiles.Contains(saveDataForTile))
        {
            SaveSystem.saveGameData.currentTiles.Remove(saveDataForTile);
        }

        saveDataForTile = new TileSave(transform.position, transform.rotation, currentData);

        SaveSystem.saveGameData.currentTiles.Add(saveDataForTile);

        SaveSystem.SaveFile();
    }

    private void OnDestroy()
    {
        if (SaveSystem.saveGameData.currentTiles.Contains(saveDataForTile))
        {
            SaveSystem.saveGameData.currentTiles.Remove(saveDataForTile);
        }
    }
}
