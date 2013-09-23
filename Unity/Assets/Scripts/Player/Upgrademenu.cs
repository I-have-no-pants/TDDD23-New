using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Upgrademenu : MonoBehaviour {
	
	public List<GameObject> Buildings;
	
	public GameObject BuildingMenuElement;
	
	private int maxSize;
	private bool ExactSize;
	
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
			
			if (target != value && value != null) {
				maxSize = value.GetComponent<UpgradeableComponent>().maxSize;
				ExactSize = value.GetComponent<UpgradeableComponent>().ExactSize;
				BuildMenu();
					
			}
			
			target = value;
			
		} 
	}
	
	private void BuildMenu() {
		
		foreach (Transform child in transform)
			Destroy(child.gameObject);
		
		float start = 0.94f;
		foreach(GameObject g in Buildings) {
			if ((g.GetComponent<Buildable>().Size <= maxSize && !ExactSize) || (ExactSize && g.GetComponent<Buildable>().Size == maxSize)) {
				// Create a new GUI element.
				GameObject newElement = Instantiate(BuildingMenuElement,new Vector3(0.07f,start,0f),Quaternion.identity) as GameObject;
				Debug.Log("Building meny for " + newElement);
				newElement.GetComponent<UpgradeGUIElement>().MyAddon = g;
				newElement.GetComponent<UpgradeGUIElement>().MyMenu = this.gameObject;
				
				newElement.transform.parent = this.transform;
				
				newElement.transform.FindChild("Name").guiText.text = g.GetComponent<Buildable>().Name;
				newElement.transform.FindChild("Description").guiText.text = g.GetComponent<Buildable>().Description;
				
				newElement.SetActive(true);
				start -=0.1f;
			}
		}
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
