using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CardGridController, It's MVC Model's Controller class. It is responsible for CardCell Data Generation.
/// </summary>
public class CardGridController : Singleton<CardGridController>
{
    [SerializeField] List<CardCellMatchData> _cardCellMatchData = new List<CardCellMatchData>();


    #region PRIVATE_MEMBERS
    GridCellSettings m_currentGridCellSetting = null;

    CardCell[] m_currentCardCellDataArray;

    CardGridUIView m_cardGridView = null;

    System.Random m_randomValue = new System.Random();
    #endregion

    private void Start()
    {
        m_cardGridView = CardGridUIView.Instance;
    }


    /// <summary>
    /// Initialization of CardCell Grid and Prepering Data
    /// </summary>
    public void InitializeCardCellGridController()
    {
        m_currentGridCellSetting = LevelRandomizer.Instance.GetRandomLevel();


        if (m_cardGridView.SetCardCellsGridLayout(m_currentGridCellSetting))
        {
            int totalCells = m_currentGridCellSetting.CardCellGridDimension.x * m_currentGridCellSetting.CardCellGridDimension.y;

            m_currentCardCellDataArray = new CardCell[totalCells];

            m_currentCardCellDataArray = m_cardGridView.InitializeCardCellGrid(totalCells, GenerateRandomMatchData(totalCells));
            
            ScoreSystem.Instance.TotalAvailableMatchesInCurrentGame = totalCells / 2;
        }


    }



    /// <summary>
    /// Get GridCellSettings Scriptable Object
    /// </summary>
    /// <returns>GridCellSettings</returns>
    public GridCellSettings GetGridCellSettings()
    {
        return m_currentGridCellSetting;
    }


    /// <summary>
    /// Generate Random Card Cells Match Data
    /// </summary>
    /// <param name="totalCells"></param>
    /// <returns>Array of CardCellMatchData</returns>
    CardCellMatchData[] GenerateRandomMatchData(int totalCells)
    {
        int randomCellMatchIconCount = totalCells / 2;

        List<CardCellMatchData> cardCellMatchData = new(totalCells);

        int count = 0;

        while (count < randomCellMatchIconCount)
        {
            CardCellMatchData matchData = _cardCellMatchData[m_randomValue.Next(0, _cardCellMatchData.Count)];

            count++;
            cardCellMatchData.Add(matchData);
        }



        cardCellMatchData.AddRange(cardCellMatchData.GetRange(0, randomCellMatchIconCount));



        return ShuffleCardCellsValues(cardCellMatchData);

    }

    /// <summary>
    /// Shuffle Card Cells
    /// </summary>
    /// <param name="cardCellMatchData"></param>
    /// <returns>Array of CardCellMatchData<</returns>
    CardCellMatchData[] ShuffleCardCellsValues(List<CardCellMatchData> cardCellMatchData)
    {

        for(int i=0;i<cardCellMatchData.Count;i++)
        {
            int shuffleIndex = m_randomValue.Next(0, cardCellMatchData.Count);

            var shuffleValue = cardCellMatchData[shuffleIndex];
            var currentValue = cardCellMatchData[i];

            cardCellMatchData[i] = shuffleValue;
            cardCellMatchData[shuffleIndex] = currentValue;
        }

        return cardCellMatchData.ToArray();
    }


    /// <summary>
    /// Dispose CardGridController, Resetting Data
    /// </summary>
    public void DisposeCardGridController()
    {
        m_currentGridCellSetting = null;

        foreach(var card in m_currentCardCellDataArray)
        {
            Destroy(card.gameObject);
        }

        m_currentCardCellDataArray = null;
    }




}

/// <summary>
/// Serialized CardCell Match Data Storage Class 
/// </summary>

[Serializable]
public class CardCellMatchData
{
    public string _uniqueID;
    public Sprite _cardCellMatchSprite;
}
