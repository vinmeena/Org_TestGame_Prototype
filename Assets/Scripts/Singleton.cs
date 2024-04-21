using UnityEngine;
/// <summary>
/// Singleton Generic Class, Responsible for Providing Singleton Capabilities without code redundancy.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : class
{
    static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(T)) as T;


            return _instance;
        }

    }


}
