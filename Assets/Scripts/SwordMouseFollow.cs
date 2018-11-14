using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class SwordMouseFollow : MonoBehaviour {
	
	//Mouse Variable holds the mouse position relative to the camera
	//This means that the mouse is always assumed to be on screen even in windowed mode
	private Vector3 mousePos;
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		//Get mouse position
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//We have to make sure there is no depth happening because Input.mousePosition returns a Vector3
		mousePos.z = 0;
		
		//Make this object move with the mouse
		transform.position = mousePos;
	}

}
