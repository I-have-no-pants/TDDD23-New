using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class AddonComponent : MonoBehaviour {

	public GameObject MyAddon;	

	protected SortedDictionary<string,AddonComponent> addons;

	
	// Use this for initialization
	void Start () {
		addons = new SortedDictionary<string, AddonComponent>();
	}
	
	
	public void AddAddon(AddonComponent addon, string position) {
		Debug.Log("Addon added!");
		addons.Add(position,addon);
	}
	
	public GameObject Decorate(GameObject obj, Transform position, GameObject root) {
		GameObject addon = Instantiate(MyAddon,position.position,position.rotation) as GameObject;
		if (root == null)
			root = addon;
		else {
			var weaponScript = addon.GetComponent<WeaponComponent>();
			if (weaponScript != null) {
				weaponScript.myUnit = root.GetComponent<PathfindMovement>();
				Debug.Log(weaponScript.myUnit);
			}
		}
		addon.SetActive(true);
		foreach(var currentAddonPosition in addons.Keys) {
			var currentAddon = addons[currentAddonPosition]; // Ugly lookup
						
			var comp = currentAddon.GetComponent<AddonComponent>();
						
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
