using UnityEngine;

namespace AtanUtils.Extensions
{
    /// <summary>
    /// Extensions for unity components
    /// </summary>
    public static class ComponentExtensions
    {
        #region Position
        
        public static void SetAnchoredPositionX(this RectTransform rectTransform, float x)
        {
            rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);
        }
        
        public static void SetAnchoredPositionY(this RectTransform rectTransform, float y)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
        }

        public static void SetPositionX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        
        public static void SetPositionY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        
        public static void SetPositionZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
        
        public static void SetLocalPositionX(this Transform transform, float x)
        {
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }
        
        public static void SetLocalPositionY(this Transform transform, float y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }
        
        public static void SetLocalPositionZ(this Transform transform, float z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
        }
        
        public static void AddPositionX(this Transform transform, float xAdd)
        {
            transform.position = new Vector3(transform.position.x + xAdd, transform.position.y, transform.position.z);
        }
        
        public static void AddPositionY(this Transform transform, float yAdd)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + yAdd, transform.position.z);
        }
        
        public static void AddPositionZ(this Transform transform, float zAdd)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + zAdd);
        }
        
        public static void Set2DPosition (this Transform transform, Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }
        
        public static void Set2DPosition (this Transform transform, float x, float y)
        {
            transform.position = new Vector3(x, y, transform.position.z);
        }
        
        public static void Set2DScale (this Transform transform, Vector2 scale)
        {
            transform.localScale = new Vector3(scale.x, scale.y, 1f);
        }
        
        public static void Set2DScale (this Transform transform, float x, float y)
        {
            transform.localScale = new Vector3(x, y, 1f);
        }
        
        /// <summary>
        /// Can be used to perform an operation on a transform without having to cache it first
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="operation"></param>
        public static void Operate (this Transform transform, System.Action<Transform> operation)
        {
            if (operation == null) return;
            operation(transform);
        }
        
        #endregion

        #region Vector
        
        public static Vector3 XZ (this Vector3 vector)
        {
            return new Vector3(vector.x, 0, vector.z);
        }

        public static Vector3 ToVector3_XZ (this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }

        public static Vector3 AddX(this Vector3 vector, float x)
        {
            return new Vector3(vector.x + x, vector.y, vector.z);
        }
        
        public static Vector3 AddY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, vector.y + y, vector.z);
        }
        
        public static Vector3 AddZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, vector.z + z);
        }
        
        public static Vector2 AddX(this Vector2 vector, float x)
        {
            return new Vector2(vector.x + x, vector.y);
        }
        
        public static Vector2 AddY(this Vector2 vector, float y)
        {
            return new Vector2(vector.x, vector.y + y);
        }
        
        public static (float x, float y) ToTuple(this Vector2 vector)
        {
            return (vector.x, vector.y);
        }
        
        public static (float x, float y, float z) ToTuple(this Vector3 vector)
        {
            return (vector.x, vector.y, vector.z);
        }
        
        public static Vector2 ToVector(this (float x, float y) tuple)
        {
            return new Vector2(tuple.x, tuple.y);
        }
        
        public static Vector3 ToVector(this (float x, float y, float z) tuple)
        {
            return new Vector3(tuple.x, tuple.y, tuple.z);
        }
        
        #endregion

        #region Rigidbody

        public static void SetLinearVelocityX(this Rigidbody rb, float x)
        {
            rb.linearVelocity = new Vector3(x, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        
        public static void SetLinearVelocityY(this Rigidbody rb, float y)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, y, rb.linearVelocity.z);
        }
        
        public static void SetLinearVelocityZ(this Rigidbody rb, float z)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, z);
        }

        #endregion

        #region Transform

        public static void DestroyAllChildren(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// Converts a world‐space rotation into local‐space relative to this transform.
        /// </summary>
        public static Quaternion InverseTransformRotation(this Transform t, Quaternion worldRotation)
        {
            return Quaternion.Inverse(t.rotation) * worldRotation;
        }

        /// <summary>
        /// Converts a local‐space rotation into world‐space using this transform’s rotation.
        /// </summary>
        public static Quaternion TransformRotation(this Transform t, Quaternion localRotation)
        {
            return t.rotation * localRotation;
        }
        
        public static Transform[] GetAllChildren(this Transform transform)
        {
            Transform[] children = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                children[i] = transform.GetChild(i);
            }
            return children;
        }
        
        #endregion
    }
}