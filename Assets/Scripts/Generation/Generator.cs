using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    Vector3 worldSize = new Vector3(100, 0, 100); // actual size double this
    Room[,] rooms;
    List<Vector3> takenPositions = new List<Vector3>();
    int gridSizeX, gridSizeZ, numberOfRooms = 20;

    [Header("Room Prefabs")]
    public List<GameObject> roomsToSpawn; // = new List<GameObject>();

    public Transform mapRoot;

    private bool bossRoomSpawned = false;

    private void Start()
    {
        //make sure there aren't more rooms that can fit
        if(numberOfRooms >= (worldSize.x * 2) * (worldSize.z * 2))
        {
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * worldSize.z * 2);
        }
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeZ = Mathf.RoundToInt(worldSize.z);

        CreateRooms();
        SpawnRooms();
    }

    void CreateRooms()
    {
        //Make the room array the proper size
        rooms = new Room[gridSizeX * 2, gridSizeZ * 2];
        // first room at the center of the grid
        rooms[gridSizeX, gridSizeZ] = new Room(new Vector3(0, mapRoot.transform.position.y, 0), 0); 
        takenPositions.Insert(0, new Vector3(0, mapRoot.transform.position.y, 0));
        Vector3 checkPos = Vector3.zero;

        // Main loop
        // Random generation variables
        float randomCompare = 0.2f;
        float randomCompareStart = 0.2f;
        float randomCompareEnd = 0.01f;

        //Add Rooms
        // loop runs once for each room we want to make
        for (int i = 0; i < numberOfRooms - 1; i++)
        {
            //
            float randomPerc = i / (((float)numberOfRooms - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc); // further we are in the loop it's less likely we will branch out
            // Grab new position
            checkPos = NewPosition();
            // Test new Position
            if(NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare) // this will make that some rooms will clump but others will branch out determined by "magic numbers"
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if (iterations >= 50) Debug.Log("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenPositions));
            }
            // Finalize position
            int randomType = Random.Range(1, roomsToSpawn.Count-1);
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.z + gridSizeZ] = new Room(checkPos, randomType);
            takenPositions.Insert(0, checkPos);
        }
    }

    public Vector3 NewPosition()
    {
        int x = 0, z = 0;
        Vector3 checkingPos = Vector3.zero;
        do
        {
            // grab a random takenpositions (generated room) from the list
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            z = (int)takenPositions[index].z;
            //randomly select if we going up/down/left/right
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    z += 1;
                }
                else
                {
                    z -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector3(x, mapRoot.transform.position.y, z);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || z >= gridSizeZ || z < -gridSizeZ); // going through the loop until we find a position that's not taken and it's within the boundries
        
        return checkingPos;
    }

    // slightlty modified NewPosition() method
    public Vector3 SelectiveNewPosition()
    {
        int index = 0, inc = 0;
        int x = 0, z = 0;
        Vector3 checkingPos = Vector3.zero;

        do
        {
            inc = 0;
            // when grabing position making sure we grab one that has only one neighbor
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            } while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
            x = (int)takenPositions[index].x;
            z = (int)takenPositions[index].z;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    z += 1;
                }
                else
                {
                    z -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector3(x, mapRoot.transform.position.y, z);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || z >= gridSizeZ || z < -gridSizeZ);
        if(inc >= numberOfRooms)
        {
            Debug.Log("Error: could not find position with only one neighbor");
        }

        return checkingPos;
    }

    public int NumberOfNeighbors(Vector3 checkingPos, List<Vector3> usedPositions)
    {
        int ret = 0;
        if (usedPositions.Contains(checkingPos + Vector3.right)) ret++;
        if (usedPositions.Contains(checkingPos + Vector3.left)) ret++;
        if (usedPositions.Contains(checkingPos + Vector3.forward)) ret++;
        if (usedPositions.Contains(checkingPos + Vector3.back)) ret++;

        return ret;
    }

    void SpawnRooms()
    {
        foreach(Room room in rooms)
        {
            if (room == null)
                continue;

            Vector3 drawPos = room.gridPos;
            drawPos.x *= 150;
            drawPos.z *= 150;
            int roomType = room.type;

            if (bossRoomSpawned == false)
            {
                if (NumberOfNeighbors(room.gridPos, takenPositions) == 1 && room.type != 0)
                {
                    roomType = 5;
                    bossRoomSpawned = true;
                }
                if (NumberOfNeighbors(room.gridPos, takenPositions) == 2 && room.type != 0)
                {
                    roomType = 5;
                    bossRoomSpawned = true;
                }
                if (NumberOfNeighbors(room.gridPos, takenPositions) == 3 && room.type != 0)
                {
                    roomType = 5;
                    bossRoomSpawned = true;
                }
                if (NumberOfNeighbors(room.gridPos, takenPositions) == 4 && room.type != 0)
                {
                    roomType = 5;
                    bossRoomSpawned = true;
                }
            }

            GameObject obj = Instantiate(roomsToSpawn[roomType], drawPos, Quaternion.identity);
            obj.transform.parent = mapRoot;
        }
    }
}
