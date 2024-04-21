using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// CardMatchUtils, Utility class
/// </summary>
public class CardMatchUtils : Singleton<CardMatchUtils>
{
    /// <summary>
    /// Delay Function, Can give delay in calling
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="SuccessAction"></param>
    public void DelayFunction(float delay, Action SuccessAction)
    {
        StopCoroutine("DelayCoroutine");
        StartCoroutine(DelayCoroutine(delay,SuccessAction));
    }

    /// <summary>
    /// Coroutine
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="SuccessAction"></param>
    /// <returns></returns>
     IEnumerator DelayCoroutine(float delay,Action SuccessAction)
    {
        float timer = 0f;

        while(timer<delay)
        {
            timer += Time.deltaTime;

            yield return null;
        }

        SuccessAction?.Invoke();
    }

}
