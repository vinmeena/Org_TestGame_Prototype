/// <summary>
/// ScoreSystem, Full Score Mechanism
/// </summary>
public class ScoreSystem : Singleton<ScoreSystem>
{
    int _matches;
    public int Matches
    {
        get
        {
            return _matches;
        }
         set
        {
            _matches = value;

            GameUIManager.Instance.UpdateMatchesScoreText(_matches);


            if (Matches == _totalMathces)
            {
                GameUIManager.Instance.ShowGameOverPopup();

            }

        }
    }

    int _turns;
    public int Turns
    {

        get
        {
            return _turns;
        }
         set
        {
            _turns = value;

            GameUIManager.Instance.UpdateTurnsScoreText(_turns);

        }

    }


    int _totalMathces;
    public int TotalAvailableMatchesInCurrentGame
    {
        get
        {
            return _totalMathces;
        }
         set
        {
            _totalMathces = value;

        }
    }


    /// <summary>
    /// Dispose Score System
    /// </summary>
    public void DisposeScoreSystem()
    {
        Matches = 0;
        Turns = 0;
        TotalAvailableMatchesInCurrentGame = -1;
    }


}
