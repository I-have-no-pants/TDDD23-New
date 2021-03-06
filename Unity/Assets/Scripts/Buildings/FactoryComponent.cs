﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactoryComponent : DecoratorComponent {
	
	Transform spawnPosition;
	
	public float BaseSpawnTime = 10;
	
	public float CalculatedSpawnTime {
		get {
			int time = 0;
			foreach (BuildableComponent b in this) {
				time += 1;
			}
			return BaseSpawnTime + time;
		}
	}
	
	
	public float radius = 3;
	public List<Transform> waypoints;
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
		
		foreach (Transform w in waypoints)
			spawn.GetComponent<PathfindMovement>().waypoints.Add(w.position);
		
		if (myTeam != null) {
			foreach (var t in spawn.GetComponentsInChildren<TeamComponent>()){
				t.MyTeam = myTeam.MyTeam;
				t.EnemyTeam = myTeam.EnemyTeam;
			}
			
		}
		
		if (myTeam.MyTeam == "TeamPlayer") {
			gameManager.unitsBuilt++;
			var factoryCost = calculateTotalCost();
			if (gameManager.mostExpensiveUnit < factoryCost)
				gameManager.mostExpensiveUnit = factoryCost;
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
