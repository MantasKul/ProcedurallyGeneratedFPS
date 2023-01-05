using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTele : MonoBehaviour
{
    Vector3 nextRoomPos;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = transform.TransformPoint(nextRoomPos);
        }
    }
}
