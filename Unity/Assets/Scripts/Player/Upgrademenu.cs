using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Upgrademenu : MonoBehaviour {
	
	public List<GameObject> Buildings;
	
	public GameObject BuildingMenuElement;
	
	private GameObject target;
	public GameObject Target {
		get {
			return target;
		}
		set {
			if (value == null)
				gameObject.SetActive(false);
			else if (target == null && value != null)
				gameObject.SetActive(true);
			
			if (target != value && value != null)
				BuildMenu();
			
			target = value;
			
		}
	}
	
	private void BuildMenu() {
		
		foreach (Transform child in transform)
			Destroy(child.gameObject);
		
		float start = 0.94f;
		foreach(GameObject g in Buildings) {
			// Create a new GUI element.
			GameObject newElement = Instantiate(BuildingMenuElement,new Vector3(0.07f,start,0f),Quaternion.identity) as GameObject;
			Debug.Log("Building meny for " + newElement);
			newElement.GetComponent<UpgradeGUIElement>().MyAddon = g;
			newElement.GetComponent<UpgradeGUIElement>().MyMenu = this.gameObject;
			
			newElement.transform.parent = this.transform;
			
			newElement.SetActive(true);
			start -=0.1f;
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Upgrade(GameObject addonBase) {
		if (Target!=null) {
			Debug.Log ("upgraded "+name);
			GameObject addon = Instantiate(addonBase,Target.transform.position,Target.transform.rotation) as GameObject;
			addon.transform.parent = Target.transform.parent;
			
			
			Destroy(Target.gameObject);
			Target = null;
			this.gameObject.SetActive(false);
		}
	}
	
}
