using TMPro;
using UnityEngine;

/// <summary>
/// Game Histoty Save Data Class, Responsible for storing all time history data and population.
/// </summary>
public class GameSaveHistoryData : MonoBehaviour
{
    string _savedMatches;
    public string SavedMatches
    {
        get
        {
            return _savedMatches;
        }
        set
        {
            _savedMatches = value;

            _matchesText.text = _savedMatches;
        }
    }


    string _savedTurns;
    public string SavedTurns
    {
        get
        {
            return _savedTurns;
        }
        set
        {
            _savedTurns = value;

            _turnsText.text = _savedTurns;


        }
    }

    string _savedGameLayoutName;
    public string SavedGameLayoutName
    {
        get
        {
            return _savedGameLayoutName;
        }
        set
        {
            _savedGameLayoutName = value;

            _gameLayoutNameText.text = _savedGameLayoutName;
        }
    }

    string _savedGameTimeStamp;
    public string SavedGameTimeStamp
    {
        get
        {
            return _savedGameTimeStamp;
        }
        set
        {
            _savedGameTimeStamp = value;

            _gameTimeStampText.text = _savedGameTimeStamp;
        }
    }


    [SerializeField] TMP_Text _gameLayoutNameText;
    [SerializeField] TMP_Text _gameTimeStampText;
    [SerializeField] TMP_Text _matchesText;
    [SerializeField] TMP_Text _turnsText;

}
