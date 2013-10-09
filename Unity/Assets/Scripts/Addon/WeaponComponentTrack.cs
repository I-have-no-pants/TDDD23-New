using UnityEngine;
using System.Collections;

public class WeaponComponentTrack : WeaponComponent {
	
	public GameObject turret;
	
	
	// Add features like lock rotations
	
	protected override void ProcessTarget() {
	
		if (Target!=null && turret != null) {
			turret.transform.LookAt(Target.gameObject.transform,gameObject.transform.up);
				
		}
		
	}
}
