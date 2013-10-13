using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class BuildableComponent : MonoBehaviour {
	
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
		if (myTeam && myTeam.MyTeam!=null && myTeam.MyTeam.CompareTo("TeamPlayer") != 0) {
			
			var enemy = GameObject.Find("Enemy").GetComponent<EnemyPlayer>();
			
			foreach (GameObject g in AddonNodes) {
				g.SetActive(false);
				enemy.PossiblePlaces.AddFirst (g.GetComponent<UpgradeableComponent>());
				Debug.Log("Added place to the enemy");
			}
		} 
		
		unit = GameObject.FindObjectOfType(typeof(GUIHandler)) as  GUIHandler;
		
		if(gameObject.layer == 8)
			AstarPath.active.UpdateGraphs(gameObject.collider.bounds);
	}
	
	
	public void AddAddon(BuildableComponent addon, string position) {
		
		addons.Add(position,addon);
	}
	
	void OnMouseDown() {
		unit.SelectedUnit = gameObject;
	}
	
}
