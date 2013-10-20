using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	
	private CameraSlide camera;
	public Transform target;
	public float speed = 3f;
	
	// Use this for initialization
	void Start () {
		camera = GameObject.FindObjectOfType(typeof(CameraSlide)) as CameraSlide;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown () {
		camera.target = target;
		camera.speed = speed;
	}
}
