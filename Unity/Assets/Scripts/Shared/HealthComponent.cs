using UnityEngine;
using System.Collections;

public abstract  class HealthComponent : MonoBehaviour {
	
	public int MaxHealth;
	
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
	}
	
	
	protected abstract void OnDeath();
	protected abstract void OnDamage(int damage);
}
