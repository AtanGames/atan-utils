using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

namespace AtanUtils.Tools
{
    public class TMP_LinkOpener : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Color _linkColor = Color.cyan;

        TextMeshProUGUI _textMeshPro;
        Camera _uiCamera;

        void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();

            Canvas canvas = _textMeshPro.canvas;
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                _uiCamera = null;
            else
                _uiCamera = canvas.worldCamera;

            ApplyLinkColor();
        }

        void ApplyLinkColor()
        {
            string originalText = _textMeshPro.text;
            string hexColor = ColorUtility.ToHtmlStringRGBA(_linkColor);

            string pattern = "<link(.*?)>(.*?)</link>";

            string replaced = Regex.Replace(
                originalText,
                pattern,
                m =>
                {
                    string linkStart = m.Groups[1].Value;
                    string visibleText = m.Groups[2].Value;

                    return $"<link{linkStart}><color=#{hexColor}>{visibleText}</color></link>";
                },
                RegexOptions.Singleline
            );

            _textMeshPro.text = replaced;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(
                _textMeshPro, eventData.position, _uiCamera);

            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = _textMeshPro.textInfo.linkInfo[linkIndex];
                string url = linkInfo.GetLinkID();

                Application.OpenURL(url);
            }
        }
    }
}