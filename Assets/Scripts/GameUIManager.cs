using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Game UI Handling Class
/// </summary>
public class GameUIManager : Singleton<GameUIManager>
{
    [Header("TEXT FIELDS SETTINGS")]
    [SerializeField] TMP_Text _matchesScoreText;
    [SerializeField] TMP_Text _matchesScoreGOText;
    [SerializeField] TMP_Text _turnsScoreText;
    [SerializeField] TMP_Text _turnsScoreGOText;


    [Header("UI PANEL SETTINGS")]
    [SerializeField] GameObject _mainMenuPanel;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] float _gameOverPopupShowDelayDuration;


    [Header("History Panel Settings")]
    [SerializeField] GameObject _gameHistoryPanel;
    [SerializeField] GameObject _gameHistoryPrefab;
    [SerializeField] Transform _gameHistoryParent;
    List<GameObject> _cachedHistoryData = new List<GameObject>();

    /// <summary>
    /// Update Matches UI Text
    /// </summary>
    /// <param name="matches"></param>
    public void UpdateMatchesScoreText(int matches)
    {
        _matchesScoreText.text = "Matches: " + matches;
        _matchesScoreGOText.text = "Matches: " + matches;
    }

    /// <summary>
    /// Update Turns UI Text
    /// </summary>
    /// <param name="turns"></param>
    public void UpdateTurnsScoreText(int turns)
    {
        _turnsScoreText.text = "Turns: " + turns;
        _turnsScoreGOText.text = "Turns: " + turns;
    }

    /// <summary>
    /// Show Game Over Popup
    /// </summary>
    public  void ShowGameOverPopup()
    {
        CardMatchUtils.Instance.DelayFunction(_gameOverPopupShowDelayDuration, () =>
        {
             GameAudioManager.Instance.PlaySFX("gamefinish");
            _gameOverPanel?.SetActive(true);



            GameProgressSaveData progressSaveData = SaveLoadSystem.LoadGameHistoryData("CardMatchSaveGame");

            if (progressSaveData == null)
                progressSaveData = new GameProgressSaveData();


            GameHistoryData historyData = new GameHistoryData();
            historyData._savedCardGridLayout = CardGridController.Instance.GetGridCellSettings().CardCellName;
            historyData._savedMatches = ScoreSystem.Instance.Matches.ToString();
            historyData._savedTurns = ScoreSystem.Instance.Turns.ToString();
            historyData._savedTimeStamp = DateTime.Now.ToString();

            progressSaveData._saveHistoryData.Add(historyData);

            SaveLoadSystem.SaveGameHistoryData("CardMatchSaveGame", progressSaveData);

        });
    }

    /// <summary>
    /// Start Game
    /// </summary>
    public void StartGame()
    {
        _mainMenuPanel?.SetActive(false);

        CardGridController.Instance.InitializeCardCellGridController();
    }

    /// <summary>
    /// Quit Game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Restart Game
    /// </summary>
    public void RestartGame()
    {

        GameAudioManager.Instance.StopSFX();

        CardGridController.Instance.DisposeCardGridController();
        ScoreSystem.Instance.DisposeScoreSystem();
        GameplayHandler.Instance.ResetCachedCardsData();

        _gameOverPanel?.SetActive(false);

        CardGridController.Instance.InitializeCardCellGridController();
    }

    /// <summary>
    /// Show Game History Panel
    /// </summary>
    public void ShowGameHistory()
    {
        
        GameProgressSaveData progressSaveData = SaveLoadSystem.LoadGameHistoryData("CardMatchSaveGame");

        _gameHistoryPanel?.SetActive(true);

        if (progressSaveData == null)
        {
            Debug.Log("No Data Available");
            return;
        }

        PopulateGameHistory(progressSaveData._saveHistoryData);


    }

    /// <summary>
    /// Close Game History Panel
    /// </summary>
    public void CloseGameHistory()
    {
        _gameHistoryPanel?.SetActive(false);

        DisposeHistoryData();
    }


    /// <summary>
    /// Generate History Data
    /// </summary>
    /// <param name="data"></param>
    void PopulateGameHistory(List<GameHistoryData> data)
    {
        for(int i=0;i<data.Count;i++)
        {
            GameObject go = Instantiate(_gameHistoryPrefab);
            go.transform.SetParent(_gameHistoryParent, false);

            GameSaveHistoryData saveHistoryData = go.GetComponent<GameSaveHistoryData>();
            saveHistoryData.SavedGameLayoutName = data[i]._savedCardGridLayout;
            saveHistoryData.SavedGameTimeStamp = data[i]._savedTimeStamp;
            saveHistoryData.SavedMatches = data[i]._savedMatches;
            saveHistoryData.SavedTurns = data[i]._savedTurns;

            go.SetActive(true);
            _cachedHistoryData.Add(go);
        }
    }

    /// <summary>
    /// Dispose History Data
    /// </summary>
    void DisposeHistoryData()
    {
        for(int i=0;i<_cachedHistoryData.Count;i++)
        {
            Destroy(_cachedHistoryData[i]);
        }
        _cachedHistoryData.Clear();
    }


}
