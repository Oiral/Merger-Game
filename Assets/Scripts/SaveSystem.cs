using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class GameData
{
    public int score;
    public List<TileSave> currentTiles = new List<TileSave>();
    public float timePlayed;

    public GameData(int scoreInt, List<TileSave> tilesSaves, float timePlayedF)
    {
        score = scoreInt;
        currentTiles = tilesSaves;
        timePlayed = timePlayedF;
    }

    public GameData()
    {
        score = 0;
        currentTiles = new List<TileSave>();
        timePlayed = Time.time;
    }
}

public static class SaveSystem
{
    public static GameData saveGameData = new GameData();

    public static void SaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        GameData data = saveGameData;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public static void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        saveGameData = data;

        Debug.Log(saveGameData.currentTiles.Count);
    }
}
