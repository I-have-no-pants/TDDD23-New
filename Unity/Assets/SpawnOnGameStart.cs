using UnityEngine;
using System.Collections;

public class SpawnOnGameStart : MonoBehaviour {
	
	public GameObject Spawn;

	// Use this for initialization
	void Start () {
		Instantiate(Spawn,transform.position,transform.rotation);
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
