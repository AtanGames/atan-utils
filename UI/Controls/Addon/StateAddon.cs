using System;
using AtanUtils.UI.Interfaces;
using UnityEngine;

namespace AtanUtils.UI.Controls.Addon
{
    [RequireComponent(typeof(IStateAware))]
    public class StateAddon : MonoBehaviour
    {
        public StateInfo[] states;
        
        private IStateAware _stateControl;
        private int _currentStateIndex;
        
        public Action<int> OnStateChanged;
        
        private void Awake()
        {
            _stateControl = GetComponent<ButtonControl>();
        }

        private void Start()
        {
            _currentStateIndex = 0;
            UpdateState();
        }

        private void OnEnable()
        {
            _stateControl.OnStateChange += OnStateChange;
        }
        
        private void OnDisable()
        {
            _stateControl.OnStateChange -= OnStateChange;
        }
        
        private void OnStateChange()
        {
            _currentStateIndex++;
            
            if (_currentStateIndex >= states.Length)
                _currentStateIndex = 0;
            
            OnStateChanged?.Invoke(_currentStateIndex);
            
            UpdateState();
        }
        
        private void UpdateState()
        {
            _stateControl.UpdateState(states[_currentStateIndex]);
        }
        
        public void FromEnum<TEnum>(TEnum state) where TEnum : Enum
        {
            var enumValues = Enum.GetValues(typeof(TEnum));
            
            states = new StateInfo[enumValues.Length];
            for (int i = 0; i < enumValues.Length; i++)
            {
                states[i] = new StateInfo
                {
                    text = enumValues.GetValue(i).ToString()
                };
            }
            
            _currentStateIndex = Array.IndexOf(enumValues, state);
        }

        public void SetStateNoNotify (int i)
        {
            _currentStateIndex = i;
            
            UpdateState();
        }
    }

    [Serializable]
    public class StateInfo
    {
        public string text;
    }
}