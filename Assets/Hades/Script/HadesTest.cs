using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesTest : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    public List<GameObject> objects;

    public List<int> noList;

    public void Awake()
    {
        Instantiate(objects[GetRandomRange()], spawnPoints[0].transform);
        Instantiate(objects[GetRandomRange()], spawnPoints[1].transform);
        Instantiate(objects[GetRandomRange()], spawnPoints[2].transform);
    }

    public int GetRandomRange()
    {
        int r;

        r = Random.Range(0, objects.Count);

        if (noList.Contains(r))
            GetRandomRange();
        else
            noList.Add(r);

        return r;
    }
}
