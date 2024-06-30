using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

public class Conductor : MonoBehaviour
{

    public GameObject musicNotePrefab_RIGHT;
    public GameObject musicNotePrefab_LEFT;
    public GameObject musicNotePrefab_UP;
    public GameObject musicNotePrefab_DOWN;

    [NonSerialized] public GameObject note;

    //Some audio file might contain an empty interval at the start. We will substract this empty offset to calculate the actual position of the song
    public float songOffset;

    // The beat-locations of all music notes in the song should be entered in this array in Editor.
    // See the image: http://shinerightstudio.com/posts/music-syncing-in-rhythm-games/pic1.png
    //public float[] track;

    //The start positionY of notes
    public float startLineY;

    //The pos X of music notes
    public float posX;

    //The pos Z of music notes
    public float posZ;

    public float[] spawnX = new float[4];

    //The rotation on X
    float rotX;

    //The rotation on Y
    float rotY;

    //The rotation on Z
    float rotZ;

    //The finish line (the positionY where players hit) of the notes
    public float finishLineY;

    //The positionY where the notes should be destroyed
    public float removeLineY;

    //How many seconds each beat last. 
    public float secondsPerBeat;

    //How many beats are contained on the screen
    public float BeatsShownOnScreen = 4f;

    //This plays the beat
    public AudioSource beatAudioSource;

    //Current song Position
    [NonSerialized] public float songposition;

    //Next index for the array "track"
    private int indexOfNextNotes;

    //Keep reference of the MusicNotes which currently on Screen
    private Queue<NoteObject> notesOnScreen;

    //To record the time passed of the audio engine in the last frame. We use this to calculate the position of the song
    private float dsptimesong;

    private bool songStarted = false;

    [SerializeField] public int player;

    public void Initialized(float secondsPerBeat, float songOffset, int player)
    {
        this.secondsPerBeat = secondsPerBeat;
        this.player = player;
        this.songOffset = songOffset;

        indexOfNextNotes = 0;
    }

    void PlayerInputted()
    {
        //Start the song if it isn't started yet.
        if(!songStarted)
        {
            Debug.Log("Poniendo musica");
            songStarted = true;
            StartSong();
            return;
        }


        if(notesOnScreen.Count > 0)
        {
            NoteObject frontNote = notesOnScreen.Peek();
        }
    }

    void StartSong()
    {
        dsptimesong = (float)AudioSettings.dspTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        notesOnScreen = new Queue<NoteObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerInputted();
        }

        if (!songStarted) return;

        songposition = (float)(AudioSettings.dspTime - dsptimesong - songOffset);

        float beatToShow = songposition / secondsPerBeat + BeatsShownOnScreen;

