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
    public class ButtonControl : MonoBehaviour, IPointerClickHandler, IPaletteAware, IStateAware, ITooltipProvider, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Button")] 
        
        public Graphic background;
        public SoundObj pressSound;

        public Color hoverTint = new Color(0.8f, 0.8f, 0.8f, 1f);
        
        public PaletteKey defaultBackgroundColor = PaletteKey.Primary;
        public PaletteKey selectedBackgroundColor = PaletteKey.Selection;
        public PaletteKey disabledBackgroundColor = PaletteKey.Warning;
        
        public Action OnPress;
        
        public Action OnStateChange
        {
            get => OnPress;
            set => OnPress = value;
        }

        private bool _isSelected;
        private bool _isHovered;
        private bool _isDisabled;

        public bool IsDisabled
        {
            get => _isDisabled;
            set => _isDisabled = value;
        }
        
        private bool _overrideBackgroundColor;
        private Color _customBackgroundColor;

        private string _tooltipText;

        private void Start()
        {
            UpdatePalette();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (_isDisabled)
                return;
            
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
            UpdateBackground();
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
            if (_isDisabled)
                return disabledBackgroundColor.GetColor();
            
            if (_isSelected)
                return selectedBackgroundColor.GetColor();
            
            if (_overrideBackgroundColor)
                return _customBackgroundColor;
            
            return defaultBackgroundColor.GetColor();
        }
        
        private void UpdateBackground ()
        {
            if (background == null)
                return;
            
            Color baseColor = GetBackgroundColor();
            
            if (_isHovered && !_isDisabled)
                baseColor *= hoverTint;
            
            background.color = baseColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isHovered = true;
            UpdateBackground();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHovered = false;
            UpdateBackground();
        }
    }
}