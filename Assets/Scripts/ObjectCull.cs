using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCull : MonoBehaviour {
	
	//Remove objects when they leave the screen
	public float offScreenBuffer;
	
	// Update is called once per frame
	void Update () 
	{
		OffScreenCull();
	}

	public void OffScreenCull()
	{
		if (transform.position.y < offScreenBuffer)
		{
			Destroy(gameObject);
		}
	}
}
