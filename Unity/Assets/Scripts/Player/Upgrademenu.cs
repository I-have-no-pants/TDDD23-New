using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Upgrademenu : MonoBehaviour {
	
	public GameObject Player;
	private PlayerComponent player;
	
	public List<GameObject> Buildings;
	
	public GameObject BuildingMenuElement;
	
	private int maxSize;
	private bool ExactSize;
	
	private TeamComponent myTeam;
	
	// Use this for initialization
	void Start () {
		player = Player.GetComponent<PlayerComponent>();
		myTeam = player.GetComponent<TeamComponent>();
	}
	
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
			
			if (target != value && value != null) {
				maxSize = value.GetComponent<UpgradeableComponent>().maxSize;
				ExactSize = value.GetComponent<UpgradeableComponent>().ExactSize;
				BuildMenu();
					
			}
			
			target = value;
			
		} 
	}
	
	private void BuildMenu() {
		
		foreach (Transform child in transform)
			Destroy(child.gameObject);
		
		float start = 0.94f;
		foreach(GameObject g in Buildings) {
			if ((g.GetComponent<Buildable>().Size <= maxSize && !ExactSize) || (ExactSize && g.GetComponent<Buildable>().Size == maxSize)) {
				// Create a new GUI element.
				GameObject newElement = Instantiate(BuildingMenuElement,new Vector3(0.07f,start,0f),Quaternion.identity) as GameObject;
				
				newElement.transform.position = new Vector3(0.07f,start,0f);
				Debug.Log("Building meny for " + newElement);
				newElement.GetComponent<UpgradeGUIElement>().MyAddon = g;
				newElement.GetComponent<UpgradeGUIElement>().MyMenu = this.gameObject;
				
				newElement.transform.parent = this.transform;
				
				newElement.transform.FindChild("Name").guiText.text = g.GetComponent<Buildable>().Name + " [ " + g.GetComponent<Buildable>().Cost + " MB ]";
				newElement.transform.FindChild("Description").guiText.text = g.GetComponent<Buildable>().Description;
				
				if (g.GetComponent<Buildable>().Cost > player.Money)
					newElement.transform.FindChild("Name").guiText.color = Color.gray;
				
				newElement.SetActive(true);
				start -=0.1f;
			}
		}
	}
	
	public void Upgrade(GameObject addonBase) {
		if (Target!=null) {
			
			if (player.Money >= addonBase.GetComponent<Buildable>().Cost) {
				
				player.Money-= addonBase.GetComponent<Buildable>().Cost;
				
			
				Target.GetComponent<UpgradeableComponent>().Upgrade(addonBase.GetComponent<Buildable>(), myTeam);
				
				Target = null;
				this.gameObject.SetActive(false);
				
			}
			
		}
	}
	
}
