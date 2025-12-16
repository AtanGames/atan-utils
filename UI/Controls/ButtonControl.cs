using System;
using AtanUtils.Extensions;
using AtanUtils.Sound;
using AtanUtils.UI.Controls.Addon;
using AtanUtils.UI.Data;
using AtanUtils.UI.Interfaces;
using AtanUtils.UI.Management;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AtanUtils.UI.Controls
{
    public class ButtonControl : MonoBehaviour, IPointerClickHandler, IPaletteAware, IStateAware, ITooltipProvider
    {
        [Header("Button")] 
        
        public Graphic background;
        public SoundObj pressSound;

        public PaletteKey defaultBackgroundColor = PaletteKey.Primary;
        public PaletteKey selectedBackgroundColor = PaletteKey.Selection;
        
        public Action OnPress;
        
        public Action OnStateChange
        {
            get => OnPress;
            set => OnPress = value;
        }

        private bool _isSelected;
        
        private bool _overrideBackgroundColor;
        private Color _customBackgroundColor;

        private string _tooltipText;

        private void Start()
        {
            UpdatePalette();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnPress?.Invoke();
            pressSound?.Play();
        }

        public virtual void OnButtonSelect()
        {
            _isSelected = true;
            UpdatePalette();
        }

        public virtual void OnButtonDeselect()
        {
            _isSelected = false;
            UpdatePalette();
        }

        public virtual void UpdatePalette()
        {
            if (background != null)
                background.color = GetBackgroundColor();
        }

        public virtual void UpdateState(StateInfo stateInfo)
        {
            
        }

        public virtual string GetTooltip()
        {
            return _tooltipText;
        }
        
        public void SetTooltip(string tooltip)
        {
            _tooltipText = tooltip;
        }

        public void SetBackgroundColor(Color color)
        {
            float mag = Mathf.Max(1, color.r, color.g, color.b);
            
            Color c = color / mag;
            c.a = 1;
            
            _customBackgroundColor = c;
            _overrideBackgroundColor = true;
            
            UpdatePalette();
        }

        private Color GetBackgroundColor()
        {
            if (_isSelected)
                return selectedBackgroundColor.GetColor();
            
            if (_overrideBackgroundColor)
                return _customBackgroundColor;
            
            return defaultBackgroundColor.GetColor();
        }
    }
}