using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenu : MonoBehaviour
{
    //Constss

    //Variables
    public KeyCode levelRestartKey = KeyCode.R;
    public KeyCode applicationQuitKey = KeyCode.Escape;


    //Methods
    
    void Update()
    {
        if (Input.GetKeyDown(levelRestartKey)) {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

        if (Input.GetKeyDown(applicationQuitKey)) {

            Application.Quit();

        }
    }
}
