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
				selectedCircle.transform.rotation = selectedUnit.gameObject.transform.rotation;
				
				Vector3 selectedCircleScale = SelectedUnit.collider.bounds.size;
				selectedCircleScale.y = 0;
				
				selectedCircle.transform.localScale = selectedCircleScale.magnitude * Vector3.one;
				
				healthComponent = value.GetComponent<HealthComponent>();
				upgradeComponent = value.GetComponent<UpgradeableComponent>();
				factoryComponent = value.GetComponent<FactoryComponent>();
				movementComponent = value.GetComponent<PathfindMovement>();
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
	private Rect tooltipPos, factoryPos;
	private HealthComponent healthComponent;
	private UpgradeableComponent upgradeComponent;
	private bool paused = false;
	private FactoryComponent factoryComponent;
	private PathfindMovement movementComponent;
	
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
	
	void showScoreScreen () {
		Time.timeScale = 0f;
		Time.fixedDeltaTime = 0f;
		var elapsedTime = Time.timeSinceLevelLoad;
		GUI.Box(new Rect(Screen.width*3/9,Screen.height*1/9,Screen.width*3/9,Screen.height*4/9),"");
		var whoWon = "";
		var style = new GUIStyle();
		if (gameManager.winner) {
			whoWon = "Victory";
			style.normal.textColor = Color.green;
		} else {
			whoWon = "Defeated";
			style.normal.textColor = Color.red;
		}
		style.fontSize = 40;
		GUI.Label(new Rect(Screen.width*3/9+Screen.width/16,Screen.height*1/9+Screen.height/32,Screen.width*2/9,Screen.height/16),""+whoWon,style);
		style.fontSize = 16;
		style.normal.textColor = Color.white;
		GUI.Label(new Rect(Screen.width*3/9+Screen.width/16,Screen.height*2/9,Screen.width*2/9,Screen.height*3/9),
			"Time: "+Mathf.Floor(elapsedTime/60)+" m "+Mathf.Floor(elapsedTime % 60)+" s\n" +
			"Units Destroyed: "+gameManager.unitsDestroyed+"\n"+
			"Units Built: "+gameManager.unitsBuilt+"\n"+
			"Buildings Destroyed: "+gameManager.buildingsDestroyed+"\n",
			style);
		//Main Menu Button
		if (GUI.Button(new Rect(Screen.width*3/9+Screen.width/32,Screen.height*4/9,Screen.width/9,Screen.height/16), "Main Menu")) {
			Application.LoadLevel("menu");
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f;
			gameManager.gameOver = false;
			gameManager.winner = false;
		}
		//Restart Button
		if (GUI.Button(new Rect(Screen.width*3/9+Screen.width*5/32,Screen.height*4/9,Screen.width/9,Screen.height/16), "Play Again")) {
			Application.LoadLevel("Versus");
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f;
			gameManager.gameOver = false;
			gameManager.winner = false;
		}
		
	}
	
	void OnGUI () {
		
		if (gameManager.gameOver)
			showScoreScreen();
		
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
		
		GUI.Box(new Rect(0,Screen.height*2/3,Screen.width,Screen.height/3),"");
		GUI.DrawTexture(new Rect(0,Screen.height*2/3,Screen.width,Screen.height/3),background);
		GUI.Label(new Rect(Screen.width*9/19,Screen.height/9,100,20), "Bytes " + player.Money);
		tooltipPos = new Rect(Screen.width*7/16,Screen.height*2/3+Screen.height/64,Screen.width*5/32,Screen.height*1/3-Screen.height/64);
		
		if(selectedUnit==null)
			SelectedUnit = selectedUnit;
		
		if (selectedUnit != null) {
			
			selectedCircle.transform.position = selectedUnit.gameObject.transform.position;
			selectedCircle.transform.rotation = selectedUnit.gameObject.transform.rotation;
			
			var namePos = new Rect(Screen.width*3/10,Screen.height*3/4-15, 150, 20);
			GUI.Label(namePos, selectedUnit.name);
			
			
			if(healthComponent != null)
				drawHealth();
			
			if (movementComponent != null)
				drawDamageSpeed();
			
			if (factoryComponent != null) {
				factoryPos = new Rect(Screen.width*3/10,Screen.height*25/32-Screen.height/128,Screen.width/5,40);
				GUI.Label(factoryPos, "Next spawn in "+Mathf.Ceil(factoryComponent.CalculatedSpawnTime-factoryComponent.spawnTimeCounter)+"s\n"+
					"Spawn Rate "+factoryComponent.CalculatedSpawnTime+"s");
				//GUI.Label(new Rect(factoryPos.x,factoryPos.y+factoryPos.height,factoryPos.width,factoryPos.height),
				//	"Spawn Rate "+factoryComponent.CalculatedSpawnTime+"s");
			}
			
			if (upgradeComponent != null) {
			
				buttonPos = 0;
				foreach (GameObject g in buildings) {
					var buildComponent = g.GetComponent<BuildableComponent>();
					if (upgradeComponent.canBuild(buildComponent.Size)) {
						content.image = buildComponent.image;
						content.tooltip = buildComponent.Name;
						content.tooltip += "\nCost: "+buildComponent.Cost+" Bytes";
						
						var decoratorComponent = g.GetComponent<DecoratorComponent>();
						if (decoratorComponent != null) {
							var weaponComponent = decoratorComponent.MyAddon.GetComponent<WeaponComponent>();
							var addonComponent = decoratorComponent.MyAddon.GetComponent<AddonComponent>();
							if (weaponComponent != null) {
								content.tooltip += "\nDamage: "+weaponComponent.Damage+
												"\nFire Rate: "+weaponComponent.ReloadTime+
												"\nRange: "+weaponComponent.Range;
							}
							if (addonComponent != null) {
								if (addonComponent.AdditionalHealth != 0)
									content.tooltip += "\nAdditional Health: "+addonComponent.AdditionalHealth;
							}
						}
						
						content.tooltip += "\n\n"+buildComponent.tooltip;
						
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
		var widthPos = pos % Mathf.Floor((Screen.width-Screen.width*5/8)/image.width);
		var heightPos = Mathf.Floor(pos / Mathf.Floor((Screen.width-Screen.width*5/8)/image.width));
		return new UnityEngine.Rect(Screen.width*5/8+widthPos*image.width,Screen.height*2/3+Screen.height/64+heightPos*image.height,image.width,image.height);
	}
	
	void drawHealth () {
		var healthPos = new Rect(Screen.width*3/10,Screen.height*3/4, 150, 20);
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
	
	void drawDamageSpeed () {
		var damagePos = new Rect(Screen.width*3/10,Screen.height*3/4+15, 150, 40);
		GUI.Label(damagePos,"Maximum DPS: "+Mathf.Floor(movementComponent.dps)+
			"\nSpeed: "+movementComponent.speed);
	}
	
}
