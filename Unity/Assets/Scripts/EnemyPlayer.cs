using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPlayer : MonoBehaviour {
	
	private float timer;
	
	private GameManagerComponent gameManager;
	
	private bool doonce = false;
	
	private TeamComponent myTeam;
	public GameObject EnemyObject;
	
	public LinkedList<UpgradeableComponent> PossiblePlaces = new LinkedList<UpgradeableComponent>();
	
	// Use this for initialization
	void Start () {
		myTeam = EnemyObject.GetComponent<TeamComponent>();
	
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerComponent>();
		
		foreach (GameObject t in GameObject.FindGameObjectsWithTag("TeamEnemyBuild")) {
			var b = t.GetComponent<UpgradeableComponent>();
			if (b)
				PossiblePlaces.AddFirst(b);
		}
		
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
		timer+=Time.fixedDeltaTime;
	
		if (timer>2) {
			timer = 0;
			
			UpgradeableComponent uprg = null;
			
			// Crappy random function for selecting random thing
			foreach (var i in PossiblePlaces) {
				if (Random.value<.25f){
					uprg = i;
					break;
				}
				
			}
			
			
			
			if (uprg != null) {
								
				BuildableComponent building = null;
				
				List<BuildableComponent> possibleBuildings = new List<BuildableComponent>();
				
				foreach (var u in gameManager.Buildings) {
					var b = u.GetComponent<BuildableComponent>();
					if (b != null && uprg.canBuild(b.Size)) {
						possibleBuildings.Add (b);
					}
				}
				
				building = possibleBuildings[Random.Range(0,possibleBuildings.Count-1)];
				
				
				if (uprg!=null && building!=null) {
					
					Debug.Log ("AI: Building " + building.name + " at " + uprg.gameObject.name);
				
					uprg.Upgrade(building,myTeam);
					
				}
			}
			
		}
	}
}
