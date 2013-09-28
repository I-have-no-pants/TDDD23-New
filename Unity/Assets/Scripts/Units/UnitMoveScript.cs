using UnityEngine;
using System.Collections;

public class UnitMoveScript : MonoBehaviour {
	
	// Moves towards Target in a retarded way, replace this way with pathfinding or something else.
	public GameObject Target;
	
	public float Speed = 1;
	
	public TeamComponent myTeam;
	
	private CharacterController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		myTeam = GetComponent<TeamComponent>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Target)
			controller.SimpleMove((Target.transform.position - transform.position).normalized * Speed);
		//transform.Rotate(0,1,0);
		
		if (Target ==null)
			Target = GameObject.FindGameObjectWithTag(myTeam.EnemyTeam);
	
	}
}
