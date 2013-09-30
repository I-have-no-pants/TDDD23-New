using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Buildable : MonoBehaviour {
	
	public int Size;
	public int Cost;
	public string Name;
	public string Description;
	
	public List<GameObject> AddonNodes;
	
	private TeamComponent myTeam;
	
	void Start() {
		// Hide addon position if we are not in players team
		
		myTeam = GetComponent<TeamComponent>();
		if (myTeam && myTeam.MyTeam.CompareTo("TeamPlayer") != 0) {
			
			var enemy = GameObject.Find("Enemy").GetComponent<EnemyPlayer>();
			
			foreach (GameObject g in AddonNodes) {
				g.SetActive(false);
				enemy.PossiblePlaces.AddFirst (g.GetComponent<UpgradeableComponent>());
			}
		} 
	}
		
}
