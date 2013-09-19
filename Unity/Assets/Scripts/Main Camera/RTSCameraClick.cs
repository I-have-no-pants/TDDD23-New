﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Allows the user to click on objects in the gameworld.
/// </summary>
public class RTSCameraClick : MonoBehaviour
{
	private bool click = false;
		
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			if (click == false) {
				click = true;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				Debug.Log ("Clicked");
				if (Physics.Raycast (ray, out hit)) {
					if (hit.transform != null) {
						var clickComp = hit.transform.GetComponent<ClickComponent>();
						if (clickComp != null)
							clickComp.OnClick();
					}
				}
			}
		} else if (click) {
			click = false;
		}
	}
}