﻿using UnityEngine;
using System.Collections;

public class ScaleSinus : MonoBehaviour {
	public Vector3 amplitude;
	public float frequency;
	
	public bool randomDirection;
	
	private float timer;
	
	// Use this for initialization
	void Start () {
		if (randomDirection)
				amplitude = Random.insideUnitSphere* (amplitude.magnitude);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer+=Time.fixedDeltaTime;
		transform.localScale+=( Mathf.Sin(timer*frequency)*amplitude);
	}
}