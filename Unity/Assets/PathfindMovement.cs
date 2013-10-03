using UnityEngine;
using System.Collections;
using Pathfinding;

public class PathfindMovement : MonoBehaviour {
	
	public Vector3 targetPosition;
	public Transform[] waypoints;
	public bool foundEnemy = false;
	
	private int activeTurrets;
	public int ActiveTurrets {
		get {
			return activeTurrets; 
		}
		set {
			activeTurrets = value;
			if (activeTurrets < 0)
				activeTurrets = 0;
		}
	}
	
	private Seeker seeker;
	//The calculated path
	public Path path;
	//The AI's speed per second
	public float speed = 1;
	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;
	//The waypoint we are currently moving towards
	private int currentWaypoint, waypointCounter = 0;
	private bool shooting;
	private float timer = 0f;
	private CharacterController controller;
	private float rotationSpeed = 0.1f;
	
	
	public void Start () {
		seeker = GetComponent<Seeker>();
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		setTargetPosition();
		controller = GetComponent<CharacterController>();
	}
	public void OnPathComplete (Path p) {
		if (!p.error) {
			path = p;
		}
	}
	public void setTargetPosition() {
		if (!foundEnemy)
			targetPosition = waypoints[waypointCounter].position;
		seeker.StartPath (transform.position,targetPosition, OnPathComplete);
	}
	public void FixedUpdate () {
		if (path == null) {
			//We have no path to move after yet
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count) {
			//Reset the waypoint counter
			currentWaypoint = 0;
			if (!foundEnemy)
				waypointCounter = (waypointCounter + 1) % waypoints.Length;
			setTargetPosition();
			return;
		}
		
		//Direction to the next waypoint
		//shooting = GetComponentInChildren<Sight>().shoot;
		Debug.LogWarning(activeTurrets);
		if (activeTurrets == 0) {
			timer += Time.fixedDeltaTime;
			if (timer > 0.5f) {
				setTargetPosition();
				timer = 0f;
			}
			Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position);
			transform.Rotate(Vector3.up * AngleAroundAxis(transform.forward, (path.vectorPath[currentWaypoint] - transform.position), Vector3.up) * rotationSpeed);
			dir = dir.normalized * speed;
			controller.SimpleMove(dir);
		//	transform.position = new Vector3(transform.position.x, controller.bounds.extents.y, transform.position.z);
			//Check if we are close enough to the next waypoint
			//If we are, proceed to follow the next waypoint
			if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
				currentWaypoint++;
				return;
			}
		}
	}
	
	 // The angle between dirA and dirB around axis
 	static float AngleAroundAxis (Vector3 dirA, Vector3 dirB , Vector3 axis) {
     	// Project A and B onto the plane orthogonal target axis
		dirA = dirA - Vector3.Project (dirA, axis);
     	dirB = dirB - Vector3.Project (dirB, axis);
    
    	 // Find (positive) angle between A and B
    	 float angle  = Vector3.Angle (dirA, dirB);
    
    	 // Return angle multiplied with 1 or -1
    	 return angle * (Vector3.Dot (axis, Vector3.Cross (dirA, dirB)) < 0 ? -1 : 1);
 	}
	
	
}
