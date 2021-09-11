using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWithRandomSound : MonoBehaviour
{
    public AudioSource _as;
    public AudioClip[] audioClipArray;

    void Awake()
    {
        _as = GetComponent<AudioSource> ();
    }

    // Start is called before the first frame update
    void Start()
    {
        _as.clip = audioClipArray[Random.Range(0,audioClipArray.Length)];
        _as.PlayOneShot(_as.clip);
    }
}
