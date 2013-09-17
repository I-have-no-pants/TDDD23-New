using UnityEngine;
using System.Collections;

/// <summary>
/// Attach a derived type to an object for letting it know if the player clicks it.
/// </summary>
public abstract class ClickComponent : MonoBehaviour {
	
	abstract public void OnClick();
}
