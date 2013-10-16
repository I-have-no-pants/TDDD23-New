using UnityEngine;
using System.Collections;

public abstract  class HealthComponent : MonoBehaviour {
	
	public int MaxHealth;
	
	protected GameManagerComponent gameManager;
	
	protected TeamComponent myTeam;
	
	public int regenPer10Second;
	private float regenTimer = 10f;
	
	
	protected BuildableComponent myBuilding;
	
	public string MyTeam {
		get {
			if (myTeam)
				return myTeam.MyTeam;
			else
				return "";
		}
	}
	
	public bool IsDead {
		get;
		private set;
	}
	
	private int health;
	public int Health {
		get {
			return health;
		}
		set {
			if (value < health)
				OnDamage (value);
			
			health=Mathf.Min(MaxHealth,value);
			if (health <= 0) {
				IsDead=true;
				if (gameObject.GetComponent<BuildableComponent>())
					gameManager.buildingsDestroyed++;
				else if (MyTeam == "TeamEnemy")
					gameManager.unitsDestroyed++;
				OnDeath ();
			}
		}
			
	}
	
	void Start() {
		health = MaxHealth;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerComponent>();
		gameManager.Units.Add(this);
		myTeam = GetComponent<TeamComponent>();
		myBuilding = GetComponent<BuildableComponent>();
	}
	
	void Update() {
		if (regenTimer<0) {
			Health+=regenPer10Second;
			regenTimer=10;
		} else
			regenTimer-=Time.deltaTime;
				
	}
	
	protected abstract void OnDeath();
	protected abstract void OnDamage(int damage);
}
