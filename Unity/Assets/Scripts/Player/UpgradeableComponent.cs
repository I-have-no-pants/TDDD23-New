using UnityEngine;
using System.Collections;

public class UpgradeableComponent : MonoBehaviour {
	public int maxSize;
	public bool ExactSize = false;
	
	public GameObject myBase;
	
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
		
		if (team.MyTeam == "TeamEnemy")
			GameObject.Find ("Enemy").GetComponent<EnemyPlayer>().PossiblePlaces.Remove (this);
		
		gameObject.SetActive(false);
		
	}
	
	public bool canBuild(int size) {
		if (ExactSize)
			return size == maxSize;
		return size <= maxSize;
	}
	
}
