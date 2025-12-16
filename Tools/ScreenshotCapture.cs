using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace AtanUtils.Tools
{
    public class ScreenshotCapture : MonoBehaviour
    {
        public Camera targetCamera;
        public int width = 1920;
        public int height = 1080;
        public string outputPath = "Screenshots";

        [ContextMenu("Capture Screenshot")]
        public void CaptureScreenshot()
        {
            string fullPath = Path.Combine(Application.dataPath, "..", outputPath);
            Directory.CreateDirectory(fullPath);

            RenderTexture rt = new RenderTexture(width, height, 24);
            targetCamera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);

            targetCamera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenShot.Apply();

            targetCamera.targetTexture = null;
            RenderTexture.active = null;
            DestroyImmediate(rt);

            byte[] bytes = screenShot.EncodeToPNG();
            string filename = Path.Combine(fullPath, $"Screenshot_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png");
            File.WriteAllBytes(filename, bytes);

            Debug.Log($"Screenshot saved to: {filename}");
        }
        
  [ContextMenu("Capture Full Resolution (Scene + Overlay UI)")]
        public void CaptureWithOverlays()
        {
            StartCoroutine(CaptureWithOverlaysCoroutine());
        }

        private IEnumerator CaptureWithOverlaysCoroutine()
        {
            // 1) Wait until Unity has drawn cameras & overlay UI
            yield return new WaitForEndOfFrame();

            // 2) Prepare file output folder
            string fullPath = Path.Combine(Application.dataPath, "..", outputPath);
            Directory.CreateDirectory(fullPath);

            // 3) Gather and switch all Overlay canvases
            var overlayCanvases = new List<Canvas>();
            var originalSettings = new List<(Canvas canvas, RenderMode mode, Camera cam, float pd)>();
            foreach (var c in FindObjectsByType<Canvas>(FindObjectsSortMode.None))
            {
                if (c.renderMode == RenderMode.ScreenSpaceOverlay)
                {
                    overlayCanvases.Add(c);
                    originalSettings.Add((c, c.renderMode, c.worldCamera, c.planeDistance));
                    c.renderMode      = RenderMode.ScreenSpaceCamera;
                    c.worldCamera     = targetCamera;
                    c.planeDistance   = 1f;
                }
            }

            // 4) Render scene + now-cameraized UI into RT
            var rt = new RenderTexture(width, height, 24);
            targetCamera.targetTexture = rt;
            targetCamera.Render();

            // 5) Read back pixels
            RenderTexture.active = rt;
            var screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenshot.Apply();

            // 6) Cleanup RT and restore camera
            targetCamera.targetTexture = null;
            RenderTexture.active       = null;
            DestroyImmediate(rt);

            // 7) Restore Overlay canvases to original state
            foreach (var (c, mode, cam, pd) in originalSettings)
            {
                c.renderMode    = mode;
                c.worldCamera   = cam;
                c.planeDistance = pd;
            }

            // 8) Save PNG
            byte[] bytes = screenshot.EncodeToPNG();
            string filename = Path.Combine(fullPath,
                $"Screenshot_Full_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png");
            File.WriteAllBytes(filename, bytes);
            Debug.Log($"[Full] Saved screenshot to: {filename}");
        }
    }
}