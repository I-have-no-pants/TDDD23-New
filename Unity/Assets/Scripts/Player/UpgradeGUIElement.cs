using UnityEngine;
using System.Collections;

public class UpgradeGUIElement : MonoBehaviour {
	
	public GameObject MyMenu;
	public GameObject MyAddon;
	/*
	public void Init(string name, string descr, bool hasMoney, GameObject MyAddon, GameObject MyMenu) {
		this.MyAddon = MyAddon;
		this.MyMenu = MyMenu;
		
		gameObject.parent = MyMenu.transform;
		
		transform.FindChild("Name").guiText.text = name;
		transform.FindChild("Description").guiText.text = descr;
		
		newElement.SetActive(true);
		
		
		
	}*/
	
	// Some set for graphic...
	
	void OnMouseDown() {
        Debug.Log("Clicked GUI Element");
		MyMenu.GetComponent<Upgrademenu>().Upgrade(MyAddon);
    }
	
}
