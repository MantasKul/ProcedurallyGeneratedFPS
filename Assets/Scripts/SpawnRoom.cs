using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(prefab, transform.position, transform.rotation);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
