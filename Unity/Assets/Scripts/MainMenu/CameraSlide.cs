using UnityEngine;
using System.Collections;

public class CameraSlide : MonoBehaviour {
	
	public Transform target;
	public float speed = 3f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.position = Vector3.Lerp(transform.position,target.position,Time.deltaTime*speed);
			transform.rotation = Quaternion.Lerp(transform.rotation,target.rotation,Time.deltaTime*speed);
		}
	}
}
