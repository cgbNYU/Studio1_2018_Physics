using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class SwordMouseFollow : MonoBehaviour {
	
	//Mouse Variable holds the mouse position relative to the camera
	//This means that the mouse is always assumed to be on screen even in windowed mode
	private Vector2 previousMousePos;
	private Vector2 currentMousePos;

	private float previousMouseX;
	private float previousMouseY;
	private float currentMouseX;
	private float currentMouseY;
	
	private Vector3 updatedPosition;

	public float vectorDamp; //reduce the scale of the vector
	public float vectorClamp; //maximum scale of the vector
	public Vector2 initialPos; //center of the radius within which this thing can move

	private Rigidbody2D rb;

	public GameObject swordBlade;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		/*//Get current mouse position
		currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		//Get difference between previous pos and current pos
		Vector3 posDiff = currentMousePos - previousMousePos;
		
		//Make this object move with the mouse
		posDiff = posDiff * vectorDamp;
		updatedPosition += posDiff;
		rb.position = updatedPosition;
		
		//Set previousMousePos to currentMousePos for the next frame
		previousMousePos = currentMousePos;*/

		MouseFollow();
	}

	private void MouseFollow()
	{	
		currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		var allowedPos = currentMousePos - initialPos;

		allowedPos = Vector2.ClampMagnitude(allowedPos, vectorClamp);

		rb.position = initialPos + allowedPos;
	}
}
