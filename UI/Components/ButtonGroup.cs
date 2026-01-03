using System;
using AtanUtils.UI.Controls;
using UnityEngine;

namespace AtanUtils.UI.Components
{
    public class ButtonGroup : MonoBehaviour
    {
        public Action<int> OnButtonSelected;
        
        [SerializeField]
        private ButtonControl[] buttons;

        private ButtonControl _currentSelection;
        
        [Header("Settings")]
        
        [Tooltip("If true, selection is cleared when the current selection is pressed again.")]
        public bool clearSelectionOnPress = true;

        private bool _buttonsLinked;
        
        private void Start()
        {
            LoadButtonGroup();
        }

        public void UpdateButtons(ButtonControl[] buttonControls)
        {
            buttons = buttonControls;
            _buttonsLinked = false;
            
            LoadButtonGroup();
        }

        private void LoadButtonGroup()
        {
            if (_buttonsLinked)
                return;
            
            _buttonsLinked = true;

            buttons ??= Array.Empty<ButtonControl>();
            
            for (var i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];
                int index = i;
                button.OnPress += () => OnPress(index);
            }
        }
        
        public void SetSelectedIndex(int index, bool invokeEvent = true)
        {
            if (index < 0 || index >= buttons.Length)
            {
                ClearSelection();
                return;
            }
            
            OnPress(index, invokeEvent);
        }

        private void OnPress(int index, bool invokeEvent = true)
        {
            if (_currentSelection != null)
            {
                _currentSelection.OnButtonDeselect();
            }
            
            if (clearSelectionOnPress && _currentSelection == buttons[index])
            {
                _currentSelection = null;
                OnButtonSelected?.Invoke(-1);
                return;
            }
            
            _currentSelection = buttons[index];
            
            _currentSelection.OnButtonSelect();
            
            if (invokeEvent)
                OnButtonSelected?.Invoke(index);
        }
        
        public void ClearSelection()
        {
            if (_currentSelection != null)
            {
                _currentSelection.OnButtonDeselect();
                _currentSelection = null;
                OnButtonSelected?.Invoke(-1);
            }
        }
    }
}
