using System;
using AtanUtils.Extensions;
using AtanUtils.UI.Data;
using AtanUtils.UI.Interfaces;
using TMPro;
using UnityEngine;

namespace AtanUtils.UI.Controls
{
    public class TextControl : MonoBehaviour, IPaletteAware
    {
        private TMP_Text _text;

        public string Text
        {
            get => _text.text;
            set => _text.text = value;
        }

        public PaletteKey defaultColor = PaletteKey.TextPrimary;
        public FontKey fontKey = FontKey.Main;
        
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
    }
}