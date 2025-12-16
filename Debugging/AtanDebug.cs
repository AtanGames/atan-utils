using UnityEngine;

namespace AtanUtils.Debugging
{
    public static class AtanDebug
    {
        public static bool EditorOnly = true;
        
        private static readonly Color DefaultColor = new Color(1f, 1f, 1f, 1f);
        
        /// <summary>
        /// Draw a point (sphere) that updates continuously, use id to identify it.
        /// Specify with autoDestroy if it should be removed once no longer updated.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="id"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="autoDestroy"></param>
        public static void DrawPointPersistant(Vector3 point, string id = "default", float size = 0.1f, Color color = default, bool autoDestroy = false)
        {
            if (IgnoreDebug())
                return;
            
            if (color == default)
                color = DefaultColor;
            
            DebugManager.Instance.DrawPointPersistant(point, id, size, color, autoDestroy);
        }
        
        /// <summary>
        /// Draw a point (sphere) that lasts for a specified duration. Use duration <= 0 to make it persistent.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="size"></param>
        /// <param name="duration"></param>
        /// <param name="color"></param>
        public static void DrawPoint(Vector3 point, float size = 0.1f, float duration = 1f, Color color = default)
        {
            if (IgnoreDebug())
                return;
            
            if (color == default)
                color = DefaultColor;
            
            DebugManager.Instance.DrawPoint(point, size, color, duration);
        }
        
        private static bool IgnoreDebug()
        {
            #if !UNITY_EDITOR
            return EditorOnly;
            #else
            return false;
            #endif
        }
    }
}