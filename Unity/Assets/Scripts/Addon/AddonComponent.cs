using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddonComponent : MonoBehaviour {
	
	//public List<GameObject> Addons;
	
	public PathfindMovement myUnit {
		get;
		set;
	}
	
	protected TeamComponent myTeam;
	
	// Use this for initialization
	void Start () {
		InitAddonComponent();
	}
	
	protected void InitAddonComponent() {
		myTeam = GetComponent<TeamComponent>();
	}
	
}
