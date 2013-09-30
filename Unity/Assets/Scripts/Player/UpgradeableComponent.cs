using UnityEngine;
using System.Collections;

public class UpgradeableComponent : MonoBehaviour {
	public int maxSize;
	public bool ExactSize = false;
	
	public GameObject myBase;
	
	// Called by the menu
	public void Upgrade(Buildable building, TeamComponent team) {

		GameObject addon = Instantiate(building.gameObject,transform.position,transform.rotation) as GameObject;
		addon.transform.parent = transform.parent;
		
		var baseObj = GetComponent<UpgradeableComponent>().myBase;
		if (baseObj!=null) {
			var baseObjAdd = baseObj.GetComponent<AddonComponent>();
			if (baseObjAdd != null)
				baseObjAdd.AddAddon(addon.GetComponent<AddonComponent>(),name);
		}
		
		var myTeam = GetComponent<TeamComponent>();
		if (myTeam) {
			myTeam.MyTeam = team.MyTeam;
			myTeam.EnemyTeam = team.EnemyTeam;
		}
		
		//Destroy(Target.gameObject);
		gameObject.SetActive(false);
		
	}
	
}
