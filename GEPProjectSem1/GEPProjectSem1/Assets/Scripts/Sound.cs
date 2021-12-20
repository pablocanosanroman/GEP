using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string m_ClipName;
    public AudioClip m_Clip;
    

    [Range(0f, 1f)]
    public float m_Volume;

    [Range(0.1f, 2f)]
    public float m_Pitch;

    public AudioMixerGroup m_MixerGroup;

    public bool m_Loop;

    [HideInInspector]
    public AudioSource m_Source;
}
