using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<AudioManager>();
            }
            return instance;
        }
    }



    public AudioSource PlayAudio(AudioClip clip, float volume)
    {
        GameObject sourceObj = new GameObject(clip.name);
        sourceObj.transform.SetParent(this.transform);
        AudioSource source = sourceObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.Play();
        StartCoroutine(PlayAudio(source));
        return source;
    }

    IEnumerator PlayAudio(AudioSource source)
    {
        yield return null;
        while (source.isPlaying) yield return null;
        Destroy(source.gameObject);
    }
}
