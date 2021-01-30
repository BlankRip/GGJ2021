using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudioSelector : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] AudioSource menuBGM;

    private void Start()
    {
        menuBGM.clip = SelectRandom();
        menuBGM.Play();
    }

    public AudioClip SelectRandom()
    {
        return audioClips[Random.Range(0,audioClips.Count)];
    }
}
