using TMPro;
using UnityEngine;

namespace AtanUtils.UI.Data
{
    [CreateAssetMenu(fileName = "ColorPalette", menuName = "AtanUtils/UI/ColorPalette")]
    public class ColorPalette : ScriptableObject
    {
        [Header("Main Colors")]
        
        public Color Primary;
        public Color Background;
        public Color Accent;

        [Header("Text Colors")] 
        
        public Color TextPrimary;

        [Header("Conditional Colors")] 
        
        public Color Selection;

        [Header("Font")] 
        
        public TMP_FontAsset MainFont;
        
        public Color GetColor(PaletteKey key)
        {
            return key switch
            {
                PaletteKey.Primary => Primary,
                PaletteKey.TextPrimary => TextPrimary,
                PaletteKey.Selection => Selection,
                PaletteKey.Background => Background,
                PaletteKey.Accent => Accent,
                _ => throw new System.ArgumentOutOfRangeException(nameof(key), key, null)
            };
        }
        
        public TMP_FontAsset GetFont(FontKey key)
        {
            return key switch
            {
                FontKey.Main => MainFont,
                _ => throw new System.ArgumentOutOfRangeException(nameof(key), key, null)
            };
        }
    }

    public enum PaletteKey
    {
        Primary,
        TextPrimary,
        Selection,
        Background,
        Accent
    }

    public enum FontKey
    {
        Main,
    }
}