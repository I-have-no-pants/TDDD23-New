using UnityEngine;
using System.Collections;

public class FadeAway : MonoBehaviour {
	
	public Vector3 speed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(speed*Time.fixedDeltaTime);
	}
}
