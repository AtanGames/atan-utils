using System;
using UnityEngine;

namespace AtanUtils.Base
{
    /// <summary>
    /// Static Instance.
    /// OVERRIDE AWAKE/ONENABLE/ONDISABLE IF YOU USE IT
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class InstanceMonoBehaviour<T> : MonoBehaviour where T : InstanceMonoBehaviour<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            EnsureInstance();
        }

        protected virtual void OnEnable()
        {
            EnsureInstance();
        }

        private void EnsureInstance()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning($"Another instance of {typeof(T).Name} already exists. Destroying duplicate.");
                Destroy(gameObject);
                return;
            }

            Instance = (T)this;
        }

        private void OnDisable()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}