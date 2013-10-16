using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class GameManagerComponent : MonoBehaviour {
	
	static private GameManagerComponent instance;
	static public GameManagerComponent GetInstance() {
		return instance;
	}
	
	public HashSet<HealthComponent> Units;
	
	public List<GameObject> Buildings;
	
	//Score Screen
	public bool gameOver, winner = false;
	public int unitsDestroyed, unitsBuilt, buildingsDestroyed = 0;
	
	// Use this for initialization
	void Start () {
		Units = new HashSet<HealthComponent>();
		instance=this;
	}
}
