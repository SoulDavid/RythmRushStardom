using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioSource musicMenu;

    [SerializeField] private GameObject buttonPlay;
    [SerializeField] private GameObject title;

    [SerializeField] private GameObject Container;

    private Scrollbar myScrollbar;
    [SerializeField] private GameObject g_Scrollbar;

    [SerializeField] private GameObject background_GO;
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject score;

    // Start is called before the first frame update
    void Start()
    {
        buttonPlay.SetActive(true);
        title.SetActive(true);
        Container.SetActive(false);

        background_GO.SetActive(true);
        score.SetActive(false);

        myScrollbar = g_Scrollbar.GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Saliendo");
            Application.Quit();
        }
    }

    public void PlayMusic()
    {
        musicMenu.Play();
    }

    public void ReduceMusicVolume(float volume)
    {
        musicMenu.volume = volume;
    }

    public void PlayButton()
    {
        buttonPlay.SetActive(false);
        title.SetActive(false);
        Container.SetActive(true);
    }

    public void SongChoosedButton()
    {
        if (myScrollbar.value < 0.4)
        {
            GameManager.Instance.option = 0;
            GameManager.Instance.CreateTrack(0);
        }
        else if (myScrollbar.value > 0.4 && myScrollbar.value < 0.65)
        {
            GameManager.Instance.option = 1;
            GameManager.Instance.CreateTrack(1);
        }
        else if (myScrollbar.value > 0.65 && myScrollbar.value < 0.86)
        {
            GameManager.Instance.option = 2; 
            GameManager.Instance.CreateTrack(2);
        }
        else if (myScrollbar.value > 0.86)
        {
            GameManager.Instance.option = 3;
            GameManager.Instance.CreateTrack(3);
        }
        else
            Debug.Log("No se puede");

        background_GO.SetActive(false);
        Container.SetActive(false);
        game.SetActive(true);
        score.SetActive(true);
        GameManager.Instance.menuMusic.Stop();
    }
}
