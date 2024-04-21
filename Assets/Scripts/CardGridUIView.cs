using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CardGridUIView, It's MVC Model's View class. Responsible for Managing CardCell Grid View UI.
/// </summary>
public class CardGridUIView : Singleton<CardGridUIView>
{
    [SerializeField] GameObject _cardCellPrefab;
    [SerializeField] Transform _cardCellGridParent;
    [SerializeField] GridLayoutGroup _gridLayoutGroup;

    [SerializeField] float _initialCardViewLoadAnimationDuration;

    /// <summary>
    /// Setting Up the UI GridLayout Group to Particular GridCellSettings Scriptable Object.
    /// </summary>
    /// <param name="gridCellSetting"></param>
    /// <returns>boolean</returns>
    public bool SetCardCellsGridLayout(GridCellSettings gridCellSetting)
    {

        if (gridCellSetting == null)
            return false;

        if (_gridLayoutGroup == null)
            return false;


        _gridLayoutGroup.cellSize = gridCellSetting.CardCellSize;
        _gridLayoutGroup.spacing = gridCellSetting.CardCellSpacing;
        _gridLayoutGroup.constraint = gridCellSetting.CardCellConstraint;
        _gridLayoutGroup.constraintCount = gridCellSetting.CardCellConstraintCount;

        return true;
    }

    /// <summary>
    /// InitializeCardCellGrid, Instantiating the CardCell Grid On UI.
    /// </summary>
    /// <param name="totalCells"></param>
    /// <param name="cellMatchData"></param>
    /// <returns>Array of CardCell</returns>
    public CardCell[] InitializeCardCellGrid(int totalCells, CardCellMatchData[] cellMatchData)
    {
        CardCell[] result = new CardCell[totalCells];

        for (int i = 0; i < totalCells; i++)
        {

            GameObject cellObject = Instantiate(_cardCellPrefab);
            cellObject.transform.SetParent(_cardCellGridParent, false);


            CardCell cell = cellObject.GetComponent<CardCell>();
            cell.IsCardInteractable = false;
            
            cell.SetCardData(cellMatchData[i]._uniqueID, cellMatchData[i]._cardCellMatchSprite);
            cell.SetDefaultCardFace(false);

            result[i] = cell;
        }



        CardMatchUtils.Instance.DelayFunction(_initialCardViewLoadAnimationDuration, () => RunInitialCardAnimation(result));


        return result;
    }

    /// <summary>
    /// Initial Animation Of All Cards On UI
    /// </summary>
    /// <param name="cardCell"></param>
    void RunInitialCardAnimation(CardCell[] cardCell)
    {
        GameAudioManager.Instance.PlaySFX("cardflip");

        foreach(var card in cardCell)
        {
            card.CardFlipAnimation(()=>card.IsCardInteractable=true, true);
        }
    }


}
