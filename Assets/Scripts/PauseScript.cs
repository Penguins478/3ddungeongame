using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject inventoryMenuUI;

    // Update is called once per frame
    void Update()
    {
        // Is p for testing reasons. For final release, set it to escape.

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P is pressed");
            if (IsPaused)
            {
                Resume();
                Debug.Log("Unpaused");
                pauseMenuUI.SetActive(false);
            }
            else
            {
                Pause();
                Debug.Log("Paused");
                pauseMenuUI.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I is pressed");
            if (IsPaused)
            {
                Resume();
                Debug.Log("inv closed");
                inventoryMenuUI.SetActive(false);
            }
            else
            {
                Pause();
                Debug.Log("inv open");
                inventoryMenuUI.SetActive(true);
            }
        }
    }

    void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }
}
