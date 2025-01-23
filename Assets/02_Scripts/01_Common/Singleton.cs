using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    static bool _isApplicationQuitting;

    public static T Instance
    {
        get
        {
            // Check if the application is quitting to avoid accessing the instance
            if (_isApplicationQuitting)
            {
                Debug.LogWarning($"[Singleton] Instance of {typeof(T)} is being accessed after application is quitting.");
                return null;
            }

            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();

                if (_instance == null)
                {
                    // Create a new GameObject to hold the Singleton
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        // Ensure that this object is the only instance of the singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Assign this as the singleton instance if it's the first one
        _instance = this as T;

        // Ensure that this instance persists across scenes
        DontDestroyOnLoad(gameObject);
    }

    // This method is called when the application quits or the scene is unloaded
    protected virtual void OnApplicationQuit()
    {
        _isApplicationQuitting = true;
    }
}
