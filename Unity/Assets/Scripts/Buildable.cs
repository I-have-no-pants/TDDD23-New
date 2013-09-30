using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour {
	
	public int Size;
	public int Cost;
	public string Name;
	public string Description;
	
	private TeamComponent myTeam;
	
	void Start() {
		// Hide addon position if we are not in players team
		myTeam = GetComponent<TeamComponent>();
		//if (myTeam != "TeamPlayer")
			
	}
		
}
