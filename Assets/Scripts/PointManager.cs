using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = System.Random;

public class PointManager : MonoBehaviour
{
	public Text pointsText;
	private float currentPoints = 0;
	public float pepperMultiplier;

	public float currentCombo = 0;
	public float comboMulitplierIncrease;
	public float comboMultiplierStart;
	private float comboMultiplierCurrent;

	public Stack<GameObject> impaledFoodStack = new Stack<GameObject>();
	public GameObject[] impaledFoodArray;
	private String impaledFoodString;

	public GameObject swordBlade;

	public Text HighScoreText;
	
	//Points variables
	private float lastPoints;
	private float thisPoints;
	private float comboPoints;
	private bool wasPepper;
	
	//Audio
	public AudioManager audioManager;
	
	private void Start()
	{
		pointsText.text = "Points: ";
		HighScoreText.text = "High Score: " + PlayerPrefs.GetFloat("HighScore");	
		swordBlade = GameObject.Find("SwordBlade");
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}

	private void Update()
	{
		//Reset high score
		if (Input.GetKeyDown(KeyCode.P))
		{
			PlayerPrefs.SetFloat("HighScore", 0);
			HighScoreText.text = "High Score: " + PlayerPrefs.GetFloat("HighScore");
		}
	}

	//We are going to call this from the ImpaleScript
	//When this is called, it will increase the variable that holds the points and then update the text to match.
	//Chops off any decimals
	public void IncreasePoints(float pointsToAdd)
	{
		currentPoints = currentPoints + pointsToAdd;
		pointsText.text = "Points: " + currentPoints.ToString("f0");
		if (currentPoints > PlayerPrefs.GetFloat("HighScore"))
		{
			PlayerPrefs.SetFloat("HighScore", currentPoints);
			HighScoreText.text = "High Score: " + currentPoints;
		}
	}

	//Call this from ImpaleScript
	//When this is called it will push the gameobject that just got stuck into the stack
	//It will also add the letter to the string
	public void AddFoodToStack(GameObject obj, Char letter)
	{
		//Stick things to the stack
		impaledFoodStack.Push(obj);
		impaledFoodString += letter;
		Debug.Log("foodStack.count = " + impaledFoodStack.Count);
		//Make an array of the stack
		impaledFoodArray = impaledFoodStack.ToArray();

		//Points stuff
		ImpaleScript foodStats = obj.GetComponent<ImpaleScript>(); //get the script off the impaled object
		thisPoints = foodStats.cookPointValue; //add the cook value of the food to the current point holder
		if (wasPepper) //check if the last object was a pepper, and if it was, multiply thisPoints
		{
			thisPoints *= pepperMultiplier;
		}
		if (foodStats.isPepper) //check if the current food is a pepper, and if it is set wasPepper to true and multiply the last points
		{
			wasPepper = true;
			lastPoints *= pepperMultiplier;
		}
		else //if it is not a pepper, set wasPepper to false
		{
			wasPepper = false;
		}

		comboPoints += lastPoints; //add the previous food's value to the comboPoints
		lastPoints = thisPoints; //make the current points value the last points value
		thisPoints = 0; //reset thisPoints just in case
	}
	
	//Call this from the grill
	//When the timer runs down, give out the combo points and reset the necessary variables
	//What if I calculate this as it happens?
	//So When food hits the sword it is added to a stack, and the relevant points values are as well
	//Every food stores its valu when it is placed on the sword
	public void ComboPayout()
	{
		//Play cook success audio
		audioManager.PlaySoundEffect(
			audioManager.Clips.grillSuccess[UnityEngine.Random.Range(0, audioManager.Clips.grillSuccess.Length)]);
		//Points calculations
		comboPoints += lastPoints; //grab the last food's points value, which will not have been updated
		IncreasePoints(comboPoints); //increase the points and display to the screen
		Debug.Log("Combo Payout = " + comboPoints);
		//Reset all of the combo variables
		comboPoints = 0;
		lastPoints = 0;
		thisPoints = 0;
		wasPepper = false;
		
		//Destroy cooked food
		DestroyCookedFood();
		//Destroy Slider Joints
		DestroySliderJoints();
	}

	//When the food is cooked, for loop through the stack and destroy all the foods
	public void DestroyCookedFood()
	{
		//for loop
		/*for (int i = 0; i < impaledFoodStack.Count + 1; i++)
		{
			GameObject cookedFood = impaledFoodStack.Pop();
			Debug.Log("cookedFood = " + cookedFood.name);
			Destroy(cookedFood);
		}*/
		Debug.Log("DestroyCookedFood");
		impaledFoodStack.Clear(); //clear out the stack so it doesn't keep it cooking
		FixedJoint2D[] oldFixed = swordBlade.GetComponents<FixedJoint2D>();
		foreach (FixedJoint2D oldFix in oldFixed)
		{
			Destroy(oldFix);
		}
	}
	
	//When the cooked food is destroyed, grab all the slider joints on the blade and destroy them
	public void DestroySliderJoints()
	{
		Debug.Log("DestroySliderJoints");
		//iterate through an make all colliders on the food on the blade triggers
		foreach (GameObject cookedFood in impaledFoodArray)
		{
			cookedFood.GetComponent<Collider2D>().isTrigger = true;
		}
		//Grab an array of all the sliders on the blade
		SliderJoint2D[] oldSliders = swordBlade.GetComponents<SliderJoint2D>();
		//iterate through and delete the sliders
		foreach (SliderJoint2D oldSlider in oldSliders)
		{
			Destroy(oldSlider);
		}
		
	}

	//When you click, pop the top object from the stack and delete the fixed joint
	public void DetachFood()
	{
		if(Input.GetMouseButtonDown(0))
		{
			GameObject poppedFood = impaledFoodStack.Pop();
			Debug.Log("Popped food is " + poppedFood);
			Destroy(poppedFood.GetComponent<FixedJoint2D>());
			Debug.Log("Food joint destroyed");
		}
	}

	public void RoundScore(float points, int decimalPlaces)
	{
		currentPoints = Mathf.Round(points * Mathf.Pow(10, decimalPlaces));
	}
}
