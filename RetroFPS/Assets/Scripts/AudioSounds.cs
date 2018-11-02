using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioSounds : MonoBehaviour
{

    public AudioSource adSource;
    public AudioClip[] adClips;

    AudioClip playAudioSequentially()
    {
        //yield return null;

        return adClips[Random.Range(0, adClips.Length)];
    }
    void Start()
    {
        adSource = FindObjectOfType<AudioSource>();
        adSource.loop = false;

    }
    void Update()
    {
        if(!adSource.isPlaying)
        {
            adSource.clip = playAudioSequentially();
            adSource.Play();
        }
    }
}
//public class AudioSounds : MonoBehaviour {

//    public AudioClip[] songs;


//    void Update()
//    {
//        if (GetComponent<AudioSource>().isPlaying == false)
//        {
//            for(int i=0; i<songs.Length; i++)
//            {
//                GetComponent<AudioSource>().clip = songs[i];
//                GetComponent<AudioSource>().Play();

//                if (i == songs.Length)
//                    i = 0;
//            }

//        }
//    }
//}
