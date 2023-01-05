using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveChecker : MonoBehaviour
{
    private GameObject t1;
    private GameObject t2;
    private GameObject t3;
    private GameObject t4;

    private void Start()
    {
        t1 = transform.parent.Find("Teleporter01").gameObject;
        t2 = transform.parent.Find("Teleporter01").gameObject;
        t3 = transform.parent.Find("Teleporter01").gameObject;
        t4 = transform.parent.Find("Teleporter01").gameObject;

        if (t1 != null) transform.parent.Find("Teleporter01").gameObject.SetActive(false);
        if (t2 != null) transform.parent.Find("Teleporter02").gameObject.SetActive(false);
        if (t3 != null) transform.parent.Find("Teleporter03").gameObject.SetActive(false);
        if (t4 != null) transform.parent.Find("Teleporter04").gameObject.SetActive(false);

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
        Collider[] enemies = Physics.OverlapSphere(gameObject.transform.position, 25f, 1 << 10);
        Collider[] keys = Physics.OverlapSphere(gameObject.transform.position, 25f, 1 << 11);

        if(enemies.Length == 0 && keys.Length == 0)
        {
            if (t1 != null) transform.parent.Find("Teleporter01").gameObject.SetActive(true);
            if (t2 != null) transform.parent.Find("Teleporter02").gameObject.SetActive(true);
            if (t3 != null) transform.parent.Find("Teleporter03").gameObject.SetActive(true);
            if (t4 != null) transform.parent.Find("Teleporter04").gameObject.SetActive(true);
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
