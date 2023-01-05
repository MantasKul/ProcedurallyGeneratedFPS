using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesLevelController : MonoBehaviour
{
    private int roomCount;

    public void Start()
    {
        roomCount = 0;
    }

    public void roomCountIncrease()
    {
        if (roomCount < 5)
        {
            roomCount++;
            Debug.Log(roomCount);
        }
        else if (roomCount >= 5)
            Debug.Log(roomCount + " Total rooms passed");
    }

    public int getRoomCount()
    {
        return roomCount;
    }
}
