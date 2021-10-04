using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioTrigger
{
    [SerializeField] string[] soundName = default;
    [SerializeField] [Range(0, 1)] float volume = .2f;
    [SerializeField] float range = 10f;
    [SerializeField] [Range(0, 1)] float stereo = .2f;
    [SerializeField] float minPitch = .95f;
    [SerializeField] float maxPitch = 1.05f;

    public void Play(Vector3 position)
    {
        if (soundName.Length == 0) return;

        GameObject newSource = new GameObject();
        AudioSource audioSource = newSource.AddComponent<AudioSource>();

        audioSource.volume = volume;
        audioSource.minDistance = 1f;
        audioSource.maxDistance = range;
        audioSource.spatialBlend = stereo;
        audioSource.pitch = minPitch + Random.value * (maxPitch - minPitch);
        audioSource.clip = AudioManager.GetSound(soundName[Random.Range(0, soundName.Length)]);
        
        audioSource.Play();
        MonoBehaviour.Destroy(newSource, audioSource.clip.length + .5f);
    }
}
