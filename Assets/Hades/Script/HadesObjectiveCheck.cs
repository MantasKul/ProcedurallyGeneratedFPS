using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesObjectiveCheck : MonoBehaviour
{
    private GameObject t1;
    private GameObject t2;
    private GameObject t3;

    private void Start()
    {
        t1 = transform.parent.Find("Spawn").gameObject;


        if (t1 != null) transform.parent.Find("Spawn").gameObject.SetActive(false);


        foreach (Transform child in transform.parent)
        {

            if (child.tag == "EnemySpawner")
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, 50f, 1 << 10);
        Collider[] keys = Physics.OverlapSphere(transform.position, 50f, 1 << 11);

        if (enemies.Length == 0 && keys.Length == 0)
        {
            if (t1 != null) transform.parent.Find("Spawn").gameObject.SetActive(true);

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Transform child in transform.parent)
            {
                if (child.tag == "EnemySpawner")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
