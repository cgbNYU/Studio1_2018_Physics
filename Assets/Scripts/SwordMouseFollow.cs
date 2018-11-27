﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class SwordMouseFollow : MonoBehaviour {
	
	//Mouse Variable holds the mouse position relative to the camera
	//This means that the mouse is always assumed to be on screen even in windowed mode
	private Vector2 previousMousePos;
	private Vector2 currentMousePos;
	
	private Vector3 updatedPosition;

	public MouseInput mouseI;

	public int whichMouseNum; //variable used to determine which mouse should control this object

	public float vectorDamp; //reduce the scale of the vector

	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		//Get current mouse position
		currentMousePos = mouseI.move[whichMouseNum];
		
		//Get difference between previous pos and current pos
		Vector3 posDiff = currentMousePos - previousMousePos;		
		
		//Make this object move with the mouse
		//transform.position += posDiff * Time.deltaTime;

		posDiff = posDiff * vectorDamp;

		updatedPosition += posDiff;
		
		Debug.Log("posDiff = " + posDiff);
		
		rb.MovePosition(updatedPosition);
		
		//Set previousMousePos to currentMousePos for the next frame
		previousMousePos = currentMousePos;
	}

}
