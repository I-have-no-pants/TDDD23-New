using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {
	
	public Vector3 speed;
	public bool randomDirection;
	// Use this for initialization
	void Start () {
		if (randomDirection)
			speed = Random.insideUnitSphere* (speed.magnitude);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(speed*Time.fixedDeltaTime);
	}
}
