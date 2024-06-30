using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    //Reference of the conductor
    public Conductor conductor;

    public float startY;
    public float endY;
    public float removeLineY;
    public float beat;

    public void Initialize(Conductor conductor, float startY, float endY, float removeLineY, float posX, float posZ, float rotX, float rotY, float rotZ, float beat)
    {
        this.conductor = conductor;
        this.startY = startY;
        this.endY = endY;
        this.beat = beat;
        this.removeLineY = removeLineY;

        transform.position = new Vector3(posX, startY, posZ);
        transform.eulerAngles = new Vector3(rotX, rotY, rotZ);
    }

    public Material Miss;

    public MeshRenderer mR;

    GameObject GCollider;

    // Start is called before the first frame update
    void Start()
    {
        mR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(transform.position.x, startY + (endY - startY) * (1f - (beat - conductor.songposition / conductor.secondsPerBeat) / conductor.BeatsShownOnScreen), transform.position.z);

        if (Input.GetKeyDown(keyToPress))
        {
            conductor.beatAudioSource.Play();

            if (canBePressed)
            {
                gameObject.SetActive(false);
                GameManager.Instance.NoteHit(GCollider, conductor.player);
            }
        }

        if (transform.position.y < removeLineY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Activator"))
        {
            GCollider = other.gameObject;
            canBePressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Activator"))
        {
            canBePressed = false;

            GameManager.Instance.NoteMiss(conductor.player);

            mR.material = Miss;
        }
    }
}
