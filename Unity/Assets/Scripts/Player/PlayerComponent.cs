using UnityEngine;
using System.Collections;

/// <summary>
/// Player component. Contains things the player needs to keep track of, like money etc.
/// </summary>
public class PlayerComponent : MonoBehaviour {
	
	public int StartMoney;
	
	private int money;
	public int Money {
		get {return money;}
		set {money = value;
			MoneyGUI.GetComponent<GUIText>().text = money + " MB";
		}
	}
			
	
	
	public GameObject MoneyGUI;
		
		
	
	// Use this for initialization
	void Start () {
		Money = StartMoney;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
