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
		GetComponent<AddonComponent>().Decorate(spawn);
		
		
	}
}
