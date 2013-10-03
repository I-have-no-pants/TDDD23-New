using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
public class WeaponComponent : MonoBehaviour {
	
	public int Damage;
	
	public float ReloadTime;
	
	private float reload;
	
	protected HealthComponent target;
	protected HealthComponent Target {
		get {
			return target;
		}
		set {
			if (target == null && value != null)
				myUnit.ActiveTurrets++;
			//else if (target != null && value == null)
			//	myUnit.ActiveTurrets--;
			target = value;
		}
	}
	
	public GameObject muzzleBlast;
	public float muzzleBlastTime;
	
	private TeamComponent myTeam;
	
	public float Range;
	
	
	protected GameManagerComponent gameManager;
	
	protected Vector3 up;
	
	public float maxAngle = 90;
	
	public Vector3 FiringAngle;
	
	public List<GameObject> Addons;
	
	public PathfindMovement myUnit {
		get;
		set;
	}
	
	
	void Start() {
		myTeam = GetComponent<TeamComponent>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerComponent>();
		up = Vector3.up;
		VerifyAngles();
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
				target = null;
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
		
		Debug.Log(this.gameObject.name + " pew'd " + target.gameObject.name);
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
				target = h;
				break;
			}
		}
	}
	
	// Do stuff here like track target!
	virtual protected void ProcessTarget() {
	}
	
	public void VerifyAngles() {
		
		Debug.Log (AngleAroundAxis(Vector3.up, Vector3.up*2, Vector3.up));
		Debug.Log (AngleAroundAxis(Vector3.zero, Vector3.left*2, Vector3.up));
		
		Debug.Log (AngleAroundAxis(Vector3.zero, Vector3.left+Vector3.up, Vector3.up));
		
		
		Debug.Log (AngleAroundAxis(Vector3.zero, Vector3.left+Vector3.up, Vector3.up));
		
	}
	
	// The angle between dirA and dirB around axis
	public static float AngleAroundAxis (Vector3 dirA, Vector3 dirB, Vector3 axis) {
	    // Project A and B onto the plane orthogonal target axis
		
		
		
		return Vector3.Angle(axis, dirB-dirA);
		
		/*
		Vector3 dist = dirA - dirB;
		
		Vector3 compa = Vector3.Project (dist, axis);
		Vector3 compb = dist - compa;*/
	    //dirA = Vector3.Project (dirA, axis);
	    //dirB = dist - Vector3.Project (dirB, axis);
	   
	    // Find (positive) angle between A and B
	    //float angle = Vector3.Angle (dirB, dirA);
	   
	    // Return angle multiplied with 1 or -1
		//Debug.Log(angle * (Vector3.Dot (axis, Vector3.Cross (dirA, dirB)) < 0 ? -1 : 1));
	    
	}
	
}
