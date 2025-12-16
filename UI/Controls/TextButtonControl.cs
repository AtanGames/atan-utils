using System;
using AtanUtils.Extensions;
using AtanUtils.UI.Controls.Addon;
using AtanUtils.UI.Data;
using AtanUtils.UI.Management;
using TMPro;
using UnityEngine;

namespace AtanUtils.UI.Controls
{
    public class TextButtonControl : ButtonControl
    {
        [Header("Text Button")] 
        
        [SerializeField] 
        private TMP_Text text;

        public PaletteKey defaultTextColor = PaletteKey.TextPrimary;
        public FontKey defaultFont = FontKey.Main;

        public void SetText(string newText)
        {
            text.text = newText;
        }
        
        public override void UpdateState(StateInfo stateInfo)
        {
            SetText(stateInfo.text);
        }

        public override void UpdatePalette()
        {
            base.UpdatePalette();

            text.color = defaultTextColor.GetColor();
            text.font = defaultFont.GetFont();
        }
    }
}