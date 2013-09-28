using UnityEngine;
using System.Collections;

public class DieWhenKilled : HealthComponent {
	
	public GameObject DeathEffect;
	public float DeathEffectLifeTime;
	
	public GameObject Damage;
	public float DamageTime;
	
	private float damageTimer;
	
	protected override void OnDeath ()
	{
		Destroy(Instantiate(DeathEffect,transform.position,Quaternion.identity) as GameObject,DeathEffectLifeTime);
		Destroy(this.gameObject);
	}
	
	protected override void OnDamage (int damage) {
		if (Damage) {
			damageTimer = DamageTime;
			Damage.particleSystem.enableEmission = true;
		}
	}
	
	void FixedUpdate () {
		if (Damage) {
			if (damageTimer>0)
				damageTimer-=Time.fixedDeltaTime;
			else if (Damage.particleSystem.enableEmission == true)
				Damage.particleSystem.enableEmission = false;
		}
	}
	
}
