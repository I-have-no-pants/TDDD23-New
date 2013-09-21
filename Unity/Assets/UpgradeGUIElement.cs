using UnityEngine;
using System.Collections;

public class UpgradeGUIElement : MonoBehaviour {
	
	public GameObject MyMenu;
	public GameObject MyAddon;
	
	// Some set for graphic...
	
	void OnMouseDown() {
        Debug.Log("Clicked GUI Element");
		MyMenu.GetComponent<Upgrademenu>().Upgrade(MyAddon);
    }
	
}
