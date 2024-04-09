using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] AudioMixer masterMixer;
    // Start is called before the first frame update
    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float val)
    {
        if (val<1)
        {
            val = 0.001f;
        }
        masterSlider.value = val;
        PlayerPrefs.SetFloat("SavedMasterVolume", val);
        masterMixer.SetFloat("Master Volume",Mathf.Log10(val/100)*20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(masterSlider.value);
    }
}
