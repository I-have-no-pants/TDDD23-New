using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPlayer : MonoBehaviour {
	
	
	static private EnemyPlayer instance;
	static public EnemyPlayer GetInstance() {
		return instance;
	}
	
	private float timer;
	
	private GameManagerComponent gameManager;
	
	private bool doonce = false;
	
	private TeamComponent myTeam;
	public GameObject EnemyObject;
	
	public LinkedList<UpgradeableComponent> PossiblePlaces = new LinkedList<UpgradeableComponent>();
	
	public List<GameObject> StartBuildingPlaces;
	
	public int Money;
	
	public GameObject BaseSpawnPosition;
	public GameObject BaseObject;
	
	// Use this for initialization
	void Start () {
		myTeam = EnemyObject.GetComponent<TeamComponent>();
	
		gameManager = GameManagerComponent.GetInstance();
		
		foreach (GameObject t in StartBuildingPlaces) {
			var b = t.GetComponent<UpgradeableComponent>();
			if (b)
				PossiblePlaces.AddFirst(b);
		}
		instance = this;
		
		BaseSpawnPosition.GetComponent<UpgradeableComponent>().Upgrade(BaseObject.GetComponent<BuildableComponent>(),myTeam);
		
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
		timer-=Time.fixedDeltaTime;
	
		if (timer<=0) {
			if (Time.timeSinceLevelLoad < 45)
				timer = Random.Range(1,2);
			else
				timer = Random.Range(2,6);
			
			//Money+=10;
			/*
			if (Random.Range(0,1)>Money/100f)
				return;*/
			
			UpgradeableComponent uprg = null;
			
			// Crappy random function for selecting random thing
			foreach (var i in PossiblePlaces) {
				if (Random.value<.25f && i.gameObject.activeInHierarchy) {
					uprg = i;
					break;
				}
				
			}
			
			
			
			if (uprg != null) {
								
				BuildableComponent building = null;
				
				List<BuildableComponent> possibleBuildings = new List<BuildableComponent>();
				
				foreach (var u in gameManager.Buildings) {
					var b = u.GetComponent<BuildableComponent>();
					if (b != null && uprg.canBuild(b.Size) ) { //&& Money>=b.Cost) {
						possibleBuildings.Add (b);
					}
				}
				if (possibleBuildings.Count > 0)
					building = possibleBuildings[Random.Range(0,possibleBuildings.Count)];
				
				
				if (uprg!=null && building!=null) {
					
					Debug.Log ("AI: Building " + building.name + " at " + uprg.gameObject.name);
				
					uprg.Upgrade(building,myTeam);
					Money-=building.Cost;
					
				}
			}
			
		}
	}
}
