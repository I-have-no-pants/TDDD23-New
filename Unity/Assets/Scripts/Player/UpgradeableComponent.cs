using UnityEngine;
using System.Collections;

public class UpgradeableComponent : MonoBehaviour {
	public int maxSize;
	public bool ExactSize = false;
	
	public GameObject myBase;
	
	private GUIHandler unit;
	
	void Start() {
		unit = GameObject.FindObjectOfType(typeof(GUIHandler)) as  GUIHandler;
	}
	
	// Called by the menu
	public void Upgrade(BuildableComponent building, TeamComponent team) {

		GameObject addon = Instantiate(building.gameObject,transform.position,transform.rotation) as GameObject;
		addon.transform.parent = transform.parent;
		
		var baseObj = GetComponent<UpgradeableComponent>().myBase;
		if (baseObj!=null) {
			var baseObjAdd = baseObj.GetComponent<BuildableComponent>();
			if (baseObjAdd != null)
				baseObjAdd.AddAddon(addon.GetComponent<BuildableComponent>(),name);
		}
		
		var myTeam = addon.GetComponent<TeamComponent>();
		if (myTeam) {
			myTeam.MyTeam = team.MyTeam;
			myTeam.EnemyTeam = team.EnemyTeam;
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
		unit.SelectedUnit = gameObject;
	}
	
}
