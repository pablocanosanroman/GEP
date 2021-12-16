using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] m_Sounds;

    private void Awake()
    {
        foreach(Sound sound in m_Sounds)
        {
            sound.m_Source = gameObject.AddComponent<AudioSource>();
            sound.m_Source.outputAudioMixerGroup = sound.m_MixerGroup;
            sound.m_Source.clip = sound.m_Clip;

            sound.m_Source.volume = sound.m_Volume;
            sound.m_Source.pitch = sound.m_Pitch;
        }
    }
    public void Play(string name)
    {
        foreach(Sound sound in m_Sounds)
        {
            sound.m_ClipName = name;

            if(sound.m_ClipName == name)
            {
                sound.m_Source.Play();
            }
        }
    }
}

