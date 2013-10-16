using UnityEngine;
using System.Collections;

/// <summary>
/// Team component. - Remembers what team I am on, usefull for weapons, etc.
/// </summary>
public class TeamComponent : MonoBehaviour {
	
	public string myTeam;
	public string MyTeam {
		get {
			return myTeam;
		}
		set {
			myTeam = value;
			
			Color c = Color.white;
			if (myTeam.CompareTo("TeamPlayer")==0)
				c = new Color(0,1,1);
			else if (myTeam.CompareTo("TeamEnemy")==0)
				c = Color.red;
			
			/*if (GetComponent<UpgradeableComponent>()!=null && (myTeam.CompareTo("TeamEnemy")==0))
				foreach(Transform r in transform) {
					r.gameObject.SetActive(false);
				}
			else*/
		
			foreach(Renderer r in GetComponentsInChildren<Renderer>()) {
				if (r!=null && r.material != null &&r.material.HasProperty("_Color") && r.material.GetColor("_Color")== Color.green)
					r.material.SetColor("_Color",c);
				
			}
				
				
			
		}
	}
	public string EnemyTeam;
	
	void Start() {
		if (MyTeam == "TeamEnemy") {
			
			
		}
	}
	
	public void Copy(TeamComponent c) {
		MyTeam = c.MyTeam;
		EnemyTeam = c.EnemyTeam;
	}
	
}
