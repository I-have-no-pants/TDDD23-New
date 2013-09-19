using UnityEngine;
using System.Collections;

public class WeaponComponent : MonoBehaviour {
	
	public string EnemyTeam;
	public int Damage;
	
	public float ReloadTime;
	
	private float reload;
	
	private GameObject target;
	
	// Update is called once per frame
	void FixedUpdate() {
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
	}
	
	protected bool ShouldShoot() {
		
		if (target==null)
			return false;
		
		if (target.GetComponent<HealthComponent>() == null || target.GetComponent<HealthComponent>().IsDead){			
			target = null;
			return false;
		}
		return true;
	}
	
	void OnTriggerEnter (Collider other) {
		Debug.Log(other.name +" entered");
		if (!other.isTrigger) {
			// Add check for if other is a better target (prefer armored units, etc)
			if (other.tag == EnemyTeam && target == null && other.gameObject.GetComponent<HealthComponent>() != null && !other.gameObject.GetComponent<HealthComponent>().IsDead) {
				target = other.gameObject;
			}
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject == target && !other.isTrigger)
			target = null;
	}
		
}
