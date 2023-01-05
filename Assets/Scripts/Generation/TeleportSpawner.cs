using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpawner : MonoBehaviour
{
    public Vector3 rayPosition;
    public Vector3 rayDirection;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.TransformPoint(Vector3.forward), transform.TransformDirection(rayDirection), out hit, 55f))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
