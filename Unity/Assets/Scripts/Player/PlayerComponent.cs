﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Player component. Contains things the player needs to keep track of, like money etc.
/// </summary>
public class PlayerComponent : MonoBehaviour {
	
	
	static private PlayerComponent instance;
	static public PlayerComponent GetInstance() {
		return instance;
	}
	
	
	public int StartMoney;
	
	private int money;
	public int Money {
		get {return money;}
		set {money = value;
		}
	}
	
	public GameObject BaseSpawnPosition;
	public GameObject BaseObject;
	
	
	public TeamComponent MyTeam {
		get;
		private set;
	}
	
	private float timer = 0f;
	
	public int IncomePerSecond {
		private set;
		get;
	}
	
	// Use this for initialization
	void Start () {
		Money = StartMoney;
		MyTeam = GetComponent<TeamComponent>();
		instance = this;
		IncomePerSecond = 3;
		
		BaseSpawnPosition.GetComponent<UpgradeableComponent>().Upgrade(BaseObject.GetComponent<BuildableComponent>(),MyTeam);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timer >= 1f) {
			Money += IncomePerSecond;
			timer = 0f;
		}
		timer += Time.deltaTime;
	}
}
