using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAnimationManager : MonoBehaviour
{
	//Hold onto the sprite to change
	public SpriteRenderer faceSprite;
	
	//Default face
	public Sprite defaultSprite;
	
	//Happy Faces Array
	public Sprite[] happyFaceSprites;
	
	//Sad Faces Array
	public Sprite[] sadFaceSprites;
	
	//Timer stuff
	private bool isTiming;
	private float timer;
	public float resetFaceTime;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isTiming)
		{
			timer += Time.deltaTime;
			if (timer >= resetFaceTime)
			{
				isTiming = false;
				timer = 0;
				faceSprite.sprite = defaultSprite;
			}
		}
	}

	public void ChangeFace(Sprite sprite, bool isHappy)
	{
		faceSprite.sprite = sprite;

		if (isHappy)
		{
			timer = 0;
			isTiming = true;
		}
	}
	
	
}
