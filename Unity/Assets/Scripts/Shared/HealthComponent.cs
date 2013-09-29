using UnityEngine;
using System.Collections;

public abstract  class HealthComponent : MonoBehaviour {
	
	public int MaxHealth;
	
	protected GameManagerComponent gameManager;
	
	private TeamComponent myTeam;
	
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
			if (value != 0)
				OnDamage (value);
			
			health =value;
			if (health <= 0) {
				IsDead=true;
				OnDeath ();
			}
		}
			
	}
	
	void Start() {
		health = MaxHealth;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerComponent>();
		gameManager.Units.Add(this);
		myTeam = GetComponent<TeamComponent>();
	}
	
	
	protected abstract void OnDeath();
	protected abstract void OnDamage(int damage);
}
