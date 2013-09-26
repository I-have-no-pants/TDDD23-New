using UnityEngine;
using System.Collections;

public class WeaponComponentTrack : WeaponComponent {
	
	public GameObject turret;
	
	// Add features like lock rotations
	
	protected override void ProcessTarget() {
	
		if (target!=null && turret != null) {
			turret.transform.LookAt(target.transform,transform.up);
				
		}
		
	}
}
