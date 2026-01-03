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
        
        public TextControl text;

        public void SetText(string newText)
        {
            text.Text = newText;
        }
    }
}