        if(indexOfNextNotes < GameManager.Instance.track.Length && GameManager.Instance.track[indexOfNextNotes] < beatToShow)
        {
            int randomNote = Random.Range(1, 5);

            if(indexOfNextNotes % 3 == 0)
            {
                GameObject[] combo = new GameObject[2];
                float[] posX_combo = new float[2];
                Vector3[] rot_combo = new Vector3[2];

                int randomCombo = Random.Range(1, 7);

                switch(randomCombo)
                {
                    // <-               ->
                    case 1:
                        combo[0] = musicNotePrefab_LEFT;
                        rot_combo[0] = new Vector3( 90, 90, 0 );
                        posX_combo[0] = spawnX[0];

                        combo[1] = musicNotePrefab_RIGHT;
                        rot_combo[1] = new Vector3(270, 90, 0);
                        posX_combo[1] = spawnX[3];
                        break;

                    // UP              DOWN
                    case 2:
                        combo[0] = musicNotePrefab_UP;
                        rot_combo[0] = new Vector3(180, 90, 0);
                        posX_combo[0] = spawnX[1];

                        combo[1] = musicNotePrefab_DOWN;
                        rot_combo[1] = new Vector3(0, 90, 0);
                        posX_combo[1] = spawnX[2];
                        break;

                    //UP                RIGHT
                    case 3:
                        combo[0] = musicNotePrefab_UP;
                        rot_combo[0] = new Vector3(180, 90, 0);
                        posX_combo[0] = spawnX[1];

                        combo[1] = musicNotePrefab_RIGHT;
                        rot_combo[1] = new Vector3(270, 90, 0);
                        posX_combo[1] = spawnX[3];
                        break;

                    //LEFT              DOWN
                    case 4:
                        combo[0] = musicNotePrefab_LEFT;
                        rot_combo[0] = new Vector3(90, 90, 0);
                        posX_combo[0] = spawnX[0];

                        combo[1] = musicNotePrefab_DOWN;
                        rot_combo[1] = new Vector3(0, 90, 0);
                        posX_combo[1] = spawnX[2];
                        break;

                    //LEFT              UP
                    case 5:
                        combo[0] = musicNotePrefab_LEFT;
                        rot_combo[0] = new Vector3(90, 90, 0);
                        posX_combo[0] = spawnX[0];

                        combo[1] = musicNotePrefab_UP;
                        rot_combo[1] = new Vector3(180, 90, 0);
                        posX_combo[1] = spawnX[1];
                        break;

                    //DOWN              RIGHT
                    case 6:
                        combo[0] = musicNotePrefab_DOWN;
                        rot_combo[0] = new Vector3(0, 90, 0);
                        posX_combo[0] = spawnX[2];

                        combo[1] = musicNotePrefab_RIGHT;
                        rot_combo[1] = new Vector3(270, 90, 0);
                        posX_combo[1] = spawnX[3];
                        break;

                }

                for(int j = 0; j < combo.Length; ++j)
                {
                    NoteObject musicCombo = ((GameObject)Instantiate(combo[j], Vector3.zero, Quaternion.identity)).GetComponent<NoteObject>();

                    musicCombo.Initialize(this, startLineY, finishLineY, removeLineY, posX_combo[j], posZ, rot_combo[j].x, rot_combo[j].y, rot_combo[j].z, GameManager.Instance.track[indexOfNextNotes]);

                    notesOnScreen.Enqueue(musicCombo);
                }

                Debug.Log("Comboooooooooo");
            }
            else
            {
                switch (randomNote)
                {
                    case 1:
                        note = musicNotePrefab_LEFT;
                        rotX = 90;
                        rotY = 90;
                        rotZ = 0;

                        posX = spawnX[0];
                        break;
                    case 2:
                        note = musicNotePrefab_UP;
                        rotX = 180;
                        rotY = 90;
                        rotZ = 0;

                        posX = spawnX[1];
                        break;
                    case 3:
                        note = musicNotePrefab_DOWN;
                        rotX = 0;
                        rotY = 90;
                        rotZ = 0;

                        posX = spawnX[2];
                        break;
                    case 4:
                        note = musicNotePrefab_RIGHT;
                        rotX = 270;
                        rotY = 90;
                        rotZ = 0;

                        posX = spawnX[3];
                        break;

                }

                NoteObject musicNote = ((GameObject)Instantiate(note, Vector3.zero, Quaternion.identity)).GetComponent<NoteObject>();

                musicNote.Initialize(this, startLineY, finishLineY, removeLineY, posX, posZ, rotX, rotY, rotZ, GameManager.Instance.track[indexOfNextNotes]);

                notesOnScreen.Enqueue(musicNote);

            }
            Debug.Log("NOTA: " + indexOfNextNotes);
            indexOfNextNotes++;
        }

        if(notesOnScreen.Count > 0)
        {
            //NoteObject currNote = notesOnScreen.Peek();
            //if(currNote.transform.position.y >= finishLineY)
            //{
            //    notesOnScreen.Dequeue();
            //}
        }

        if(indexOfNextNotes >= GameManager.Instance.track.Length)
        {
            indexOfNextNotes = 0;
            GameManager.Instance.ShowResults();
        }
    }
}
