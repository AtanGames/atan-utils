using System;
using AtanUtils.UI.Data;
using UnityEngine;
using UnityEngine.UI;

namespace AtanUtils.UI.Controls
{
    public class IconButtonControl : ButtonControl
    {
        [Header("Icon Button")]
        
        [SerializeField]
        private Image icon;

        public void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }
        
        public void SetIconColor(Color color)
        {
            icon.color = color;
        }
    }
}
