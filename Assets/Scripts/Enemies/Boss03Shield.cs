using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Shield : MonoBehaviour
{
    public GameObject prot1;
    public GameObject prot2;
    public GameObject prot3;
    public GameObject prot4;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        if (!prot1.activeInHierarchy && !prot2.activeInHierarchy && !prot3.activeInHierarchy && !prot4.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
