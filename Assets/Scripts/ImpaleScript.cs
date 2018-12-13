using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class ImpaleScript : MonoBehaviour {
	
	//When this object hits the sword tip trigger, create a slider joint in the direction of the sword
	//Also, increase the score by pointsValue
	
	//points values for impaling and cooking
	public int impalePointValue;
	public int cookPointValue;
	
	//Bool that stores whether it is a pepper or not
	public bool isPepper;
	
	//point manager
	public PointManager pointManager;
	
	//CookScript
	public CookScript cookScript;
	public Color cookedColor;
	public Color originalColor;
	
	//Joint variables
	private SliderJoint2D foodSlide;
	private FixedJoint2D foodStickJoint;
	
	//Variables for sword gameobject and rigidbody
	public GameObject blade;
	public Rigidbody2D bladeRB;
	
	//Joint limit floats
	public float jointLimitMin;
	public float jointLimitMax;
	
	//Break force and new friction
	public float breakForce;
	public float impaledFriction;
	
	//Slide speed
	public float slideSpeed;
	
	//Impale Angle
	//Something close to -1
	public float impaleAngle;
	
	//Bools
	public bool isOffTip;
	private bool isImpaled = false;
	private bool isStuck = false;
	
	//Stick Trigger
	private GameObject stickToSword;
	
	//This object's rigidbody
	private Rigidbody2D rb;
	
	//Food Letter Value
	public Char foodLetter;
	
	//Audio Manager
	public AudioManager audioManager;
	
	//Face manager
	public FaceAnimationManager faceManager;

	// Use this for initialization
	void Start ()
	{
		//Initialize
		stickToSword = GameObject.Find("StickToSwordTrigger");
		rb = GetComponent<Rigidbody2D>();
		pointManager = GameObject.Find("PointManager").GetComponent<PointManager>();
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		faceManager = GameObject.Find("FirstPlayerController").GetComponent<FaceAnimationManager>();
		cookScript = GameObject.Find("CookTrigger").GetComponent<CookScript>();
	}

	//Trigger checks
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("SwordTip") && foodSlide == null)
		{
			CheckAngle(other);
		}

		if (other.CompareTag("Hilt") && foodSlide != null && !isStuck)
		{
			StickToSword();
			//Debug.Log("Trigger Stick " + gameObject.name);
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (foodSlide != null && other.gameObject.CompareTag("Food") && !isStuck) //if this object is on the blade and the other object is stuck on the blade
		{
			if (other.gameObject.GetComponent<ImpaleScript>().isStuck)
			{
				StickToSword();
				//Debug.Log("Collision Stick " + gameObject.name);
			}
		}
	}

	//Delete this object's rigidbody 2D and make it a child of the blade
	public void StickToSword()
	{
		//Reset the cooking timer
		cookScript.timer = 0;
		
		foodStickJoint = blade.AddComponent<FixedJoint2D>();
		foodStickJoint.connectedBody = rb;

		//gameObject.layer = 11;

		isStuck = true;
		pointManager.AddFoodToStack(gameObject, foodLetter);
		
		//Debug.Log("isStuck = " + isStuck);
	}

	public void CheckAngle(Collider2D other)
	{
		//Get blade rigidbody and gameobject
		blade = other.transform.parent.gameObject;
		bladeRB = other.GetComponentInParent<Rigidbody2D>();

		Vector2 stabAngle = rb.velocity - bladeRB.velocity;

		if (Vector2.Dot(stabAngle, blade.transform.right) <= impaleAngle)
		{
			Impaled();
		}
	}

	//When it is determined the angle of impact is correct, impale the food on the sword
	public void Impaled()
	{	
		//Play sound
		if (foodLetter == 'o')
		{
			audioManager.PlaySoundEffect(audioManager.Clips.onionSound);
		}
		else if (foodLetter == 'm')
		{
			audioManager.PlaySoundEffect(audioManager.Clips.meatSound);
		}
		else 
		{
			audioManager.PlaySoundEffect(audioManager.Clips.impaledSounds[UnityEngine.Random.Range(0, audioManager.Clips.impaledSounds.Length)]);	
		}
		
		//Change face
		faceManager.ChangeFace(faceManager.happyFaceSprites[UnityEngine.Random.Range(0, faceManager.happyFaceSprites.Length)], true);
		
		//Give the player points
		pointManager.IncreasePoints(impalePointValue);
		
		//Create the joint between this object and the blade, and set the parameters the way I want
		foodSlide = blade.AddComponent<SliderJoint2D>();
		foodSlide.connectedBody = rb;
		foodSlide.autoConfigureAngle = false;
		//foodSlide.autoConfigureConnectedAnchor = true;
		foodSlide.useLimits = true;
		foodSlide.breakForce = breakForce;
		foodSlide.angle = 0;

		JointTranslationLimits2D foodSlideLimits = foodSlide.limits;
		foodSlideLimits.min = jointLimitMin;
		foodSlideLimits.max = jointLimitMax;

		foodSlide.limits = foodSlideLimits;
		
		//Set bools
		isOffTip = false;
		isImpaled = true;
	}
	
	//Increase drag on impaled object
	public void ImpaledDrag()
	{
		float bvel = Mathf.Abs(Vector2.Dot(rb.velocity.normalized, (Vector2)blade.transform.right));
		rb.AddForce(-bvel * bvel * impaledFriction * rb.velocity.normalized);
	}
	
	//Tween sprite color as the food cooks
	public void CookTween()
	{
		SpriteRenderer foodColor = GetComponentInChildren<SpriteRenderer>(); //grab the color from the sprite renderer
		foodColor.DOColor(cookedColor, cookScript.cookTime);
	}
	
	//Tween sprite color as the food cools
	public void CoolTween()
	{
		SpriteRenderer foodColor = GetComponentInChildren<SpriteRenderer>(); //grab the sprite renderer
		foodColor.DOColor(originalColor, cookScript.cookTime * 2);
	}
}
