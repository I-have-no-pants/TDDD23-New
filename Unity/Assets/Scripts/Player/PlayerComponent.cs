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
		}
	}
	
	
	public TeamComponent MyTeam {
		get;
		private set;
	}
	
	public GameObject MoneyGUI;
		
	private float timer = 0f;
	
	// Use this for initialization
	void Start () {
		Money = StartMoney;
		MyTeam = GetComponent<TeamComponent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (timer >= 10f) {
			Money += 5;
			timer = 0f;
		}
		timer += Time.deltaTime;
	}
}
