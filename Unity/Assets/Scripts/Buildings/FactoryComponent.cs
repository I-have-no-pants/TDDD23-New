using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactoryComponent : DecoratorComponent {
	
	Transform spawnPosition;
	
	public float BaseSpawnTime = 10;
	
	public float CalculatedSpawnTime {
		get {
			return BaseSpawnTime + this.addons.Count;
		}
	}
	
	public float radius = 3;
	public Transform[] waypoints;
	public float spawnTimeCounter {
		get;
		private set;
	}
	
	private GameManagerComponent gameManager;
	
	// Use this for initialization
	void Start () {
		
		InitDecoratorComponent();
		
		//Debug.Log("Spawnposition is " + this.transform.FindChild("SpawnPosition").name);
		spawnPosition = this.transform.FindChild("SpawnPosition").transform;
		spawnTimeCounter=0;
		gameManager = GameObject.FindObjectOfType(typeof(GameManagerComponent)) as  GameManagerComponent;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		spawnTimeCounter+=Time.fixedDeltaTime;
		if (spawnTimeCounter>= CalculatedSpawnTime) {
			// Add some unit limit check here?
			spawnTimeCounter=0;
			Spawn ();
			if (myTeam.MyTeam == "TeamPlayer")
				gameManager.unitsBuilt++;
		}
	}
	
	public void Spawn() {
		foreach (HealthComponent h in myGameManager.Units) {
			if ((h.gameObject.transform.position - spawnPosition.position).magnitude < radius) {
				spawnTimeCounter = CalculatedSpawnTime-0.1f;
				return;
			}
		}
		
		var spawn = Decorate(null,spawnPosition, null);
		spawn.GetComponent<PathfindMovement>().waypoints = waypoints;
		
		if (myTeam != null) {
			foreach (var t in spawn.GetComponentsInChildren<TeamComponent>()){
				t.MyTeam = myTeam.MyTeam;
				t.EnemyTeam = myTeam.EnemyTeam;
			}
			
		}
		
		
		
		// Set team of the newly spawned thingy

		/*
		GameObject spawn = Instantiate(BaseSpawn,spawnPosition,Quaternion.identity) as GameObject;
		
		// Instanciate childrens...
		int i = 0;
		foreach (Transform child in transform) {
			var comp = child.GetComponent<AddonComponent>();
			if (comp != null) {
				Transform pos = spawn.transform.FindChild("Addon"+i);
				Debug.Log("Decorating at " + pos);
				comp.Decorate(spawn, pos);
				i++;
			}
		}*/
		
	}
}
