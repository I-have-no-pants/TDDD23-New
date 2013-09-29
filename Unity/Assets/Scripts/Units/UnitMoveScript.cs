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
		if (Target)
			controller.SimpleMove((Target.transform.position - transform.position).normalized * Speed);
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
}
