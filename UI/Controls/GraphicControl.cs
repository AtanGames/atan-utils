using System;
using AtanUtils.Extensions;
using AtanUtils.UI.Data;
using AtanUtils.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace AtanUtils.UI.Controls
{
    public class GraphicControl : MonoBehaviour, IPaletteAware
    {
        public PaletteKey defaultColorKey = PaletteKey.Background;

        private void Start()
        {
            UpdatePalette();
        }

        [ContextMenu("Update Palette")]
        public void UpdatePalette()
        {
            GetComponent<Graphic>().color = defaultColorKey.GetColor();
        }
    }
}