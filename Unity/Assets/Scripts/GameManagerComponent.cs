using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class GameManagerComponent : MonoBehaviour {
	
	public HashSet<HealthComponent> Units;
	
	// Use this for initialization
	void Start () {
		Units = new HashSet<HealthComponent>();
	}
}
