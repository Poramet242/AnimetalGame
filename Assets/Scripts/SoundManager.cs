using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
     void Awake()
    {
          foreach (Sound s in sounds )
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            

        }

    }
    private void Start()
    {
        PlaySounded("Theam");
    }
    public void PlaySounded (string name )
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //s.source.Play();
        s.source.PlayOneShot(s.clip);
    }
    public void StopSounded(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    public void HalfDownSounded(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume /= 2 ;      
    }
}
