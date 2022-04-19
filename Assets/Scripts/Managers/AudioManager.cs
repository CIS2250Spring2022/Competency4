using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System;

public class AudioManager : MonoBehaviour, IGameManager
{
    public List<Sound> sounds;

    public ManagerStatus status { get; private set; }

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

    public void Startup(NetworkService service)
    {
        Debug.Log("Audio Manager starting...");
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.onLoop;
        }
        status = ManagerStatus.Started;
    }
}
