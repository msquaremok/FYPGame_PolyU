using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuControl : MonoBehaviour
{
    //pause menu related
    public GameObject pauseMenu;
    public bool pauseMenuOpened = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuOpened == false)
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            pauseMenuOpened = true;
            return;
        } else

        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuOpened == true)
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            pauseMenuOpened = false;
            return;
        }

    }
}
