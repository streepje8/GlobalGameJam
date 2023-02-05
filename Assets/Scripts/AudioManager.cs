using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Camera highResCam;
    public List<Sound> sounds = new List<Sound>();
    private Dictionary<string, Sound> bakedLookup = new Dictionary<string, Sound>();
    private List<AudioSource> activeSources = new List<AudioSource>();
    private Dictionary<string, AudioSource> activeSounds = new Dictionary<string, AudioSource>();
    void Awake()
    {
        Instance = this;
        foreach (var sound in sounds)
        {
            bakedLookup.Add(sound.path.ToLower(),sound);
        }
    }

    public void PlaySoundGlobal(string soundPath, bool looping = false)
    {
        if (bakedLookup.TryGetValue(soundPath.ToLower(), out Sound sound))
        {
            if(activeSounds.ContainsKey(soundPath.ToLower())) StopSound(soundPath);
            GameObject soundObj = new GameObject("Audio Player");
            soundObj.transform.SetParent(highResCam.transform);
            soundObj.transform.position = highResCam.transform.position;
            soundObj.transform.rotation = highResCam.transform.rotation;
            AudioSource source = soundObj.AddComponent<AudioSource>();
            if (looping)
            {
                source.clip = sound.clip;
                source.loop = true;
                source.Play();
            }
            else
            {
                source.PlayOneShot(sound.clip);
            }
            activeSources.Add(source);
            activeSounds.Add(soundPath.ToLower(),source);
        }
    }

    public void StopSound(string soundPath)
    {
        if (activeSounds.TryGetValue(soundPath.ToLower(), out AudioSource value))
        {
            Destroy(value.gameObject);
            activeSounds.Remove(soundPath.ToLower());
        }
    }

    public void PlaySoundLocation(string soundPath, Vector3 position)
    {
        if (bakedLookup.TryGetValue(soundPath.ToLower(), out Sound sound))
        {
            if(activeSounds.ContainsKey(soundPath.ToLower())) StopSound(soundPath);
            GameObject soundObj = new GameObject("Audio Player");
            soundObj.transform.position = position;
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.PlayOneShot(sound.clip);
            activeSources.Add(source);
            activeSounds.Add(soundPath.ToLower(),source);
        }
    }
    
    void Update()
    {
        for(int i = activeSources.Count - 1; i >= 0; i--)
        {
            AudioSource activeSource = activeSources[i];
            if (!activeSource.isPlaying)
            {
                Destroy(activeSource.gameObject);
                activeSources.Remove(activeSource);
                activeSounds.Remove(activeSounds.FirstOrDefault(x => x.Value == activeSource).Key);
            }
        }
    }
}
[System.Serializable]
public struct Sound
{
    public string path;
    public AudioClip clip;
}