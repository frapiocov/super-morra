using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    [Header("Sound Settings")]
    private AudioSource audio;
    public Image soundPrefab;
    public Sprite yesSoundSprite;
    public Sprite noSoundSprite;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        soundPrefab.sprite = yesSoundSprite;
    }

    public void SetMusic()
    {
        if (audio.enabled)
        {
            soundPrefab.sprite = noSoundSprite;
            audio.enabled = false;
        } else
        {
            soundPrefab.sprite = yesSoundSprite;
            audio.enabled = true;
        }
        
    }

}
