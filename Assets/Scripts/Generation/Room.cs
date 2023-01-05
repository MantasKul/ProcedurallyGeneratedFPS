using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
	public Vector3 gridPos;
	public int type;
	//public bool doorTop, doorBot, doorLeft, doorRight;
	public Room(Vector3 _gridPos, int _type){
		gridPos = _gridPos;
		type = _type;
	}
}
