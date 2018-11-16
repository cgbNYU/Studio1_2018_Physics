using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpaleScript : MonoBehaviour {
	
	//When this object hits the sword tip trigger, create a slider joint in the direction of the sword
	
	//Joint variable
	private SliderJoint2D foodSlide;
	
	//Variables for sword gameobject and rigidbody
	public GameObject blade;
	public Rigidbody2D bladeRB;
	
	//Joint limit floats
	public float jointLimitMin;
	public float jointLimitMax;
	
	//Bools
	public bool isOffTip;
	private bool isImpaled = false;
	
	//Stick Trigger
	private GameObject stickToSword;

	// Use this for initialization
	void Start ()
	{
		//Debugging
		Debug.Log("Food slide is " + foodSlide);
		
		//Initialize
		stickToSword = GameObject.Find("StickToSwordTrigger");
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (foodSlide != null)
		{
			//Debugging
			Debug.Log("Joint angle = " + foodSlide.angle);
			Debug.Log("Joint Limit Min = " + foodSlide.limits.min);
			Debug.Log("Joint Limit Max = " + foodSlide.limits.max);
			
			CheckDropObject();
		}
	}

	//Trigger checks
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("SwordTip") && !isImpaled)
		{
			Impaled(other);
		}

		if (other.CompareTag("SwordStick") && isImpaled)
		{
			
		}
	}

	public void CheckDropObject()
	{
		//Drop the food if it reaches the lower limit of the joint
		//First it checks to make sure it has had a chance to slide down the sword a bit so it doesn't fall right away
		JointLimitState2D foodSlideLimitState = foodSlide.limitState;

		if (foodSlideLimitState > JointLimitState2D.LowerLimit)
		{
			isOffTip = true;
			Debug.Log("is off tip = " + isOffTip);
		}
		
		if (foodSlideLimitState == JointLimitState2D.LowerLimit && isOffTip)
		{
			Debug.Log("Joint Destroyed!");
			isImpaled = false;
			isOffTip = false;
			Destroy(foodSlide);
		}
	}

	//Delete this object's rigidbody 2D and make it a child of the blade
	public void StickToSword()
	{
		Debug.Log("Stick to sword");
	}

	public void Impaled(Collider2D other)
	{
		//Get blade rigidbody and gameobject
		blade = other.transform.parent.gameObject;
		bladeRB = other.GetComponentInParent<Rigidbody2D>();
			
		//Create the joint between this object and the blade, and set the parameters the way I want
		foodSlide = blade.AddComponent<SliderJoint2D>();
		foodSlide.connectedBody = GetComponent<Rigidbody2D>();
		foodSlide.autoConfigureAngle = false;
		foodSlide.useLimits = true;
			
		Debug.Log("Connected RigidBody = " + foodSlide.connectedBody); //connection debug
		foodSlide.angle = 0;

		JointTranslationLimits2D foodSlideLimits = foodSlide.limits;
		foodSlideLimits.min = jointLimitMin;
		foodSlideLimits.max = jointLimitMax;

		foodSlide.limits = foodSlideLimits;
		
		//Set bools
		isOffTip = false;
		isImpaled = true;
	}
}
