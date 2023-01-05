using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleToLevel2 : MonoBehaviour
{
    private Vector3 target = new Vector3(0f, -98f, 0f);

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Transform>().position = target;
        }
    }
}
