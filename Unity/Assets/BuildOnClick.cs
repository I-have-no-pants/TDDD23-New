using UnityEngine;
using System.Collections;

public class BuildOnClick : ClickComponent {
	
	public int Cost;
	public GameObject Upgrade;
	
	
	public override void OnClick() {
		Debug.Log ("upgraded "+name);
		
		GameObject addon = Instantiate(Upgrade,transform.position,transform.rotation) as GameObject;
		addon.transform.parent = this.transform.parent;
		
		
		Destroy(this.gameObject);
		
	}
	
}
