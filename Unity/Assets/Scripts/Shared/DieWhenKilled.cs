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
		if (DeathEffect) {
			var deatheffect = Instantiate(DeathEffect,transform.position,Quaternion.identity) as GameObject;
			deatheffect.SetActive(true);
			Destroy(deatheffect,DeathEffectLifeTime);
		}
		gameManager.Units.Remove(this);
		
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
