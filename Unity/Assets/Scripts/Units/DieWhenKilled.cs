using UnityEngine;
using System.Collections;

public class DieWhenKilled : HealthComponent {

	protected override void OnDeath ()
	{
		Destroy(this.gameObject);
	}
	
	protected override void OnDamage (int damage) {
	}
}
