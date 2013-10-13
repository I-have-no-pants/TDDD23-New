using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class DecoratorComponent : BuildableComponent {

	public GameObject MyAddon;
		
	// Use this for initialization
	void Start () {
		InitDecoratorComponent();
		
	}
	
	protected void InitDecoratorComponent() {
		InitBuildableComponent();
	}
	
	
	public GameObject Decorate(GameObject obj, Transform position, GameObject root) {
		GameObject addon = Instantiate(MyAddon,position.position,position.rotation) as GameObject;
		
		// Set name
		if (addon.GetComponent<PathfindMovement>())
			addon.name = MyAddon.GetComponent<PathfindMovement>().Name;
		
		// I am root node
		if (root == null)
			root = addon;
		else {
			
			var rootUnit = root.GetComponent<PathfindMovement>();
			
			// Attach all other addons to the root
			var addonScript = addon.GetComponent<AddonComponent>();
			if (addonScript != null) {
				addonScript.myUnit = rootUnit;
				if (addonScript is WeaponComponent) {
					root.GetComponent<PathfindMovement>().totalTurrets++;
					var weaponScript = addon.GetComponent<WeaponComponent>();
					root.GetComponent<PathfindMovement>().dps += weaponScript.Damage / weaponScript.ReloadTime;
				}
				//Debug.Log(weaponScript.myUnit);
				
				
				// Add extra health to root unit
				var healthComponent = root.GetComponent<HealthComponent>();
				if (healthComponent && addonScript.AdditionalHealth!=0) {
					healthComponent.MaxHealth+=addonScript.AdditionalHealth;
					healthComponent.Health+=addonScript.AdditionalHealth;
				}
			}
			
			
			
			
		}
		
		addon.SetActive(true);
		foreach(var currentAddonPosition in addons.Keys) {
			var currentAddon = addons[currentAddonPosition]; // Ugly lookup
						
			var comp = currentAddon.GetComponent<DecoratorComponent>();
						
			if (comp != null) {
				Transform pos = addon.transform.FindChild(currentAddonPosition); // Position we want to place our addon on
				//Transform pos = addon.GetComponent<WeaponComponent>().Addons.
				//Debug.Log("Decorating at " + currentAddonPosition);
				comp.Decorate(addon, pos, root).transform.parent = addon.transform;
			}
			
		}
		return addon;
				
		//
	}
}
