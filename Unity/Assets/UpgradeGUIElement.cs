using UnityEngine;
using System.Collections;

public class UpgradeGUIElement : MonoBehaviour {
	
	public GameObject MyMenu;
	public GameObject MyAddon;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown() {
        Debug.Log("Clicked GUI Element");
		MyMenu.GetComponent<Upgrademenu>().Upgrade(MyAddon);
    }
	
}
