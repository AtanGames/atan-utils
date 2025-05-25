using UnityEngine;

namespace AtanUtils.Base
{
    /// <summary>
    /// Static Instance + DontDestroyOnLoad
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning($"Another instance of {typeof(T).Name} already exists. Destroying duplicate.");
                Destroy(gameObject);
                return;
            }

            Instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
    }
}