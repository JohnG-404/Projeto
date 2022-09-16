using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioMixer  AuM;

    public void SetVolume(float volume)
    {
        AuM.SetFloat("volume", volume);
    } 
}
