using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAnimationManager : MonoBehaviour
{
	//Hold onto the sprite to change
	public SpriteRenderer faceSprite;
	
	//Happy Faces Array
	public Sprite[] happyFaceSprites;
	
	//Sad Faces Array
	public Sprite[] sadFaceSprites;
	
	//Timer stuff
	private float timer;
	public float impaleTime;
	public float cookTime;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeFace(Sprite sprite, bool isHappy)
	{
		
	}
}
