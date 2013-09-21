using UnityEngine;
using System.Collections;

public class Upgrademenu : MonoBehaviour {
	
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
			target = value;
			
		}
	}
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
