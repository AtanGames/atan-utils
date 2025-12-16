using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace AtanUtils.Debugging
{
    public class DebugManager : MonoBehaviour
    {
        private const int AUTO_DELETE_FRAMES = 3;
        
        public static DebugManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject debugManagerObject = new GameObject("DebugManager");
                    _instance = debugManagerObject.AddComponent<DebugManager>();
                }
                
                return _instance;
            }
        }
        private static DebugManager _instance;
        
        private Dictionary<Color,Material> _matCache;
        private Dictionary<string, PersistentPointData> _persistentPoints;

        private class PersistentPointData
        {
            public GameObject Sphere;
            public bool AutoDestroy;
            public int FramesSinceLastUpdate;
        }
        
        private void Awake()
        {
            _persistentPoints = new Dictionary<string, PersistentPointData>();
            _matCache = new Dictionary<Color, Material>();
        }

        private void Update()
        {
            foreach (var kvp in _persistentPoints.ToArray())
            {
                var key  = kvp.Key;
                var data = kvp.Value;

                if (!data.AutoDestroy)
                    continue;

                data.FramesSinceLastUpdate++;

                if (data.FramesSinceLastUpdate >= AUTO_DELETE_FRAMES)
                {
                    Destroy(data.Sphere);
                    _persistentPoints.Remove(key);
                }
            }
        }
        
        public void DrawPointPersistant(Vector3 point, string id, float size, Color color, bool autoDestroy)
        {
            if (_persistentPoints.TryGetValue(id, out var persistentPoint))
            {
                persistentPoint.Sphere.transform.position = point;
                persistentPoint.Sphere.transform.localScale = Vector3.one * size;
                ChangeColor(persistentPoint.Sphere, color);
                persistentPoint.AutoDestroy = autoDestroy;
                persistentPoint.FramesSinceLastUpdate = 0;
                
                return;
            }
            
            var sphere = SpawnPrimitive(PrimitiveType.Sphere, color);
            sphere.transform.localScale = Vector3.one * size;
            sphere.transform.position = point;
            sphere.name = "PersistentPoint_" + id;
            
            _persistentPoints.Add(id, new PersistentPointData()
            {
                Sphere = sphere,
                AutoDestroy = autoDestroy,
                FramesSinceLastUpdate = 0
            });
        }

        public void DrawPoint(Vector3 point, float size, Color color, float duration)
        {
            var sphere = SpawnPrimitive(PrimitiveType.Sphere, color);
            sphere.transform.localScale = Vector3.one * size;
            sphere.transform.position = point;
            sphere.name = "Point";

            if (duration > 0)
                Destroy(sphere, duration);
        }
        
        private static void ChangeColor(GameObject obj, Color color)
        {
            var mat = obj.GetComponent<MeshRenderer>().material;
            
            if (mat.HasProperty("_BaseColor"))
                mat.SetColor("_BaseColor", color);
            else if (mat.HasProperty("_Color"))
                mat.SetColor("_Color", color);
        }
        
        private GameObject SpawnPrimitive(PrimitiveType type, Color color)
        {
            var obj = GameObject.CreatePrimitive(type);
            obj.transform.parent = transform;
            
            var meshRenderer = obj.GetComponent<MeshRenderer>();
            DestroyImmediate(obj.GetComponent<Collider>());  
            meshRenderer.material = GetCachedMaterial(color);
            meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
            meshRenderer.receiveShadows = false;
            
            return obj;
        }
        
        private Material GetCachedMaterial(Color color)
        {
            if (_matCache.TryGetValue(color, out var mat))
                return mat;

            // 1) Try URP’s Unlit if we’re on Universal RP
            var rp = GraphicsSettings.currentRenderPipeline;
            Shader shader = null;
            if (rp != null && rp.GetType().Name.Contains("Universal"))
                shader = Shader.Find("Universal Render Pipeline/Unlit");

            // 2) Guaranteed-engine fallbacks
            shader ??= Shader.Find("Hidden/Internal-Colored");
            shader ??= Shader.Find("Sprites/Default");

            if (shader == null)
                throw new Exception("Debug shader not found; make sure at least “Sprites/Default” exists.");

            mat = new Material(shader);

            // set whichever color property the shader uses
            if (mat.HasProperty("_BaseColor"))
                mat.SetColor("_BaseColor", color);
            else if (mat.HasProperty("_Color"))
                mat.SetColor("_Color", color);

            _matCache[color] = mat;
            return mat;
        }
        
        private void OnDestroy()
        {
            foreach (var mat in _matCache.Values)
                Destroy(mat);
            
            _matCache.Clear();
        }
    }
}