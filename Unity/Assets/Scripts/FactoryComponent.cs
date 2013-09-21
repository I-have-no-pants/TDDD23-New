using UnityEngine;
using System.Collections;

public class FactoryComponent : MonoBehaviour, TeamComponent {
	
	public IList Addons = new ArrayList();
	
	public GameObject BaseSpawn;
	
	public string MyTeam{get;set;}
	
	Vector3 spawnPosition;
	
	// Use this for initialization
	void Start () {
		Debug.Log("Spawnposition is " + this.transform.FindChild("SpawnPosition").name);
		spawnPosition = this.transform.FindChild("SpawnPosition").transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Spawn() {
		Debug.Log(""+name + " spawns a " + BaseSpawn.name);
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
		}
		
	}
}
