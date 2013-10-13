using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {
	
	public float distanceFactor = 1f;
	
	void OnDrawGizmos () {
		Gizmos.DrawIcon (transform.position, "Waypoint.tif");
	}
	
}
