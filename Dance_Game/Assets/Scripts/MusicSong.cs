using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSong : MonoBehaviour
{
    private float TolerationOffset;
    private float SecondsPerBeat;
    public float TrackLimit;

    public float[] SongCaracteristics(int option)
    {
        switch(option)
        {
            case 0:
                TolerationOffset = 0.7f;
                SecondsPerBeat = 0.8f;
                TrackLimit = 159;

                break;
        }


        float[] Values = { TolerationOffset, SecondsPerBeat };
        return Values; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
