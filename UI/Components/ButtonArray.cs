using System;
using System.Collections.Generic;
using AtanUtils.UI.Controls;
using UnityEngine;
using UnityEngine.UI;

namespace AtanUtils.UI.Components
{
    public class ButtonArray : MonoBehaviour
    {
        public Action<int> OnButtonPressed;
        
        public GameObject buttonPrefab;
        public bool clearOnStart = true;
        
        private List<ButtonControl> _buttons;

        private void Awake()
        {
            if (!clearOnStart)
                return;
            
            var childCount = transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                
                if (child.name.StartsWith(buttonPrefab.name))
                    Destroy(child.gameObject);
            }
        }

        public void LoadIconButtons(Sprite[] icons, Color[] iconColors = null)
        {
            LoadCustom(icons.Length, (i, button) =>
            {
                if (button is IconButtonControl iconButton)
                {
                    iconButton.SetIcon(icons[i]);
                
                    if (iconColors != null && iconColors.Length > i)
                        iconButton.SetIconColor(iconColors[i]);
                }
            });
        }

        public void LoadCustom (int count, Action<int, ButtonControl> link)
        {
            _buttons ??= new List<ButtonControl>();
            
            for (var i = 0; i < count; i++)
            {
                var obj = Instantiate(buttonPrefab, transform);
                var button = obj.GetComponent<ButtonControl>();

                int index = i;
                button.OnPress += () => OnButtonPressed?.Invoke(index);
                
                link(i, button);
                
                _buttons.Add(button);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            
            LinkButtonGroup();
        }
        
        public void CustomOperation (Action<int, ButtonControl> operation)
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                var button = _buttons[i];
                operation(i, button);
            }
        }
        
        private void LinkButtonGroup()
        {
            if (!TryGetComponent<ButtonGroup>(out var buttonGroup))
                return;
            
            buttonGroup.UpdateButtons(_buttons.ToArray());
        }

        public void ClearButtons()
        {
            if (_buttons == null) 
                return;

            foreach (var button in _buttons)
                Destroy(button.gameObject);
            
            _buttons.Clear();
        }
    }
}
