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
	
	public void Decorate(GameObject obj) {
		GameObject addon = Instantiate(MyAddon,transform.position,transform.rotation) as GameObject;
		
		// Instanciate childrens...
		foreach (Transform child in transform) {
			var comp = child.GetComponent<AddonComponent>();
			if (comp != null)
				comp.Decorate(addon);
		}
		
		addon.transform.parent = obj.transform;
	}
}
