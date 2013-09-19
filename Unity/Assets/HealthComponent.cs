using UnityEngine;
using System.Collections;

public abstract  class HealthComponent : MonoBehaviour {
	
	public int maxHealth;
	
	private int health;
	public int Health {
		get {
			return health;
		}
		private set {
			if (value < 0)
				OnDamage (value);
			health =value;
			if (health < 0)
				OnDeath ();
		}
			
	}
	
	
	protected abstract void OnDeath();
	protected abstract void OnDamage(int damage);
}
