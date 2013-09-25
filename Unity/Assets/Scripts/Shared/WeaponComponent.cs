using UnityEngine;
using System.Collections;

public class WeaponComponent : MonoBehaviour {
	
	public string EnemyTeam;
	public int Damage;
	
	public float ReloadTime;
	
	private float reload;
	
	public GameObject target;
	
	// Update is called once per frame
	void FixedUpdate() {
		ProcessTarget();
				
		if (reload <= 0) {
			if (ShouldShoot()) {
				reload = ReloadTime;
				Shoot ();
			}
		} else {
			reload -= Time.fixedDeltaTime;
		}
		
	}
		
	// Overwrite this if we want projectile or something
	protected void Shoot() {
		target.GetComponent<HealthComponent>().Health-=Damage;
		Debug.Log(this.gameObject.name + " pew'd " + target.name);
	}
	
	protected bool ShouldShoot() {
		
		if (target==null)
			return false;
		
		if (target.GetComponent<HealthComponent>() == null || target.GetComponent<HealthComponent>().IsDead){			
			target = null;
			Debug.Log(this.gameObject.name + " has new target: " + target);
			return false;
		}
		return true;
	}
	
	void OnTriggerStay (Collider other) {
		//Debug.Log(other.name +" entered");
		if (!other.isTrigger) {
			// Add check for if other is a better target (prefer armored units, etc)
			if (other.tag == EnemyTeam && target == null && other.gameObject.GetComponent<HealthComponent>() != null && !other.gameObject.GetComponent<HealthComponent>().IsDead) {
				target = other.gameObject;
				Debug.Log(name + " has new target: " + target.name);
			}
		}
	}
		/*
	void OnTriggerExit(Collider other) {
		if (other.gameObject == target && !other.isTrigger) {
			Debug.Log(name + " has new target: " + target.name);
			target = null;
		}
	}*/
	
	// Do stuff here like track target!
	virtual protected void ProcessTarget() {
	}
}
