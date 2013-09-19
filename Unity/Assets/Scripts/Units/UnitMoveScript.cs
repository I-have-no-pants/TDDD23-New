using UnityEngine;
using System.Collections;

public class UnitMoveScript : MonoBehaviour {
	
	public GameObject Target;
	
	private CharacterController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		controller.SimpleMove(new Vector3(1,0,0));
	
	}
}
