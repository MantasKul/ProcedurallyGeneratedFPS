using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleToVictory : MonoBehaviour
{
    public GameObject canvas;
    public GameObject WinCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canvas.gameObject.SetActive(false);
            WinCanvas.gameObject.SetActive(true);

            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;

            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
        }
    }
}
