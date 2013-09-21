using UnityEngine;
using System.Collections;

/// <summary>
/// Allows the user to click on objects in the gameworld.
/// </summary>
public class RTSCameraClick : MonoBehaviour
{
	private bool click = false;
	
	public GameObject UpgradeMenu;
		
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			if (click == false) {
				click = true;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				
				Debug.Log ("Clicked");
				
				UpgradeMenu.GetComponent<Upgrademenu>().Target = null;
				
				if (Physics.Raycast (ray, out hit)) {
					if (hit.transform != null) {
						var clickComp = hit.transform.GetComponent<ClickComponent>();
						if (clickComp != null)
							clickComp.OnClick();
						
						var upgradeComp = hit.transform.GetComponent<UpgradeableComponent>();
						if (upgradeComp != null)
							UpgradeMenu.GetComponent<Upgrademenu>().Target = upgradeComp.gameObject;
						
					}
				}
			}
		} else if (click) {
			click = false;
		}
	}
}
