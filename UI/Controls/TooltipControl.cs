using System;
using AtanUtils.UI.Interfaces;
using AtanUtils.UI.Management;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AtanUtils.UI.Controls
{
    public class TooltipControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private RectTransform _rectTransform;
        private ITooltipProvider _tooltipProvider;

        public enum TooltipMode
        {
            Fixed,
            FollowMouse
        }
        
        public TooltipMode tooltipMode = TooltipMode.Fixed;
        
        public Vector2 offset = Vector2.zero;

        private bool _tooltipShown;
        
        private void Start()
        {
            _rectTransform = transform as RectTransform;
            _tooltipProvider = GetComponent<ITooltipProvider>();
            
            if (_tooltipProvider == null)
            {
                Debug.LogError("TooltipControl requires an ITooltipProvider component.");
                enabled = false;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            string tooltipText = _tooltipProvider.GetTooltip();
            
            if (string.IsNullOrEmpty(tooltipText))
                return;

            _tooltipShown = true;
            TooltipManager.Instance.ShowTooltipFixed(tooltipText, CenterWorld);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideTooltip();
        }

        private void OnDisable()
        {
            HideTooltip();
        }

        private void HideTooltip()
        {
            if (!_tooltipShown)
                return;
            
            _tooltipShown = false;

            TooltipManager.Instance.HideTooltip();
        }

        private Vector2 CenterWorld => transform.TransformPoint(_rectTransform.rect.center + offset);

        private void OnDrawGizmosSelected()
        {
            _rectTransform = transform as RectTransform;
            
            Gizmos.DrawLine(transform.TransformPoint(_rectTransform!.rect.center), 
                transform.TransformPoint(_rectTransform.rect.center + offset));
        }
    }
}
