using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSaver : MonoBehaviour
{
    public SaveData currentData;
    public void SaveGame(SaveData currentData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/MySaveData.dat");
        SaveData data = new SaveData();
        data = currentData;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    public SaveData LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            Debug.Log("Game data loaded!");
            return data;
        }
        else
        {
            Debug.LogError("There is no save data!");
            return null;
        }
    }
    void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/MySaveData.dat");
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }
}

[Serializable]
public class SaveData
{
    public World[] worlds = new World[3];
}
[Serializable]
public class World
{
    public int[,] tileMatrix;
    public int[,] objectMatrix;
}
