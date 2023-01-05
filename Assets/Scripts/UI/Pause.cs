using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void Resume()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameController>().Resume();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
