using System.Linq;
using AtanUtils.Base;
using AtanUtils.Extensions;
using AtanUtils.UI.Data;
using AtanUtils.UI.Interfaces;
using TMPro;
using UnityEngine;

namespace AtanUtils.UI.Management
{
    [ExecuteInEditMode]
    public class PaletteManager : InstanceMonoBehaviour<PaletteManager>
    {
        [SerializeField]
        private ColorPalette palette;
        
        public void ChangePalette(ColorPalette newPalette)
        {
            if (newPalette == null)
            {
                Debug.LogWarning("Attempted to change to a null palette.");
                return;
            }
            
            palette = newPalette;
            
            UpdatePalette();
        }

        [ContextMenu("Update Palette")]
        private void UpdatePalette()
        {
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IPaletteAware>()
                .ForEach(t => t.UpdatePalette());
        }

        public static Color GetColor(PaletteKey key)
        {
            if (Instance == null)
            {
                Debug.LogWarning("PaletteManager instance is null. Cannot get color.");
                return Color.magenta;
            }
            
            if (Instance.palette == null)
            {
                Debug.LogWarning("Palette is not set in PaletteManager. Cannot get color.");
                return Color.magenta;
            }

            return Instance.palette.GetColor(key);
        }

        public static TMP_FontAsset GetFont(FontKey key)
        {
            if (Instance == null)
            {
                Debug.LogWarning("PaletteManager instance is null. Cannot get font.");
                return null;
            }
            
            if (Instance.palette == null)
            {
                Debug.LogWarning("Palette is not set in PaletteManager. Cannot get font.");
                return null;
            }

            return Instance.palette.GetFont(key);
        }
    }
}
