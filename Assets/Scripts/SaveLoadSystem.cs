using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
/// <summary>
/// Saving and Loading Data System.Responsible for saving/loading serialized data into encrypted format.
/// </summary>
public class SaveLoadSystem : MonoBehaviour
{

    static string fileSavePath = Application.persistentDataPath;

    /// <summary>
    /// Saving Data into a File
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="saveData"></param>
    public static void SaveGameHistoryData(string fileName,GameProgressSaveData saveData)
    {

        if (saveData == null)
        {
            return;
        }


        BinaryFormatter formatter = new BinaryFormatter();

        var directoryPath = Path.Combine(fileSavePath, "SaveGame");

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);


        var savePath = Path.Combine(directoryPath, $"{fileName}.dat");

        var file = new FileStream(savePath, FileMode.OpenOrCreate);
        formatter.Serialize(file, saveData);

        file.Close();

    }

    /// <summary>
    /// Loading data from saved file
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static GameProgressSaveData LoadGameHistoryData(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return null;

        var loadPath = Path.Combine(Path.Combine(fileSavePath, "SaveGame"), $"{fileName}.dat");

        if (!File.Exists(loadPath))
            return null;

        BinaryFormatter formatter = new BinaryFormatter();
        var file = File.Open(loadPath, FileMode.Open);

        var data =  formatter.Deserialize(file) as GameProgressSaveData;

        file.Close();

        return data;

    }


}

/// <summary>
/// Progress Saved Data Class
/// </summary>
[Serializable]
public class GameProgressSaveData
{
    public List<GameHistoryData> _saveHistoryData = new List<GameHistoryData>();
}


/// <summary>
/// History Save Data Storage Class
/// </summary>
[Serializable]
public class GameHistoryData
{
    public string _savedCardGridLayout;
    public string _savedMatches;
    public string _savedTurns;
    public string _savedTimeStamp;
}