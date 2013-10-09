using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
public class WeaponComponent : AddonComponent {
	
	public int Damage;
	
	public float ReloadTime;
	
	private float reload;
	
	private bool isActive;
	
	public HealthComponent target;
	protected HealthComponent Target {
		get {
			return target;
		}
		set {
			if(myUnit) {
				if (target == null && value != null) {
					myUnit.ActiveTurrets++;
					isActive = true;
				}
				else if (target == null && isActive) {
					isActive = false;
					myUnit.ActiveTurrets--;
				}
			}
			target = value;
		}
	}
	
	public GameObject muzzleBlast;
	public float muzzleBlastTime;
	
	
	public float Range;
	
	
	protected GameManagerComponent gameManager;
	
	protected Vector3 up;
	
	public float maxAngle = 90;
	
	public Vector3 FiringAngle;
		
	
	void Start() {
		InitWeaponComponent();
	}
	
	protected void InitWeaponComponent() {
		InitAddonComponent();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerComponent>();
		up = Vector3.up;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		
		if (muzzleBlast != null && ReloadTime-muzzleBlastTime >= reload)
			muzzleBlast.SetActive(false);
		
		
		if (ValidTarget(target))
			ProcessTarget();
				
		if (reload <= 0) {
			
			if (ValidTarget(target)) {
				reload = ReloadTime;
				Shoot ();
				
				
			} else {
				reload = 0.1f; // Small delay so we don't spam findTarget();
				Target = null;
				findTarget();
				
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
		
		//Debug.Log(this.gameObject.name + " pew'd " + target.gameObject.name);
	}
	
	protected bool ValidTarget(HealthComponent g) {
		
		if (g==null)
			return false;
		
		if (g.IsDead)		// Check distance here!	
			return false;
		
		if (g.MyTeam != myTeam.EnemyTeam)
			return false;
		
		Vector3 dist = (g.gameObject.transform.position - gameObject.transform.position);
			
		if (dist.magnitude > Range)
			return false;
		
		Debug.DrawLine(gameObject.transform.position, gameObject.transform.position+transform.TransformDirection(FiringAngle),Color.red);
		
		float angle =AngleAroundAxis(gameObject.transform.position,g.gameObject.transform.position,gameObject.transform.TransformDirection(FiringAngle));
		if (angle > maxAngle || angle < -maxAngle)
			return false;
	
		
		// Check angles
		
		
		
		return true;
	}
	
	private void findTarget() {
		foreach (HealthComponent h in gameManager.Units){
			if (ValidTarget(h)) {
				Target = h;
				break;
			}
		}
	}
	
	// Do stuff here like track target!
	virtual protected void ProcessTarget() {
	}
		
	// The angle between dirA and dirB around axis
	public static float AngleAroundAxis (Vector3 dirA, Vector3 dirB, Vector3 axis) {
		return Vector3.Angle(axis, dirB-dirA);
	}
	
}
