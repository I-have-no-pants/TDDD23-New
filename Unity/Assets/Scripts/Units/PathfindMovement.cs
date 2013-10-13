using UnityEngine;
using System.Collections;
using Pathfinding;

public class PathfindMovement : MonoBehaviour {
	
	public Vector3 targetPosition;
	public Transform[] waypoints;
	public string Name;
	
	public int activeTurrets;
	public int ActiveTurrets {
		get {
			return activeTurrets; 
		}
		set {
			
			activeTurrets = value;
			if (activeTurrets < 0)
				activeTurrets = 0;
			
			if (!ShouldMove()) {
				gameObject.layer = 8; //Obstacle
				AstarPath.active.UpdateGraphs(gameObject.collider.bounds);
			} else  {
				gameObject.layer = 0; //Default
				AstarPath.active.UpdateGraphs(gameObject.collider.bounds);
			}
			
		}
	}
	
	public int totalTurrets;
	
	private Seeker seeker;
	//The calculated path
	public Path path;
	//The AI's speed per second
	public float speed = 1;
	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;
	//The waypoint we are currently moving towards
	private int currentWaypoint, waypointCounter = 0;
	private CharacterController controller;
	private float rotationSpeed = 0.1f;
	private GUIHandler unit;
	private float distanceFactor = 1f;
	public float dps = 0f;
	
	public void Start () {
		seeker = GetComponent<Seeker>();
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		setTargetPosition();
		controller = GetComponent<CharacterController>();
		unit = GameObject.FindObjectOfType(typeof(GUIHandler)) as  GUIHandler;
		AstarPath.OnGraphsUpdated += OnGraphsUpdated;
	}
	public void OnPathComplete (Path p) {
		if (!p.error) {
			path = p;
		}
	}
	public void setTargetPosition() {
		var randomPos = Random.insideUnitCircle*nextWaypointDistance;
		targetPosition.x = waypoints[waypointCounter].position.x + randomPos.x;
		targetPosition.y = waypoints[waypointCounter].position.y;
		targetPosition.z = waypoints[waypointCounter].position.z + randomPos.y;
		if (seeker.IsDone())
			seeker.StartPath (transform.position,targetPosition, OnPathComplete);
	}
	public void Update () {
		if (path == null) {
			//We have no path to move after yet
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count) {
			//Reset the waypoint counter
			currentWaypoint = 0;
			waypointCounter = (waypointCounter + 1) % waypoints.Length;
			distanceFactor = waypoints[waypointCounter].GetComponent<Waypoint>().distanceFactor;
			setTargetPosition();
			return;
		}
		
		//Direction to the next waypoint
		//shooting = GetComponentInChildren<Sight>().shoot;
		//Debug.LogWarning(activeTurrets);
		if (ShouldMove()) {
			Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position);
			transform.Rotate(Vector3.up * AngleAroundAxis(transform.forward, (path.vectorPath[currentWaypoint] - transform.position), Vector3.up) * rotationSpeed);
			dir = dir.normalized * speed;
			controller.SimpleMove(dir);
			//Check if we are close enough to the next waypoint
			//If we are, proceed to follow the next waypoint
			if (currentWaypoint - 1 >= path.vectorPath.Count && Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance * distanceFactor ||
				(Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance)) {
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
	
	public void OnGraphsUpdated (AstarPath script) {
		setTargetPosition();
	}
	
	void OnMouseDown() {
		unit.SelectedUnit = gameObject;
	}
	
	private bool ShouldMove() {
		return totalTurrets == 0 || ActiveTurrets < Mathf.Ceil(totalTurrets / 2.0f);
	}
	
	
}
