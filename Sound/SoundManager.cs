using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AtanUtils.Sound
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        private List<(Transform, AudioSource)> dynamicSources;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            
            dynamicSources = new List<(Transform, AudioSource)>();
        }
        
        private void Update()
        {
            for (int i = dynamicSources.Count - 1; i >= 0; i--)
            {
                var (target, source) = dynamicSources[i];

                if (source == null || target == null)
                {
                    dynamicSources.RemoveAt(i);
                    continue;
                }

                source.transform.position = target.position;
            }
        }
        
        public void Play(SoundObj s, bool ignoreExisting = true)
        {
            PlayInternal(s, ignoreExisting, false, Vector3.zero);
        }
        
        public void Play3D(SoundObj s, Vector3 pos, bool ignoreExisting = true)
        {
            PlayInternal(s, ignoreExisting, true, pos);
        }
        
        public void PlayDynamic(SoundObj s, Transform target, bool ignoreExisting = true)
        {
            var source = PlayInternal(s, ignoreExisting, true, target.position);
            
            if (source == null)
                return;
            
            dynamicSources.Add((target, source));
        }

        private AudioSource PlayInternal(SoundObj s, bool ignoreExisting, bool is3D, Vector3 pos)
        {
            for (int i = s.source.Count - 1; i > -1; i--)
            {
                if (s.source[i] == null)
                    s.source.RemoveAt(i);
            }

            if (!ignoreExisting && s.source.Count > 0)
                return null;

            GameObject g = new GameObject
            {
                transform =
                {
                    parent = transform,
                    position = pos,
                    localRotation = Quaternion.identity,
                    name = s.name + " source"
                }
            };

            AudioSource localSource = g.AddComponent<AudioSource>();
            s.source.Add(localSource);
            var clip = GetClip(s);
            
            localSource.clip = clip;
            localSource.volume = s.volume;
            localSource.pitch = s.pitch;
            localSource.loop = s.loop;

            localSource.outputAudioMixerGroup = s.group;
            localSource.rolloffMode = AudioRolloffMode.Linear;
            localSource.maxDistance = s.maxDis;
            localSource.spatialBlend = is3D ? 1f : 0f;

            localSource.Play();

            if (!s.loop)
                Destroy(g, clip.length + 0.1f);
            
            return localSource;
        }

        private AudioClip GetClip(SoundObj s)
        {
            return s.loadMode switch
            {
                LoadMode.First => s.clips[0],
                LoadMode.Random => s.clips[UnityEngine.Random.Range(0, s.clips.Length)],
                LoadMode.Sequential => GetSequentialClip(s),
                _ => throw new NotImplementedException()
            };
        }
        
        private static AudioClip GetSequentialClip(SoundObj s)
        {
            if (s.clipIndex >= s.clips.Length)
                s.clipIndex = 0;
            
            return s.clips[s.clipIndex++];
        }
        
        public void Stop (SoundObj s)
        {
            if (s == null)
                return;
            
            if (s.source.Count < 1)
                return;

            foreach (AudioSource sources in s.source)
            {
                if (sources == null)
                    continue;

                sources.Stop();
                Destroy(sources.gameObject);
            }

            s.source.Clear();
        }
        
        public bool IsPlaying (SoundObj s)
        {
            if (s == null)
                return false;
            
            for (int i = s.source.Count - 1; i > -1; i--)
            {
                if (s.source[i] == null)
                    s.source.RemoveAt(i);
            }

            if (s.source.Count > 0)
                return true;
            
            return false;
        }
    }
}