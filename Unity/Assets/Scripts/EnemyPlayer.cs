using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPlayer : MonoBehaviour {
	
	private float timer;
	
	private bool doonce = false;
	
	public GameObject gameMenu;
	private Upgrademenu menu; // So I can access all building types.
	
	private TeamComponent myTeam;
	public GameObject EnemyObject;
	
	public LinkedList<UpgradeableComponent> PossiblePlaces = new LinkedList<UpgradeableComponent>();
	
	// Use this for initialization
	void Start () {
		myTeam = EnemyObject.GetComponent<TeamComponent>();
		menu = gameMenu.GetComponent<Upgrademenu>();
		
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
				
				foreach (var u in menu.Buildings) {
					var b = u.GetComponent<BuildableComponent>();
					if (b != null && uprg.canBuild(b.Size) && Random.value<.25f) {
						building = b;
					}
				}
				
				
				if (uprg!=null && building!=null) {
					
					Debug.Log ("AI: Building " + building.name + " at " + uprg.gameObject.name);
				
					uprg.Upgrade(building,myTeam);
					
				}
			}
			
		}
	}
}
