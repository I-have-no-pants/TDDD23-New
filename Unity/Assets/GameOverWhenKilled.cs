using UnityEngine;
using System.Collections;

public class GameOverWhenKilled : HealthComponent  {

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
		
		if (myBuilding!=null && myBuilding.MyLocation != null)
			myBuilding.MyLocation.NotifyBuildingKilled(myBuilding);
		
		gameManager.Units.Remove(this);
		
		gameObject.layer = 0; //Default
		AstarPath.OnGraphsUpdated -= GetComponent<PathfindMovement>().OnGraphsUpdated;
		AstarPath.active.UpdateGraphs(gameObject.collider.bounds);
		
		Destroy(this.gameObject);
		
		// Check for game over here
		gameManager.gameOver = true;
		if (MyTeam == "TeamEnemy")
			gameManager.winner = true;
		
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
