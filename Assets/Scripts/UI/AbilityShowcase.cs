using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShowcase : MonoBehaviour
{
    private PlayerController playerController;
    private Transform shield;
    private Transform dash;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        shield = transform.Find("Shield");
        dash = transform.Find("Dash");
    }

    private void Update()
    {
        if(playerController.shieldEnabled == true && playerController.dashEnabled == false)
        {
            shield.gameObject.SetActive(true);
            dash.gameObject.SetActive(false);
        }
        else if(playerController.shieldEnabled == false && playerController.dashEnabled == true)
        {
            shield.gameObject.SetActive(false);
            dash.gameObject.SetActive(true);
        }
    }
}
