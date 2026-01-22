using UnityEngine;

namespace AtanUtils.Debugging
{
    public static class AtanDebug
    {
        public static void DrawPoint(Vector3 position, float size, Color color, float duration = 0f)
        {
            float halfSize = size * 0.5f;

            Debug.DrawLine(position - Vector3.right   * halfSize, position + Vector3.right   * halfSize, color, duration);
            Debug.DrawLine(position - Vector3.up      * halfSize, position + Vector3.up      * halfSize, color, duration);
            Debug.DrawLine(position - Vector3.forward * halfSize, position + Vector3.forward * halfSize, color, duration);
        }
    }
}