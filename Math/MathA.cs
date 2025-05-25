using UnityEngine;

namespace AtanUtils.Math
{
    public static class MathA
    {
        /// <summary>
        /// Converts latitude and longitude to a position
        /// Longitude will be wrapped, Latitude will be clamped
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static Vector3 LatLonToPos(float latitude, float longitude)
        {
            latitude = Mathf.Clamp(latitude, -90f, 90f);

            longitude = (longitude + 180f) % 360f;
            if (longitude < 0)
                longitude += 360f;
            longitude -= 180f;
            
            float latRad = latitude * Mathf.Deg2Rad;
            float lonRad = longitude * Mathf.Deg2Rad;

            float x = Mathf.Cos(latRad) * Mathf.Cos(lonRad);
            float y = Mathf.Sin(latRad);
            float z = Mathf.Cos(latRad) * Mathf.Sin(lonRad);

            return new Vector3(x, y, z);
        }
        
        /// <summary>
        /// Converts a position to latitude and longitude
        /// </summary>
        /// <param name="position"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public static void PosToLatLon(Vector3 position, out float latitude, out float longitude)
        {
            position.Normalize();

            latitude = Mathf.Asin(position.y) * Mathf.Rad2Deg;
            longitude = Mathf.Atan2(position.z, position.x) * Mathf.Rad2Deg;
        }
        
        /// <summary>
        /// Lerp between 3 values
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float Lerp3(float a, float b, float c, float t)
        {
            if (t <= 0.5f)
            {
                // Scale t from [0, 0.5] to [0, 1] and interpolate between a and b.
                return a + (b - a) * (t * 2f);
            }

            // Scale t from [0.5, 1] to [0, 1] and interpolate between b and c.
            return b + (c - b) * ((t - 0.5f) * 2f);
        }
    }
}