using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class BuildableComponent : MonoBehaviour, IEnumerable {
	
	public GameObject spawnEffect;
	
	public int Size;
	public int Cost;
	public string Name;
	public string tooltip;
	public Texture image;
	
	public List<GameObject> AddonNodes;
	
	protected SortedDictionary<string,BuildableComponent> addons;
	
	private GUIHandler unit;
	protected GameManagerComponent myGameManager;
	
	public BuildableComponent MyParent;
	
	public BuildableComponent MyRoot {
		get {
			return (MyParent==null) ? this : MyParent.MyRoot;
		}
	}
	
	public UpgradeableComponent MyLocation { // Enable this again when we are dead.
		set; get;
	}
		

	public TeamComponent myTeam {
		get;
		private set;
	}
	
	void Start() {
		InitBuildableComponent();
	}
	
	protected void InitBuildableComponent() {
		myGameManager = GameObject.Find("GameManager").GetComponent<GameManagerComponent>();
		addons = new SortedDictionary<string, BuildableComponent>();
		myTeam = GetComponent<TeamComponent>();
		
			
		var enemy = GameObject.Find("Enemy").GetComponent<EnemyPlayer>();
		
		foreach (GameObject g in AddonNodes) {
			//g.SetActive(false);
			g.GetComponent<TeamComponent>().Copy(myTeam);
			if (myTeam && myTeam.MyTeam!=null && myTeam.MyTeam.CompareTo("TeamPlayer") != 0)
				enemy.PossiblePlaces.AddFirst (g.GetComponent<UpgradeableComponent>());
		}

		unit = GameObject.FindObjectOfType(typeof(GUIHandler)) as  GUIHandler;
		
		if(gameObject.layer == 8)
			AstarPath.active.UpdateGraphs(gameObject.collider.bounds);
		
		SpawnEffect();
		
	}
	
	public void SpawnEffect() {
		if(spawnEffect!=null) {
			GameObject spawn = Instantiate(spawnEffect,transform.position,transform.rotation) as GameObject;
			Destroy(spawn,2);
		}
	}
	
	
	public void AddAddon(BuildableComponent addon, string position) {
		
		addons.Add(position,addon);
	}
	
	void OnMouseDown() {
		unit.SelectedUnit = gameObject;
		
		Debug.Log (this.calculateTotalCost());
	}
	
	public int calculateTotalCost() {
		int totalCost = 0;
		foreach (BuildableComponent b in this) {
			totalCost += b.Cost;
		}
		return totalCost;
	}
	
	public IEnumerator GetEnumerator ()
	{
		yield return this;
		if (addons!=null)
			foreach (BuildableComponent b in addons.Values)
				foreach(BuildableComponent b2 in b)
					yield return b2;
	}
}
