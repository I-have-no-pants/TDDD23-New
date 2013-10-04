using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class BuildableComponent : MonoBehaviour {
	
	public int Size;
	public int Cost;
	public string Name;
	public string Description;
	
	public List<GameObject> AddonNodes;
	
	protected SortedDictionary<string,BuildableComponent> addons;

	protected TeamComponent myTeam;
	
	void Start() {
		 InitBuildableComponent();
		
	}
	
	protected void InitBuildableComponent() {
		addons = new SortedDictionary<string, BuildableComponent>();
		myTeam = GetComponent<TeamComponent>();
		if (myTeam && myTeam.MyTeam.CompareTo("TeamPlayer") != 0) {
			
			var enemy = GameObject.Find("Enemy").GetComponent<EnemyPlayer>();
			
			foreach (GameObject g in AddonNodes) {
				g.SetActive(false);
				enemy.PossiblePlaces.AddFirst (g.GetComponent<UpgradeableComponent>());
			}
		} 
	}
	
	
	public void AddAddon(BuildableComponent addon, string position) {
		Debug.Log("Addon added!");
		addons.Add(position,addon);
	}
	
}
