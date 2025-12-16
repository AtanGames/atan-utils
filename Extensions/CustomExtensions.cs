using AtanUtils.UI.Data;
using AtanUtils.UI.Management;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace AtanUtils.Extensions
{
    public static class CustomExtensions
    {
        public static Color GetColor(this PaletteKey key)
        {
            return PaletteManager.GetColor(key);
        }
        
        public static TMP_FontAsset GetFont(this FontKey key)
        {
            return PaletteManager.GetFont(key);
        }
    }
}