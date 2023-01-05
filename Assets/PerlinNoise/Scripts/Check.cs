using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit))
        {
            if (transform.position.y - hit.distance < 1.5f)
            {
                Debug.Log(transform.position + "HIT");
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
