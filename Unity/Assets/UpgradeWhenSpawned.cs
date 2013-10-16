using UnityEngine;
using System.Collections;

public class UpgradeWhenSpawned : MonoBehaviour {
	
	public GameObject MyUpgrade;
	
	// Use this for initialization
	void Start () {
		if (gameObject.activeInHierarchy)
			GetComponent<UpgradeableComponent>().Upgrade(MyUpgrade.GetComponent<BuildableComponent>(),transform.parent.GetComponent<TeamComponent>());
	}
	
}
