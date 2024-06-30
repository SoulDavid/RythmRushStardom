using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
    private Scrollbar myScrollbar;
    [SerializeField] private GameObject g_Scrollbar;

    public List<Image> LevelThumbnails;
    public float angles = 0;

    private enum ImageEnum {Tsuna, Today, Color_Parade, StreetKnowledge};
    [SerializeField] ImageEnum ScrollImage;

    [SerializeField] private GameObject musicProof;
    private AudioSource AS_Proof;

    // Start is called before the first frame update
    void Start()
    {
        myScrollbar = g_Scrollbar.GetComponent<Scrollbar>();
        AS_Proof = musicProof.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (myScrollbar.value < 0.4)
        {
            AS_Proof.clip = GameManager.Instance.music[0];
            ResetMusic(ImageEnum.Tsuna);
            ResetAngle(ImageEnum.Tsuna);
        }
        else if (myScrollbar.value > 0.4 && myScrollbar.value < 0.65)
        {
            AS_Proof.clip = GameManager.Instance.music[1];
            ResetMusic(ImageEnum.Today);
            ResetAngle(ImageEnum.Today);
        }
        else if (myScrollbar.value > 0.65 && myScrollbar.value < 0.86)
        {
            AS_Proof.clip = GameManager.Instance.music[2];
            ResetMusic(ImageEnum.Color_Parade);
            ResetAngle(ImageEnum.Color_Parade);
        }
        else if (myScrollbar.value > 0.86)
        {
            AS_Proof.clip = GameManager.Instance.music[3];
            ResetMusic(ImageEnum.StreetKnowledge);
            ResetAngle(ImageEnum.StreetKnowledge);
        }

        for (int i = 0; i < LevelThumbnails.Count; ++i)
        {
            if (i == (int)ScrollImage)
            {
                LevelThumbnails[i].transform.localScale = Vector2.Lerp(LevelThumbnails[i].transform.localScale, new Vector2(1.5f, 1.5f), 0.1f);
                LevelThumbnails[i].transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angles);

            }
            else
            {
                LevelThumbnails[i].transform.localScale = Vector2.Lerp(LevelThumbnails[i].transform.localScale, new Vector2(1, 1), 0.1f);
                LevelThumbnails[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        
        angles += 0.5f;
    }

    private void ResetAngle(ImageEnum Value)
    {
        if(Value != ScrollImage)
        {
            ScrollImage = Value;
            angles = 0;
        }
    }

    private void ResetMusic(ImageEnum Value)
    {
        if(Value != ScrollImage)
        {
            ScrollImage = Value;
            AS_Proof.Play();
        }
    }

}
