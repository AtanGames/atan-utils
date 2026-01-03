using AtanUtils.Extensions;
using AtanUtils.UI.Controls;
using AtanUtils.UI.Controls.Addon;
using AtanUtils.UI.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AtanUtils.UI.Controls
{
    public class TextIconButtonControl : ButtonControl
    {
        [Header("Icon Button")]
        
        [SerializeField]
        private Image icon;

        [Header("Text Button")] 
        
        [SerializeField]
        private TextControl text;

        public string Text
        {
            get => text.Text;
            set => text.Text = value;
        }

        public Sprite Icon
        {
            get => icon.sprite;
            set => icon.sprite = value;
        }
        
        public void SetIconColor(Color color)
        {
            icon.color = color;
        }
    }
}