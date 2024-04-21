using System.Collections.Generic;

/// <summary>
/// GameplayHandler, Responsible for Main Game Logic.
/// </summary>
public class GameplayHandler : Singleton<GameplayHandler>
{
    #region PRIVATE_MEMBERS
    CardCell m_matchCardOne = null;
    CardCell m_matchCardTwo = null;

    Queue<CardCell[]> m_matchDataQueue = new Queue<CardCell[]>();

    ScoreSystem m_scoreSystem;
    #endregion

    private void Start()
    {
        m_scoreSystem = ScoreSystem.Instance;
    }

    private void OnEnable()
    {
        CardCell.OnCardSelect += OnCardSelect;
    }

    private void OnDisable()
    {
        CardCell.OnCardSelect -= OnCardSelect;
    }


    /// <summary>
    /// Card Select Event
    /// </summary>
    /// <param name="cardCell"></param>
    void OnCardSelect(CardCell cardCell)
    {

        if (cardCell == null)
            return;


        MatchCards(cardCell);
    }

    /// <summary>
    /// Cards Matching Logic
    /// </summary>
    /// <param name="cardCell"></param>
    void MatchCards(CardCell cardCell)
    {

        if (m_matchCardOne == null)
        {
            m_matchCardOne = cardCell;
            return;
        }

        if (m_matchCardTwo == null)
            m_matchCardTwo = cardCell;


        m_matchDataQueue.Enqueue(new CardCell[] { m_matchCardOne, m_matchCardTwo});

        m_matchCardOne = null;
        m_matchCardTwo = null;


        m_scoreSystem.Turns++;

        InitiateMatching();

    }

    /// <summary>
    /// Initiate Matching, Responsible for caching the two matching cards. 
    /// </summary>
    void InitiateMatching()
    {
        CardCell[] cardCell = m_matchDataQueue.Dequeue();
        bool isMatch = cardCell[0].CardUniqueMatchID.Equals(cardCell[1].CardUniqueMatchID);

        if (isMatch)
            OnCardsMatch(cardCell[0], cardCell[1]);
        else
            OnCardMismatch(cardCell[0], cardCell[1]);
    }


    /// <summary>
    /// On Pair of Cards Match
    /// </summary>
    /// <param name="cardCellOne"></param>
    /// <param name="cardCellTwo"></param>
    void OnCardsMatch(CardCell cardCellOne, CardCell cardCellTwo)
    {
        if (cardCellOne == null && cardCellTwo == null)
            return;


        m_scoreSystem.Matches++;

        GameAudioManager.Instance.PlaySFX("match");

        cardCellOne.CardDisposeAnimation(()=> 
        {
            cardCellOne.IsCardDisposed = true;
        });
        cardCellTwo.CardDisposeAnimation(() => 
        {
            cardCellTwo.IsCardDisposed = true;
        });


    }


    /// <summary>
    /// On Pair of Cards Mismatch
    /// </summary>
    /// <param name="cardCellOne"></param>
    /// <param name="cardCellTwo"></param>
    void OnCardMismatch(CardCell cardCellOne, CardCell cardCellTwo)
    {
        if (cardCellOne == null && cardCellTwo == null)
            return;


        GameAudioManager.Instance.PlaySFX("mismatch");


        cardCellOne.CardFlipAnimation(() =>
        {
            cardCellOne.IsCardFlipped = false;

        }, true);
        cardCellTwo.CardFlipAnimation(() =>
        {
            cardCellTwo.IsCardFlipped = false;
        }, true);

    }


    /// <summary>
    /// Reset Cached Pair of Cards
    /// </summary>
    public void ResetCachedCardsData()
    {
        m_matchCardOne = null;
        m_matchCardTwo = null;
    }

}
