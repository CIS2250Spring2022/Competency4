using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System;

public class AudioManager : MonoBehaviour, IGameManager
{
    public List<Sound> sounds;

    public ManagerStatus status { get; private set; }
<<<<<<< Updated upstream:Assets/Scripts/Managers/AudioManager.cs
=======
    private NetworkService _network;
>>>>>>> Stashed changes:Assets/Scripts/AudioManager.cs

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
<<<<<<< Updated upstream:Assets/Scripts/Managers/AudioManager.cs
        Debug.Log("Audio Manager starting...");
=======
        status = ManagerStatus.Initializing;
        Debug.Log("Data manager starting...");

        _network = service;
>>>>>>> Stashed changes:Assets/Scripts/AudioManager.cs
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.onLoop;
        }
<<<<<<< Updated upstream:Assets/Scripts/Managers/AudioManager.cs
        status = ManagerStatus.Started;
=======
        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        status = ManagerStatus.Started;
        
>>>>>>> Stashed changes:Assets/Scripts/AudioManager.cs
    }
}
