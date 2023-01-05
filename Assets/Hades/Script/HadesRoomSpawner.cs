using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesRoomSpawner : MonoBehaviour
{
    public GameObject room;
    public Vector3 pos;

    private GameObject levelController;

    void Start()
    {
        pos = this.transform.position;
        levelController = GameObject.Find("LevelController");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (levelController.GetComponent<HadesLevelController>().getRoomCount() < 5)
            {
                Object.Instantiate(room, transform.position + transform.forward * 50 + transform.up * -5, Quaternion.identity);
                other.gameObject.transform.position = transform.TransformPoint(Vector3.forward * 27);

                levelController.GetComponent<HadesLevelController>().roomCountIncrease();
            }
            // Else spawn boss room
        }
    }

}
