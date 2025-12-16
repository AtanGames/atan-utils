using System;
using AtanUtils.Base;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AtanUtils.UI.Tools
{
    public class SceneFade : InstanceMonoBehaviour<SceneFade>
    {
        private CanvasGroup canvasGroup;

        public float fadeInDuration = 0.5f;
        public float fadeDuration;

        private bool isTransitioning;

        private bool _transitionStarted;
        
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = false;
        }

        private void Update()
        {
            if (!_transitionStarted)
            {
                _transitionStarted = true;
                Tween.Alpha(canvasGroup, 0f, fadeInDuration, Ease.InQuad, useUnscaledTime: true);
            }
        }

        public static void ReloadScene()
        {
            Instance._LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static void QuitGame()
        {
            Instance._LoadScene(-1);
        }
        
        public static void LoadScene(int scene)
        {
            Instance._LoadScene(scene);
        }

        public static void LoadNextLevel()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            if (sceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
                Instance._LoadScene(sceneIndex + 1);
            else
                Instance._LoadScene(0);
        }
        
        private void _LoadScene(int scene)
        {
            if (isTransitioning)
                return;
            
            isTransitioning = true;

            if (scene == -1)
            {
                canvasGroup.blocksRaycasts = true;
                Tween.Alpha(canvasGroup, 1f, fadeDuration, useUnscaledTime: true).OnComplete(Application.Quit);
                return;
            }

            AsyncOperation op = SceneManager.LoadSceneAsync(scene);
            canvasGroup.blocksRaycasts = true;
            op.allowSceneActivation = false;
            
            Tween.Alpha(canvasGroup, 1f, fadeDuration, useUnscaledTime: true).OnComplete(() =>
            {
                op.allowSceneActivation = true;
            });
        }
    }
}