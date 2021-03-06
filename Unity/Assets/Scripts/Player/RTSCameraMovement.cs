﻿using UnityEngine;
using System.Collections;

/// <summary>
/// RTS camera movement.
/// Zoom is disable due to some bug.
/// </summary>
public class RTSCameraMovement : MonoBehaviour
{
	
	public float ScrollSpeed = 25f;
	private float previousScrollSpeed = 0f;
	public float ScrollEdge = 0.01f;
	public float MobileSpeed = 0.1f;
	private int HorizontalScroll = 1;
	private int VerticalScroll = 1;
	private int DiagonalScroll = 1;
	public float PanSpeed = 10;
	public Vector2 ZoomRange = new Vector2 (-15, 15);
	public float CurrentZoom = 0;
	public float ZoomZpeed = 1;
	public float ZoomRotation = 1;
	private Vector3 InitPos;
	private Vector3 InitRotation;
	
	public float KeyboardZoomSpeed;
	
	public float RotateSpeed;
	
	public Rect CameraBounds;
	
	public float MouseRotateSpeed;
	
	public GameObject MyCamera;

	// Use this for initialization
	void Start ()
	{
		InitPos = transform.position;
		InitRotation = MyCamera.transform.eulerAngles;
		previousScrollSpeed = ScrollSpeed;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		//PAN
		if (Input.GetKey ("mouse 2")) {
			//(Input.mousePosition.x - Screen.width * 0.5)/(Screen.width * 0.5)
           
			transform.Translate (transform.right * Time.deltaTime * PanSpeed * (Input.mousePosition.x - Screen.width * 0.5f) / (Screen.width * 0.5f), Space.World);
			transform.Translate (transform.forward * Time.deltaTime * PanSpeed * (Input.mousePosition.y - Screen.height * 0.5f) / (Screen.height * 0.5f), Space.World);
     
		} else {
			if (Input.GetAxis("Horizontal") > 0 || Input.mousePosition.x >= Screen.width * (1 - ScrollEdge)) {
				transform.Translate (transform.right * Time.deltaTime * ScrollSpeed, Space.World);
			} else if (Input.GetAxis("Horizontal") < 0 || Input.mousePosition.x <= Screen.width * ScrollEdge) {
				transform.Translate (transform.right * Time.deltaTime * -ScrollSpeed, Space.World);
			}
           
			if (Input.GetAxis("Vertical") > 0 || Input.mousePosition.y >= Screen.height * (1 - ScrollEdge)) {
				transform.Translate (transform.forward * Time.deltaTime * ScrollSpeed, Space.World);
			} else if (Input.GetAxis("Vertical") < 0  || Input.mousePosition.y <= Screen.height * ScrollEdge) {
				transform.Translate (transform.forward * Time.deltaTime * -ScrollSpeed, Space.World);
			}
		}
		
		if (Input.GetAxis("Rotate") < 0) {
			transform.Rotate(new Vector3(0,RotateSpeed*Time.deltaTime,0));
		} else if (Input.GetAxis("Rotate") > 0) {
			transform.Rotate(new Vector3(0,-RotateSpeed*Time.deltaTime,0));
		}
        
		if (Input.touchCount > 0 && 
		  Input.GetTouch (0).phase == TouchPhase.Moved) {
		
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
			
			// Move object across XY plane
			transform.Translate (-touchDeltaPosition.x * MobileSpeed, 
						-touchDeltaPosition.y * MobileSpeed, 0);
		}
       	// Increase scroll speed.
		if (Input.GetAxis("Boost") > 0) {
			if (previousScrollSpeed == ScrollSpeed) 
					ScrollSpeed = 50f;
		} else       
			ScrollSpeed = previousScrollSpeed;
		
		
		
		if (Input.GetAxis ("Zoom Key") < 0) {
			CurrentZoom  -= Time.deltaTime * KeyboardZoomSpeed * ZoomZpeed;
		} else if (Input.GetAxis ("Zoom Key") > 0) {
			CurrentZoom  += Time.deltaTime * KeyboardZoomSpeed * ZoomZpeed;
		}
		
		//ZOOM IN/OUT
		if (Input.GetKey(KeyCode.LeftControl)) {
			transform.Rotate(new Vector3(0,Input.GetAxis ("Zoom") * Time.deltaTime * MouseRotateSpeed,0));
		} else {
			CurrentZoom -= Input.GetAxis ("Zoom") * Time.deltaTime * 1000 * ZoomZpeed;
		}
		
		CurrentZoom = Mathf.Clamp (CurrentZoom, ZoomRange.x, ZoomRange.y);
       
		transform.Translate(0,-(transform.position.y - CurrentZoom)*Time.deltaTime, 0);
		
		
		//Debug.Log (transform.position.y + CurrentZoom);
		//transform.Translate(0,CurrentZoom, 0);
		MyCamera.transform.Rotate((-MyCamera.transform.eulerAngles.x + (InitRotation.x + CurrentZoom * ZoomRotation))*Time.deltaTime,0,0);
			
		
		
		
		// Map bounds
		
		float xBound = Mathf.Min(Mathf.Max(transform.position.x,CameraBounds.xMin),CameraBounds.xMax);
		float zBound = Mathf.Min(Mathf.Max(transform.position.z,CameraBounds.yMin),CameraBounds.yMax);
		
		transform.position = new Vector3(xBound,transform.position.y,zBound);
		
		
	}
}
