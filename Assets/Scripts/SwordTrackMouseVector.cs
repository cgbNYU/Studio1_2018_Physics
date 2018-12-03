using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrackMouseVector : MonoBehaviour
{
	public float moveForce;
	public Rigidbody2D swordRB;
	private Rigidbody2D rb;

	private Vector3 lastMousePos;
	private Camera mainCamera;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		mainCamera = Camera.main;
		
		lastMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		GetMouseTrajectory();
	}

	public void GetMouseTrajectory()
	{
		Vector3 currentMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

		if (currentMousePos != lastMousePos)
		{
			Vector3 mouseDif = currentMousePos - lastMousePos;
			rb.position = currentMousePos;
			MoveSword(mouseDif);
		}
	}

	public void MoveSword(Vector3 mouseDif)
	{
		swordRB.AddForce(mouseDif * moveForce);
	}
}
