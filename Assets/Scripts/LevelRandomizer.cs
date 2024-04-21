using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Resposible for Randomize the level
/// </summary>
public class LevelRandomizer : Singleton<LevelRandomizer>
{

    [SerializeField] List<GridCellSettings> _availableGridCellSettings = new List<GridCellSettings>();


    System.Random random = new System.Random();


    /// <summary>
    /// Level Randomizer, It fetches random Card Grid Layout 
    /// </summary>
    /// <returns>GridCellSettings</returns>
    public GridCellSettings GetRandomLevel()
    {
        return _availableGridCellSettings[random.Next(0, _availableGridCellSettings.Count)];
    }

}
