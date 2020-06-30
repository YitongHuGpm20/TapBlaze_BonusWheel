using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip[] audioList;
    private List<AudioSource> sources = new List<AudioSource>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < audioList.Length; i++)
        {
            sources.Add(new AudioSource());
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].clip = audioList[i];
        }
    }

    private void Update()
    {
        GetComponent<AudioSource>().mute = !GameManager.bgmOn;
        for (int i = 0; i < audioList.Length; i++)
            sources[i].mute = !GameManager.sfxOn;
    }

    public void PlaySound(int index)
    {
        sources[index].Play();
    }

    public void StopSound(int index)
    {
        sources[index].Stop();
    }
}
