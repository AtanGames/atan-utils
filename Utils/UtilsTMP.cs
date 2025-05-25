using System;
using UnityEngine;

namespace AtanUtils.Utils
{
    public static class UtilsTMP
    {
        public static string RT_Sprite(int spriteIndex)
        {
            return "<sprite=" + spriteIndex + ">";
        }        
        
        public static string RT_Sprite(string spriteName)
        {
            if (String.IsNullOrEmpty(spriteName))
                spriteName = " ";
            
            return "<sprite name=\"" + spriteName + "\">";
        }

        public static string RT_Color(Color color, string innerText)
        {
            return "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + innerText + "</color>";
        }
        
        public static string RT_Size(int size, string innerText)
        {
            return "<size=" + size + ">" + innerText + "</size>";
        }
        
        public static string RT_Bold(string innerText)
        {
            return "<b>" + innerText + "</b>";
        }
        
        public static string RT_Italic(string innerText)
        {
            return "<i>" + innerText + "</i>";
        }
        
        public static string RT_Underline(string innerText)
        {
            return "<u>" + innerText + "</u>";
        }
    }
}