using UnityEngine;
using System.Collections;

public class UnitMoveScript : MonoBehaviour {
	
	// Moves towards Target in a retarded way, replace this way with pathfinding or something else.
	public GameObject Target;
	
	public float Speed = 1;
	
	private CharacterController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		Target = GameObject.FindGameObjectWithTag("TeamEnemy");
	}
	
	// Update is called once per frame
	void Update () {
		if (Target)
		controller.SimpleMove((Target.transform.position - transform.position).normalized * Speed);
		transform.Rotate(0,1,0);
	
	}
}
