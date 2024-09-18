using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        //오디오 파일
        public AudioClip clip;

        [Range(0f, 2f)]
        public float volume;

        [Range(0.1f, 3f)]
        public float pitch;

        public bool loop;

        //오디오를 재생해주는 컴포넌트 
        [HideInInspector]
        public AudioSource source;
    }

    public Sound[] sounds;

    public static SoundManager instance;

    //FindObjectOfType<SoundManager>().Play("Click");

    void Awake()
    {
        instance = this;

        for(int i = 0; i < sounds.Length;i++)
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;

            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
            sounds[i].source.loop = sounds[i].loop;
        }

        SoundManager.instance.Play("BGM");
    }

    public void Play(string name)
    {
        //배열에서 이름이 일치하는 사운드를 찾음 // sound라는 변수는 일시적으로 사용
        
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            return;
        }

        s.source.Play();
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);

        if (s == null)
        {
            return;
        }

        s.source.Stop();
    }
}
