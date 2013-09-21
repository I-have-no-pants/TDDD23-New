using UnityEngine;
using System.Collections;

public class AddonComponent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public GameObject MyAddon;
	
	public void Decorate(GameObject obj, Transform position) {
		GameObject addon = Instantiate(MyAddon,position.position,position.rotation) as GameObject;
		
		// Instanciate childrens...
		int i = 0;
		foreach (Transform child in transform) {
			var comp = child.GetComponent<AddonComponent>();
			if (comp != null) {
				Transform pos = addon.transform.FindChild("Addon"+i);
				Debug.Log("Decorating at " + pos);
				comp.Decorate(addon, pos);
				i++;
			}
			
		}
		
		addon.transform.parent = obj.transform;
	}
}
