using System;
using AtanUtils.Extensions;
using AtanUtils.UI.Data;
using AtanUtils.UI.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AtanUtils.UI.Controls
{
    public class TextControl : MonoBehaviour, IPaletteAware, IPointerEnterHandler, IPointerExitHandler
    {
        public enum HoverMode
        {
            None,
            Underline,
        }
        
        private TMP_Text _text;

        public string Text
        {
            get => _text.text;
            set => _text.text = value;
        }

        public PaletteKey defaultColor = PaletteKey.TextPrimary;
        public FontKey fontKey = FontKey.Main;
        public HoverMode hoverMode = HoverMode.None;
        
        private bool _isHovered = false;
        
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            UpdatePalette();
        }

        [ContextMenu("Update Palette")]
        public void UpdatePalette()
        {
            _text ??= GetComponent<TMP_Text>();
            _text.color = defaultColor.GetColor();
            _text.font = fontKey.GetFont();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isHovered)
                return;
            
            _isHovered = true;
            
            if (hoverMode == HoverMode.Underline)
            {
                _text.text = "<u>" + _text.text + "</u>";
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isHovered)
                return;
            
            _isHovered = false;
            
            if (hoverMode == HoverMode.Underline)
            {
                _text.text = _text.text.Replace("<u>", "").Replace("</u>", "");
            }
        }
    }
}