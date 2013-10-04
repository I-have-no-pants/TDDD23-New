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
		
		// I am root node
		if (root == null)
			root = addon;
		else {
			
			// Attach all other addons to the root
			var weaponScript = addon.GetComponent<AddonComponent>();
			if (weaponScript != null) {
				weaponScript.myUnit = root.GetComponent<PathfindMovement>();
				//Debug.Log(weaponScript.myUnit);
			}
		}
		
		addon.SetActive(true);
		foreach(var currentAddonPosition in addons.Keys) {
			var currentAddon = addons[currentAddonPosition]; // Ugly lookup
						
			var comp = currentAddon.GetComponent<DecoratorComponent>();
						
			if (comp != null) {
				Transform pos = addon.transform.FindChild(currentAddonPosition); // Position we want to place our addon on
				//Transform pos = addon.GetComponent<WeaponComponent>().Addons.
				Debug.Log("Decorating at " + currentAddonPosition);
				comp.Decorate(addon, pos, root).transform.parent = addon.transform;
			}
			
		}
		return addon;
				
		//
	}
}
