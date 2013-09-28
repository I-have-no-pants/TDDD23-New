using UnityEngine;
using System.Collections;

public class ChangeLevelOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnMouseDown() {
        Debug.Log("Clicked GUI Element");
		Application.LoadLevel("level1");
    }
	
}
