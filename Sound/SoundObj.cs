using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AtanUtils.Sound
{
    [CreateAssetMenu(fileName = "SoundObj", menuName = "Audio/SoundObj")]
    public class SoundObj : ScriptableObject
    {
        public AudioClip[] clips;
        public LoadMode loadMode = LoadMode.First;
        
        [HideInInspector]
        public List<AudioSource> source = new List<AudioSource>();

        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(-3f, 3f)]
        public float pitch = 1f;

        public bool loop;
        public float maxDis;

        public AudioMixerGroup group;

        [HideInInspector]
        public int clipIndex;
        
        public void Play(bool ignoreExisting = true) => SoundManager.Instance.Play(this, ignoreExisting);
        public void Play3D(Vector3 pos, bool ignoreExisting = true) => SoundManager.Instance.Play3D(this, pos, ignoreExisting);
        public void PlayDynamic(Transform target, bool ignoreExisting = true) => SoundManager.Instance.PlayDynamic(this, target, ignoreExisting);
    }
    
    public enum LoadMode
    {
        First, Random, Sequential
    }
}