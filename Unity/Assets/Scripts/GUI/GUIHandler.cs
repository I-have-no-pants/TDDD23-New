using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIHandler : MonoBehaviour {

	protected GameObject selectedUnit = null;
    
	public GameObject SelectedUnit {
        get {      
			return selectedUnit;   
        }
		set {
			selectedCircle.SetActive(value != null);
			selectedUnit = value;
			if(value != null) {
				selectedCircle.transform.position = selectedUnit.gameObject.transform.position;
				healthComponent = value.GetComponent<HealthComponent>();
				upgradeComponent = value.GetComponent<UpgradeableComponent>();
			}
		}
	}
	
	public GameObject selectedCircle;
	
	/*
	protected string type;
	public string Type {
		get {
			return type;
		}
		set {
			type = value;
		}
	}*/
	
	public Texture background;
	private GUIStyle style = new GUIStyle();
	private GUIContent content = new GUIContent();
	private GameManagerComponent gameManager;
	public List<GameObject> buildings;
	private int buttonPos;
	private Rect tooltipPos;
	private HealthComponent healthComponent;
	private UpgradeableComponent upgradeComponent;
	private bool paused = false;
	
	public PlayerComponent player;
	
	void Start () {
		gameManager = GameObject.FindObjectOfType(typeof(GameManagerComponent)) as  GameManagerComponent;
		buildings = gameManager.Buildings;
		
		player = GameObject.Find ("Player").GetComponent<PlayerComponent>();
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.Escape)) {
			Debug.LogWarning(Time.fixedDeltaTime);
			Time.timeScale = 0f;
			Time.fixedDeltaTime = 0f;
			paused = true;
		}
	}
	
	void OnGUI () {
		
		if (paused) {
			GUI.Label(new Rect(Screen.width*9/19,Screen.height*9/19,100,20),"GAME PAUSED");
			if (GUI.Button(new Rect(Screen.width*9/19,Screen.height*10/19,100,20),"UNPAUSE")) {
				Time.timeScale = 1f;
				Time.fixedDeltaTime = 0.02f;
				paused = false;
			}
			if (GUI.Button(new Rect(Screen.width*9/19,Screen.height*11/19,100,20),"Exit to menu")) {
				Time.timeScale = 1f;
				Time.fixedDeltaTime = 0.02f;
				Application.LoadLevel("menu");
			}
		}
		
		GUI.DrawTexture(new Rect(0,Screen.height*2/3,Screen.width,Screen.height/3),background);
		GUI.Label(new Rect(Screen.width*9/19,Screen.height/9,100,20), "Bytes " + player.Money);
		tooltipPos = new Rect(Screen.width*6/8,Screen.height*2/3-Screen.height*1/8,Screen.width*2/8,100);
		
		if(selectedUnit==null)
			SelectedUnit = selectedUnit;
		
		if (selectedUnit != null) {
			
			selectedCircle.transform.position = selectedUnit.gameObject.transform.position;
			
			var namePos = new Rect(Screen.width*2/5,Screen.height*3/4-20, 150, 20);
			var damagePos = new Rect(Screen.width*2/5,Screen.height*3/4+20, 100, 20);
			GUI.Label(namePos, selectedUnit.name);
			
			
			if(healthComponent != null)
				drawHealth();
			
			
			if (upgradeComponent != null) {
			
				buttonPos = 0;
				foreach (GameObject g in buildings) {
					var buildComponent = g.GetComponent<BuildableComponent>();
					if (upgradeComponent.canBuild(buildComponent.Size)) {
						content.image = buildComponent.image;
						content.tooltip = buildComponent.tooltip;
						var pos = nextPosition(buttonPos,buildComponent.image);
						if (buildComponent.Cost <= player.Money) { //vet inte var money finns!
							if (GUI.Button(pos,content)) {
								upgradeComponent.Upgrade(buildComponent,player.MyTeam);
								player.Money -= buildComponent.Cost;
								SelectedUnit = null;
								return;
							}
						} else {
							GUI.color = Color.grey;
							GUI.Button(pos,content);
							GUI.color = Color.white;
						}
						buttonPos++;
					}
				}
        		GUI.Label(tooltipPos, GUI.tooltip);
			}
		}
	}
	
	Rect nextPosition(int pos, Texture image) {
		var widthPos = pos % Mathf.Floor((Screen.width-Screen.width*6/8)/image.width);
		var heightPos = Mathf.Floor(pos / Mathf.Floor((Screen.width-Screen.width*6/8)/image.width));
		return new UnityEngine.Rect(Screen.width*6/8+widthPos*image.width,Screen.height*2/3+Screen.height/64+heightPos*image.height,image.width,image.height);
	}
	
	void drawHealth () {
		var healthPos = new Rect(Screen.width*2/5,Screen.height*3/4, 150, 20);
		var maxHealth = (double) healthComponent.MaxHealth;
		var health = (double) healthComponent.Health;
		if (health/maxHealth > 0.66f)
			GUI.color = Color.green;
		else if (health/maxHealth > 0.33f)
			GUI.color = Color.yellow;
		else
			GUI.color = Color.red;
		GUI.Label(healthPos, "Health: "+health + " / " + maxHealth);
		GUI.color = Color.white;
	}
	
	
}
