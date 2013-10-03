using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactoryComponent : AddonComponent {
	
	public IList Addons = new ArrayList();
	
	public string MyTeam{get;set;}
	
	Transform spawnPosition;
	
	public float BaseSpawnTime = 10;
	private float spawnTimeCounter;
	
	private TeamComponent myTeam;
	
	// Use this for initialization
	void Start () {
		
		//Debug.Log("Spawnposition is " + this.transform.FindChild("SpawnPosition").name);
		spawnPosition = this.transform.FindChild("SpawnPosition").transform;
		spawnTimeCounter=BaseSpawnTime;
		
		addons = new SortedDictionary<string, AddonComponent>();
		
		myTeam = GetComponent<TeamComponent>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		spawnTimeCounter-=Time.fixedDeltaTime;
		if (spawnTimeCounter<= 0) {
			// Add some unit limit check here?
			spawnTimeCounter=BaseSpawnTime;
			Spawn ();
			
		}
	}
	
	public void Spawn() {
		var spawn = Decorate(null,spawnPosition, null);
		
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
