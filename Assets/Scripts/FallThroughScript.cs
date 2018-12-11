using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThroughScript : MonoBehaviour
{
	//If the object hits anything but the floor, move it to the fallthrough layer (9)
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (!other.gameObject.CompareTag("Floor"))
		{
			gameObject.layer = 9;
		}
	}
}
