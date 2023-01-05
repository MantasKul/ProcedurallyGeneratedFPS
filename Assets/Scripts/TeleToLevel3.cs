using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleToLevel3 : MonoBehaviour
{
    private Vector3 target = new Vector3(0f, -198f, 0f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Transform>().position = target;
        }
    }
}
