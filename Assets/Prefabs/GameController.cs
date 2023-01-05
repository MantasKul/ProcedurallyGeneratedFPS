using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<GameObject> toDisable;
    public GameObject gameOverText;

    public GameObject pauseMenu;
    private PlayerController playerController;

    private bool gameOver = false;
    private bool paused = false;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        foreach (var d in toDisable)
            d.SetActive(false);
        gameOverText.SetActive(true);
        gameOver = true;
    }

    private void Update()
    {
        if (gameOver)
            if (Input.GetKeyDown("r"))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        if (Input.GetKeyDown(KeyCode.P) && paused == false)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            foreach (var d in toDisable)
                d.SetActive(false);

            playerController.enabled = false;
            pauseMenu.SetActive(true);
            paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && paused == true)
        {
            Resume();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        foreach (var d in toDisable)
            d.SetActive(true);

        playerController.enabled = true;
        pauseMenu.SetActive(false);
        paused = false;
    }
}
