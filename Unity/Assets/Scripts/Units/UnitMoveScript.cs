using UnityEngine;
using System.Collections;

public class UnitMoveScript : MonoBehaviour {
	
	// Moves towards Target in a retarded way, replace this way with pathfinding or something else.
	public GameObject Target;
	
	public float Speed = 1;
	
	public TeamComponent myTeam;
	
	private CharacterController controller;
	
	private GameManagerComponent gameManager;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		myTeam = GetComponent<TeamComponent>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerComponent>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Target) {
			controller.SimpleMove((Target.transform.position - transform.position).normalized * Speed);
			transform.Rotate(Vector3.up * AngleAroundAxis (transform.forward, (Target.transform.position - transform.position), Vector3.up) * .1f);
		}
		//transform.Rotate(0,1,0);
		
		if (Target ==null){
			Target = null;
			foreach(HealthComponent h in gameManager.Units) {
				if (h.MyTeam == myTeam.EnemyTeam) {
					Target = h.gameObject;
					break;
				}
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
	
}
