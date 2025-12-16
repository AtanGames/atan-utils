using System;
using AtanUtils.Base;
using TMPro;
using UnityEngine;

namespace AtanUtils.UI.Management
{
    public class TooltipManager : InstanceMonoBehaviour<TooltipManager>
    {
        [Header("Tooltip")] 
        
        public RectTransform toolTipObject;
        public TMP_Text toolTipText;

        private bool _tooltipShown;

        private void Start()
        {
            toolTipObject.gameObject.SetActive(false);
        }

        public void ShowTooltipFixed(string text, Vector2 worldPos)
        {
            if (_tooltipShown)
            {
                Debug.LogWarning("More than one element is trying to show a tooltip at the same time. This is not supported.");
                return;
            }
            
            _tooltipShown = true;
            
            toolTipObject.gameObject.SetActive(true);
            toolTipText.text = text;

            UpdatePositionWorld(worldPos);
        }

        public void HideTooltip()
        {
            _tooltipShown = false;
            
            toolTipObject.gameObject.SetActive(false);
        }
        
        private void UpdatePositionScreen(Vector2 screenPos)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform as RectTransform, screenPos, null, out var local);
            
            toolTipObject.anchoredPosition = local;
        }
        
        private void UpdatePositionWorld(Vector2 worldPos)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform as RectTransform,
                RectTransformUtility.WorldToScreenPoint(null, worldPos),
                null,
                out Vector2 localPos
            );
            
            toolTipObject.anchoredPosition = localPos;
        }
    }
}