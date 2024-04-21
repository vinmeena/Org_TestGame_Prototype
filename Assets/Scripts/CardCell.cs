using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// CardCell, Main card cell class which stores all the required data.
/// </summary>
public class CardCell : MonoBehaviour,IPointerClickHandler
{
    public bool IsCardFlipped { get; set; }

    public bool IsCardDisposed { get; set; }

    public string CardUniqueMatchID { get; private set; }

    public Transform CardBackFace {get;private set;}
    public Transform CardFrontFace{get;private set;}

    public bool IsCardInteractable { get; set; }


    [Header("Card Settings")]
    [SerializeField] Image _cardCellMatchIcon;
    public Transform _mainFrameTransform;
    


    [Header("Card Animation Settings")]
    public float _cardFlipAnimationSpeed;
    public float _cardDisposeAnimationSpeed;
    public float _maxDisposeZoomValue;


    public static Action<CardCell> OnCardSelect;



    private void Awake()
    {
        CardBackFace = _mainFrameTransform.GetChild(0);
        CardFrontFace = _mainFrameTransform.GetChild(1);
    }

    /// <summary>
    /// Set Card Data, Set Card Icon and Unique Match ID
    /// </summary>
    /// <param name="cardMatchUniqueID"></param>
    /// <param name="cardIcon"></param>
    public void SetCardData(string cardMatchUniqueID,Sprite cardIcon)
    {
        CardUniqueMatchID = cardMatchUniqueID;
        _cardCellMatchIcon.sprite = cardIcon;
    }

    /// <summary>
    /// Event Handler for Pointer Click
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {

        if (!IsCardInteractable)
            return;


        if (IsCardFlipped)
            return;


        GameAudioManager.Instance.PlaySFX("cardflip");

        this.CardFlipAnimation(() => 
        {

            IsCardFlipped = true;
            OnCardSelect?.Invoke(this);
        });
            }


/// <summary>
/// Default Card Face Update Method
/// </summary>
/// <param name="backFace"></param>
    public void SetDefaultCardFace(bool backFace)
    {
        if(backFace)
        {
            CardBackFace.localScale = new Vector3(1, 1, 1);
            CardFrontFace.localScale = new Vector3(0, 1, 1);
        }
        else
        {
            CardBackFace.localScale = new Vector3(0, 1, 1);
            CardFrontFace.localScale = new Vector3(1, 1, 1);
        }
    }


}
