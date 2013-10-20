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
				teamComponent = value.GetComponent<TeamComponent>();
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
	private GameManagerComponent gameManager;
	public List<GameObject> buildings;
	private int buttonPos;
	private Rect tooltipPos,infoPos;
	private HealthComponent healthComponent;
	private UpgradeableComponent upgradeComponent;
	private bool paused = false;
	private bool chooseDifficulty = true;
	private FactoryComponent factoryComponent;
	private PathfindMovement movementComponent;
	public Texture sellImage;
	private TeamComponent teamComponent;
	
	public PlayerComponent player;
	public EnemyPlayer enemy;
	
	void Start () {
		gameManager = GameObject.FindObjectOfType(typeof(GameManagerComponent)) as  GameManagerComponent;
		buildings = gameManager.Buildings;
		player = GameObject.Find ("Player").GetComponent<PlayerComponent>();
		enemy = GameObject.Find ("Enemy").GetComponent<EnemyPlayer>();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (paused) {
				Time.timeScale = 1f;
				Time.fixedDeltaTime = 0.02f;
				paused = !paused;
			} else {
				Time.timeScale = 0f;
				Time.fixedDeltaTime = 0f;
				paused = !paused;
			}
		} 
	}
	
	void OnGUI () {
		
		if (chooseDifficulty) {
			drawDifficulty();
		}
		
		if (gameManager.gameOver)
			showScoreScreen();
		
		if (paused)
			pauseGame();
		// Background with an unclickable box.
		drawBackground();
		
		// Displays money and income per second.
		drawIncome();
		
		// Tooltip position
		tooltipPos = new Rect(Screen.width*7/16,Screen.height*2/3+Screen.height/64,Screen.width*5/32,Screen.height*1/3-Screen.height/64);
		infoPos = new Rect(Screen.width*5/16,Screen.height*2/3+Screen.height/64,Screen.width*5/32,Screen.height*1/3-Screen.height/64);
		
		if(selectedUnit==null)
			SelectedUnit = selectedUnit;
		
		if (selectedUnit != null) {
			
			selectedCircle.transform.position = selectedUnit.gameObject.transform.position;
			selectedCircle.transform.rotation = selectedUnit.gameObject.transform.rotation;
			
			// Name
			if (movementComponent != null || factoryComponent != null || selectedUnit.name.CompareTo("Mother Core") == 0 || selectedUnit.name.CompareTo("Turret") == 0)
				GUI.Label(infoPos, selectedUnit.name);
			// Health
			if(healthComponent != null)
				drawHealth();
			// Damage and attack speed
			if (movementComponent != null)
				drawDamageSpeed();
			// Spawn time from factory
			if (factoryComponent != null) {
				drawFactory();
			}
			if ((selectedUnit.name.CompareTo("Turret") == 0 || factoryComponent != null) && teamComponent.MyTeam.CompareTo("TeamPlayer") == 0)
				sellBuilding();
			// Upgrades
			if (upgradeComponent != null) 
				drawUpgrades();
		}
	}
	
	void drawDifficulty () {
		Time.timeScale = 0f;
		Time.fixedDeltaTime = 0f;
		var pos = new Rect(Screen.width/2,Screen.height*2/9,400,200);
		GUI.Box(new Rect(pos.x-200,pos.y,pos.width,pos.height),"");
		var style = new GUIStyle();
		style.fontSize = 30;
		style.normal.textColor = Color.white;
		GUI.Label(new Rect(pos.x-100,pos.y+10,200,pos.height),"Choose Difficulty",style);
		GUI.color = Color.red;
		if (GUI.Button(new Rect(pos.x-50,pos.y+50,100,20),"Hard")) {
			enemy.InitialTimeBetweenBuild = new Vector2(1,2);
			enemy.TimeBetweenBuild = new Vector2(2,6);
			chooseDifficulty = false;
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f;
		}
		GUI.color = Color.yellow;
		if (GUI.Button(new Rect(pos.x-50,pos.y+90,100,20),"Medium")) {
			enemy.InitialTimeBetweenBuild = new Vector2(2,3);
			enemy.TimeBetweenBuild = new Vector2(6,8);
			chooseDifficulty = false;
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f;
		}
		GUI.color = Color.green;
		if (GUI.Button(new Rect(pos.x-50,pos.y+130,100,20),"Easy")) {
			enemy.InitialTimeBetweenBuild = new Vector2(3,4);
			enemy.TimeBetweenBuild = new Vector2(8,10);
			chooseDifficulty = false;
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f;
		}
		GUI.color = Color.white;
	}
	
	void showScoreScreen () {
		Time.timeScale = 0f;
		Time.fixedDeltaTime = 0f;
		var elapsedTime = Time.timeSinceLevelLoad;
		var pos = new Rect(Screen.width/2,150,280,200);
		GUI.Box(new Rect(pos.x-120,pos.y,pos.width,pos.height),"");
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
		GUI.Label(new Rect(pos.x-60,pos.y+15,300,30),""+whoWon,style);
		style.fontSize = 16;
		style.normal.textColor = Color.white;
		GUI.Label(new Rect(pos.x-60,pos.y+70,150,20),
			"Time: "+Mathf.Floor(elapsedTime/60)+" m "+Mathf.Floor(elapsedTime % 60)+" s\n" +
			"Units Destroyed: "+gameManager.unitsDestroyed+"\n"+
			"Units Built: "+gameManager.unitsBuilt+"\n"+
			"Buildings Destroyed: "+gameManager.buildingsDestroyed+"\n"+
			"Most Expensive Unit: "+gameManager.mostExpensiveUnit,
			style);
		//Main Menu Button
		if (GUI.Button(new Rect(pos.x-100,pos.y+pos.height-30,100,20), "Main Menu")) {
			Application.LoadLevel("menu");
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f;
			gameManager.gameOver = false;
			gameManager.winner = false;
		}
		//Restart Button
		if (GUI.Button(new Rect(pos.x+10,pos.y+pos.height-30,100,20), "Play Again")) {
			Application.LoadLevel("Versus");
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f;
			gameManager.gameOver = false;
			gameManager.winner = false;
		}
		
	}
	
	void pauseGame () {
		var style = new GUIStyle();
		style.fontSize = 30;
		style.normal.textColor = Color.white;
		var pausePos = new Rect(Screen.width/2,Screen.height*4/10,240,110);
		GUI.Box(new Rect(pausePos.x-130,pausePos.y,pausePos.width,pausePos.height),"");
		GUI.Label(new Rect(pausePos.x-120,pausePos.y+10,pausePos.width,pausePos.height),"GAME PAUSED",style);
		if (GUI.Button(new Rect(pausePos.x-60,pausePos.y+50,100,20),"Unpause")) {
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f;
			paused = false;
		}
		if (GUI.Button(new Rect(pausePos.x-60,pausePos.y+80,100,20),"Exit to menu")) {
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f;
			Application.LoadLevel("menu");
		}
	}
	
	void drawUpgrades () {
		buttonPos = 0;
		foreach (GameObject g in buildings) {
			var buildComponent = g.GetComponent<BuildableComponent>();
			if (upgradeComponent.canBuild(buildComponent.Size)) {
				var content = new GUIContent();
				content.image = buildComponent.image;
				content.tooltip = buildComponent.Name;
				content.tooltip += "\n\nCost: "+buildComponent.Cost+" Bytes";
				
				var decoratorComponent = g.GetComponent<DecoratorComponent>();
				var weaponComponentTrack = g.GetComponent<WeaponComponentTrack>();
				if (decoratorComponent != null) {
					var weaponComponent = decoratorComponent.MyAddon.GetComponent<WeaponComponent>();
					var addonComponent = decoratorComponent.MyAddon.GetComponent<AddonComponent>();
					if (weaponComponent != null) {
						content.tooltip += "\nDamage: "+weaponComponent.Damage+
										"\nFire Rate: "+Mathf.Floor(1/weaponComponent.ReloadTime)+" / s"+
										"\nRange: "+weaponComponent.Range;
					}
					if (addonComponent != null) {
						if (addonComponent.AdditionalHealth != 0 && buildComponent.Name.CompareTo("Turret") == -1)
							content.tooltip += "\nAdditional Health: "+addonComponent.AdditionalHealth;
					}
				} else if (weaponComponentTrack != null) {
						content.tooltip += "\nDamage: "+weaponComponentTrack.Damage+
										"\nFire Rate: "+Mathf.Floor(1/weaponComponentTrack.ReloadTime)+" / s"+
										"\nRange: "+weaponComponentTrack.Range;
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
	
	Rect nextPosition(int pos, Texture image) {
		var widthPos = pos % Mathf.Floor((Screen.width-Screen.width*5/8)/image.width);
		var heightPos = Mathf.Floor(pos / Mathf.Floor((Screen.width-Screen.width*5/8)/image.width));
		return new UnityEngine.Rect(Screen.width*5/8+widthPos*image.width,Screen.height*2/3+Screen.height/64+heightPos*image.height,image.width,image.height);
	}
	
	void drawHealth () {
		var healthPos = new Rect(infoPos.x,tooltipPos.y+15, 150, 20);
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
		var damagePos = new Rect(infoPos.x,infoPos.y+30, 150, 40);
		GUI.Label(damagePos,"Maximum DPS: "+Mathf.Floor(movementComponent.dps)+
			"\nSpeed: "+movementComponent.speed);
	}
	
	void drawFactory () {
		GUI.Label(new Rect(infoPos.x,infoPos.y+30, 150, 40), "Next spawn in "+Mathf.Ceil(factoryComponent.CalculatedSpawnTime-factoryComponent.spawnTimeCounter)+"s\n"+
					"Spawn Rate "+factoryComponent.CalculatedSpawnTime+"s");
		
	}
	
	void sellBuilding () {
		var pos = nextPosition(0,sellImage);
		var content = new GUIContent();
		var buildComponent = selectedUnit.GetComponent<BuildableComponent>();
		var cost = buildComponent.calculateTotalCost()*3/4;
		content.image = sellImage;
		content.tooltip = "Deallocates the building from memory.\nRegain "+cost+" bytes (75%)";
		if (GUI.Button(pos,content)) {
			healthComponent.Health = 0;
			player.Money += cost;
			//selectedUnit = null;
		}
		GUI.Label(tooltipPos, GUI.tooltip);
	}
	
	void drawBackground () {
		GUI.Box(new Rect(0,Screen.height*2/3,Screen.width,Screen.height/3),"",new GUIStyle());
		GUI.DrawTexture(new Rect(0,Screen.height*2/3-3,Screen.width,Screen.height/3+3),background);
	}
	
	void drawIncome () {
		var style = new GUIStyle();
		style.normal.textColor = Color.yellow;
		style.fontSize = 22;
		GUI.Label(new Rect(Screen.width-180,Screen.height/128,180,20), "Bytes: " + player.Money + " (+"+player.IncomePerSecond+"/s)", style);
	}
}
