using UnityEngine;

public class Singleton<T>
    : MonoBehaviour where T
    : Singleton<T>
{
    private static T _instance = null;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this as T)
        {
            Destroy(gameObject);
        }
        init();
    }

    protected virtual void init()
    { }

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(T)) as T;
            }
            return _instance;
        }
    }

}