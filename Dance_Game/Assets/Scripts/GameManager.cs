using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get => instance;  }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public int[] score = { 0, 0 };
    public Text[] scoreText = new Text[2];

    public int[] consecutiveHit = { 0, 0 };
    public int[] Multipliyer = { 1, 1 };
    public Text[] MultiplyerText = new Text[2];

    public int[] NotesHits = new int[2];

    public GameObject[] effectsText = new GameObject[3];
    public GameObject[] borderEffect = new GameObject[2];

    public int option = 0;

    public SpriteRenderer sR_Background;
    public GameObject background;
    public Sprite[] spritesBackgrounds = new Sprite[3];

    // The beat-locations of all music notes in the song should be entered in this array in Editor.
    // See the image: http://shinerightstudio.com/posts/music-syncing-in-rhythm-games/pic1.png
    public float[] track;
    public int[] trackLimit;

    public AudioSource menuMusic;

    //How many seconds each beat last. 
    public float[] secondsPerBeat;

    public float[] songOffset;

    public GameObject[] playersConductor;

    [SerializeField] public List<AudioClip> music = new List<AudioClip>();
    [SerializeField] private GameObject AudioSource_GO;

    [SerializeField] private GameObject ResultsMenu;
    [SerializeField] private Text[] textResults = new Text[2];

    public enum scenes { One_Player, Two_Players };
    public scenes Actual_Scene;

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");

        sR_Background = background.GetComponent<SpriteRenderer>();
        option = 0;

        ResultsMenu.SetActive(false);

        if (playersConductor.Length > 1)
            Actual_Scene = scenes.Two_Players;
        else
            Actual_Scene = scenes.One_Player;

        for (int i = 0; i < borderEffect.Length; ++i)
            borderEffect[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowResults()
    {
        StartCoroutine(corrutineShowResults());
    }

    private IEnumerator corrutineShowResults()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < textResults.Length; ++i)
        {
            textResults[i].text = score[i] + "";
        }
        ResultsMenu.SetActive(true);
    }

    public void CreateTrack(int optionSelected)
    {
        switch (optionSelected)
        {
            case 0:
                track = new float[trackLimit[0]];

                track[0] = 5f;

                for (int i = 1; i < trackLimit[0]; ++i)
                {
                    track[i] = track[i - 1] + 1f;
                }
                break;

            case 1:
                track = new float[trackLimit[1]];

                track[0] = 4.7f;

                for (int i = 1; i < trackLimit[1]; ++i)
                {
                    track[i] = track[i - 1] + 1.25f;
                }
                break;

            case 2:
                track = new float[trackLimit[2]];

                track[0] = 5f;

                for (int i = 1; i < 78; ++i)
                {
                    track[i] = track[i - 1] + 1f;
                }

                for (int i = 78; i < trackLimit[2]; ++i)
                {
                    track[i] = track[i - 1] + .75f;
                }
                break;

            case 3:
                track = new float[trackLimit[3]];

                track[0] = 5f;

                for (int i = 1; i < track.Length; ++i)
                {
                    track[i] = track[i - 1] + 1.2f;
                }

                break;

        }

        for (int i = 0; i < playersConductor.Length; ++i)
        {
            playersConductor[i].GetComponent<Conductor>().Initialized(secondsPerBeat[optionSelected], songOffset[optionSelected], i);
        }

        sR_Background.sprite = spritesBackgrounds[optionSelected];
        AudioSource_GO.GetComponent<AudioSource>().clip = music[optionSelected];
    }


    public void Play(AudioSource song)
    {
        song.Play();
    }


    public void NoteHit(GameObject cube, int player)
    {
        score[player] += 10 * Multipliyer[player];
        consecutiveHit[player]++;

        if (consecutiveHit[player] % 10 == 0 && Multipliyer[player] < 5)
            Multipliyer[player]++;

        scoreText[player].text = score[player] + "";
        MultiplyerText[player].text = "x" + Multipliyer[player];
        NotesHits[player]++;

        if(Multipliyer[player] > 3)
        {
            borderEffect[player].SetActive(true);
        }

        int x = Random.Range(0, 3);

        switch(x)
        {
            case 0:
                Instantiate(effectsText[0], new Vector3(cube.transform.position.x, cube.transform.position.y, cube.transform.position.z - 1.5f), effectsText[0].transform.rotation);
                break;
            case 1:
                Instantiate(effectsText[1], new Vector3(cube.transform.position.x, cube.transform.position.y, cube.transform.position.z - 1.5f), effectsText[1].transform.rotation);
                break;
            case 2:
                Instantiate(effectsText[2], new Vector3(cube.transform.position.x, cube.transform.position.y, cube.transform.position.z - 1.5f), effectsText[2].transform.rotation);
                break;
        }
        
        Debug.Log("Hit On Time");
    }

    public void NoteMiss(int player)
    {
        if (score[player] > 0)
        {
            score[player] -= 10;
            scoreText[player].text = score[player] + "";
        }

        consecutiveHit[player] = 0;
        borderEffect[player].SetActive(false);
        Multipliyer[player] = 1;
        MultiplyerText[player].text = "x" + Multipliyer[player];
        Debug.Log("Missed Note");
    }

    public int getOption()
    {
        return option;
    }
}
