using DG.Tweening;
using System;
using UnityEngine;

/// <summary>
/// CardAnimationHelper Extension Class, It is a Custom Extension class for "CardCell" Class.
/// </summary>
public static class CardAnimationHelper
{
    /// <summary>
    /// Card Flip Animation Method. Can Call Like :- CardCell.CardFlipAnimation()
    /// </summary>
    /// <param name="cardCell"></param>
    /// <param name="AnimationCompleteCallback"></param>
    /// <param name="reverse"></param>
    public static void CardFlipAnimation(this CardCell cardCell,Action AnimationCompleteCallback=null,bool reverse = false)
    {

        Transform cardBackFace = cardCell.CardBackFace;
        Transform cardFrontFace = cardCell.CardFrontFace;

        float flipSpeed = cardCell._cardFlipAnimationSpeed;


        float cardBackFaceScaleEndValue = -1;
        float cardFrontFaceScaleEndValue = -1;


        if (flipSpeed <= 0f)
            flipSpeed = 0.1f;


        if (reverse)
        {
            cardBackFaceScaleEndValue = 1f;
            cardFrontFaceScaleEndValue = 0f;


            cardFrontFace.DOScaleX(cardFrontFaceScaleEndValue, flipSpeed / 2f).OnComplete(() =>
            {
                cardBackFace.DOScaleX(cardBackFaceScaleEndValue, flipSpeed / 2f).OnComplete(() =>
                {
                    AnimationCompleteCallback?.Invoke();
                });
            });


        }
        else
        {
            cardBackFaceScaleEndValue = 0f;
            cardFrontFaceScaleEndValue = 1f;


            cardBackFace.DOScaleX(cardBackFaceScaleEndValue, flipSpeed / 2f).OnComplete(() =>
            {
                cardFrontFace.DOScaleX(cardFrontFaceScaleEndValue, flipSpeed / 2f).OnComplete(() =>
                {
                    AnimationCompleteCallback?.Invoke();
                });
            });

        }
    }




    /// <summary>
    /// Card Dispose Animation. Can Call Like :- CardCell.CardDisposeAnimation()
    /// </summary>
    /// <param name="cardCell"></param>
    /// <param name="AnimationCompleteCallback"></param>
    public static void CardDisposeAnimation(this CardCell cardCell,Action AnimationCompleteCallback=null)
    {
        float disposeAnimationSpeed = cardCell._cardDisposeAnimationSpeed;
        float maxZoom = cardCell._maxDisposeZoomValue;

        Transform mainFrame = cardCell._mainFrameTransform;

        if (disposeAnimationSpeed <= 0)
            disposeAnimationSpeed = 0.1f;


        mainFrame.DOScale(maxZoom, disposeAnimationSpeed / 2f).OnComplete(() =>
        {

            mainFrame.DOScale(0f, disposeAnimationSpeed / 2f).OnComplete(() =>
            {

                AnimationCompleteCallback?.Invoke();
            });

        });



    }

}
