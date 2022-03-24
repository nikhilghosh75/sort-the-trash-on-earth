using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For UnityEvents

public class AudioManagerCommands : MonoBehaviour
{
    public void Play(string soundName)
    {
        AudioManager.instance.Play(soundName);
    }

    public void Stop(string soundName)
    {
        AudioManager.instance.Stop(soundName);
    }
}
