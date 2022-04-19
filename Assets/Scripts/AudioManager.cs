using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System;

public class AudioManager : MonoBehaviour
{
    public List<Sound> sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.onLoop;
        }
    }

    public void Play(string Name)
    {
        Sound sound = sounds.Find(s => s.name == Name);
        try
        {
            sound.source.Play();
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Sound: " + Name + " not found.");
        }
    }
}
