using UnityEngine;
using System.Collections;

using System.Collections.Generic;
public class UpgradeableComponent : MonoBehaviour {
	public int maxSize;
	public bool ExactSize = false;
	public List<Transform> waypoints;
	
	public GameObject myBase;
	
	private GUIHandler unit;
	
	private TeamComponent myTeam;
	
	void Start() {
		unit = GameObject.FindObjectOfType(typeof(GUIHandler)) as  GUIHandler;
		myTeam = GetComponent<TeamComponent>();
	}
	
	// Called by the menu
	public void Upgrade(BuildableComponent building, TeamComponent team) {

		GameObject addon = Instantiate(building.gameObject,transform.position,transform.rotation) as GameObject;
		addon.name = building.Name;
		addon.transform.parent = transform.parent;
		if (addon.GetComponent<FactoryComponent>())
			addon.GetComponent<FactoryComponent>().waypoints = waypoints;
		
		if (myBase!=null) {
			var baseObjAdd = myBase.GetComponent<BuildableComponent>();
			if (baseObjAdd != null) {
				baseObjAdd.AddAddon(addon.GetComponent<BuildableComponent>(),name);
				addon.GetComponent<BuildableComponent>().MyParent = baseObjAdd;
			}
		}
		
		var myTeam2 = addon.GetComponent<TeamComponent>();
		if (myTeam2) {
			myTeam2.MyTeam = team.MyTeam;
			myTeam2.EnemyTeam = team.EnemyTeam;
		}
		
		//Destroy(Target.gameObject);
		
		if (team.MyTeam == "TeamEnemy") {
			GameObject.Find ("Enemy").GetComponent<EnemyPlayer>().PossiblePlaces.Remove (this);
		}
		
		addon.GetComponent<BuildableComponent>().MyLocation =this;
		
		gameObject.SetActive(false);
		
	}
	
	/// <summary>
	/// Notifies the building killed. Called by buildings health component when it dies
	/// </summary>
	public void NotifyBuildingKilled(BuildableComponent building) {
		gameObject.SetActive(true);
		if (building.myTeam.myTeam == "TeamEnemy") {
			GameObject.Find ("Enemy").GetComponent<EnemyPlayer>().PossiblePlaces.AddLast (this);
		}
	}
	
	public bool canBuild(int size) {
		if (ExactSize)
			return size == maxSize;
		return size <= maxSize;
	}
	
	void OnMouseDown() {	
		if (myTeam == null || myTeam.MyTeam=="TeamPlayer")
			unit.SelectedUnit = gameObject;
	}
	
}
