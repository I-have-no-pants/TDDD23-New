using UnityEngine;
using System.Collections;

public class NonSelectable : MonoBehaviour {

	private GUIHandler unit;
	
	void Start () {
		unit = GameObject.FindObjectOfType(typeof(GUIHandler)) as  GUIHandler;
	}
	
	void OnMouseDown() {
		unit.SelectedUnit = null;
	}
}
