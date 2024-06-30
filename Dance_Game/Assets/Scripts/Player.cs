using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioSource song;
    public bool start;

    // Start is called before the first frame update
    void Start()
    {
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!start)
            {
                start = true;
                StartCoroutine(WaitForSong());
            }

        }
    }

    private IEnumerator WaitForSong()
    {
        yield return new WaitForSeconds(0.4f);
        GameManager.Instance.Play(song);
    }
}
