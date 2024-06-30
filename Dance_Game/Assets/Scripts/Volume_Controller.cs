using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Volume_Controller : MonoBehaviour
{
    [SerializeField] private GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("volume", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenVolumeMenu()
    {
        Panel.SetActive(true);
    }

    public void CloseVolumeMenu()
    {
        Panel.SetActive(false);
    }

    public void changeVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("volume", newVolume);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
}
