using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMouseFollow : MonoBehaviour {
	
	//This code is going to check to see where the mouse is relative to the sword tip, then push the sword tip in that direction
	
	//Movement Variables
	//We are going to use this to adjust how fast the blade tip moves towards the mouse in the editor
	public float moveForce;
	
	//Mouse Variable holds the mouse position relative to the camera
	//This means that the mouse is always assumed to be on screen even in windowed mode
	private Vector3 mousePos;
	private Vector3 mouseDif;
	
	//Position variable
	//This is going to hold this game object's position
	//Just makes it easier to type
	private Vector3 tipPos;
	
	//Holds tip rigidbody
	public Rigidbody2D rb;
	
	//Bools
	private bool debug = false;

	// Use this for initialization
	void Start ()
	{
		//initilaizing this in start because it doesn't like to do it up there
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		//We have to make sure there is no depth happening because Input.mousePosition returns a Vector3
		mousePos.z = 0;
		
		//Temporary variable that holds the vector between the sword tip and the mouse
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		tipPos = gameObject.transform.position;
		mouseDif = mousePos - tipPos;
		
		//Applys force to the sword tip in the direction of the mouse
		rb.AddForce(mouseDif.normalized * moveForce);
		
		//Debug
		if (Input.GetKeyDown(KeyCode.D))
		{
			debug = true;
		}
		DebugMouseFollow();
	}

	public void DebugMouseFollow()
	{
		if (debug)
		{
			Debug.Log("tipPos = " + tipPos);
			Debug.Log("mouseDif = " + mouseDif);
		}
	}
}
