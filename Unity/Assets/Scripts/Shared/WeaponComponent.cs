using UnityEngine;
using System.Collections;

public class WeaponComponent : MonoBehaviour {
	
	public int Damage;
	
	public float ReloadTime;
	
	private float reload;
	
	protected HealthComponent target;
	
	public GameObject muzzleBlast;
	public float muzzleBlastTime;
	
	public TeamComponent myTeam;
	
	public float Range;
	
	
	protected GameManagerComponent gameManager;
	
	protected Vector3 up;
	
	
	void Start() {
		myTeam = GetComponent<TeamComponent>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerComponent>();
		up = Vector3.up;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		
		if (muzzleBlast != null && ReloadTime-muzzleBlastTime >= reload)
			muzzleBlast.SetActive(false);
		
		ProcessTarget();
				
		if (reload <= 0) {
			
			if (ValidTarget(target)) {
				reload = ReloadTime;
				Shoot ();
			} else {
				findTarget();
				reload = 0.1f; // Small delay so we don't spam findTarget();
			}
			
		} else {
			reload -= Time.fixedDeltaTime;
		}
		
	}
		
	// Overwrite this if we want projectile or something
	protected void Shoot() {
		target.Health-=Damage;
		if (muzzleBlast != null)
			muzzleBlast.SetActive(true);
		
		Debug.Log(this.gameObject.name + " pew'd " + target.gameObject.name);
	}
	
	protected bool ValidTarget(HealthComponent g) {
		
		if (g==null)
			return false;
		
		if (g.IsDead)		// Check distance here!	
			return false;
		
		if (g.MyTeam != myTeam.EnemyTeam)
			return false;
			
		if ((g.gameObject.transform.position - gameObject.transform.position).magnitude > Range)
			return false;
		
		// Check angles
		
		
		
		return true;
	}
	
	private void findTarget() {
		foreach (HealthComponent h in gameManager.Units){
			if (ValidTarget(h)) {
				target = h;
				break;
			}
		}
	}
	
	// Do stuff here like track target!
	virtual protected void ProcessTarget() {
	}
}
