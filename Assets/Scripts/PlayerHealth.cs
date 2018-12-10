using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

	public int hitPoints;

	public GameObject frontMouse;
	public GameObject backMouse;

	public HingeJoint2D backCalfJoint;
	public SliderJoint2D frontCalfJoint;

	private bool isGameOver = false;

	// Use this for initialization
	void Start ()
	{
	
	}

	public void GetHit()
	{
		//reduce health
		hitPoints--;

		if (hitPoints <= 0 && !isGameOver)
		{
			isGameOver = true;
			GameOver();
		}
	}

	public void GameOver()
	{
		//player loses control
		
		//Destroy the mouse control objects so the player loses control
		Destroy(frontMouse);
		Destroy(backMouse);
		//Destroy the joints that fix the player to the floor so it can fall down
		Destroy(backCalfJoint);
		Destroy(frontCalfJoint);
	}
}
