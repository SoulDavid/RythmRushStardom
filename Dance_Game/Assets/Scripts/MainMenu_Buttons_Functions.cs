using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Buttons_Functions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Option(int option)
    {
        Debug.Log("Hola");
        switch(option)
        {
            case 0:
                SceneManager.LoadScene("One_Player_Game");
                break;
            case 1:
                SceneManager.LoadScene("Game");
                break;
            case 2:
                SceneManager.LoadScene("Credits");
                break;
            case 3:
                Application.Quit();
                break;
            case 4:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

}
