using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

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
	private String impaledFoodString;

	public GameObject swordBlade;

	public Text HighScoreText;
	
	//Points variables
	private float lastPoints;
	private float thisPoints;
	private float comboPoints;
	private bool wasPepper;
	
	private void Start()
	{
		pointsText.text = "Points: ";
		HighScoreText.text = "High Score" + PlayerPrefs.GetFloat("HighScore");	

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
			HighScoreText.text = "High Score" + currentPoints;
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
		//Points calculations
		comboPoints += lastPoints; //grab the last food's points value, which will not have been updated
		IncreasePoints(comboPoints); //increase the points and display to the screen
		//Reset all of the combo variables
		comboPoints = 0;
		lastPoints = 0;
		thisPoints = 0;
		wasPepper = false;
		
		//Destroy cooked food
		DestroyCookedFood();
		//Destroy Slider Joints
		DestroySliderJoints();
		
		

		/*if (impaledFoodStack.Count != 0)
		{
			float comboTotal = 0;
			float comboMultiplier = 1;
			float lastPoints = 0;
			float points = 0;
			bool wasPepper = false;

			for (int i = 0; i < impaledFoodStack.Count; i++)
			{
				GameObject poppedFood = impaledFoodStack.Pop();
				if (poppedFood != null)
				{
					ImpaleScript poppedFoodScript = poppedFood.GetComponent<ImpaleScript>();

					points = poppedFoodScript.impalePointValue;

					if (i == 0)
					{
						comboTotal += points;
					}
					else
					{
						if (wasPepper)
						{
							points *= pepperMultiplier;
							wasPepper = false;
						}

						if (poppedFoodScript.isPepper)
						{
							wasPepper = true;
							lastPoints *= pepperMultiplier;
						}

						comboTotal += lastPoints;
					}

					lastPoints = points;
					comboMultiplier += comboMulitplierIncrease;

					Destroy(poppedFood);
				}
			}

			SliderJoint2D[] oldSliders = swordBlade.GetComponents<SliderJoint2D>();

			for (int i = 0; i < oldSliders.Length; i++)
			{
				Destroy(oldSliders[i]);
			}

			comboTotal *= comboMultiplier;
			IncreasePoints(comboTotal);
		}*/

	}

	//When the food is cooked, for loop through the stack and destroy all the foods
	public void DestroyCookedFood()
	{
		//for loop
		for (int i = 0; i < impaledFoodStack.Count + 1; i++)
		{
			GameObject cookedFood = impaledFoodStack.Pop();
			Destroy(cookedFood);
		}
		impaledFoodStack.Clear(); //clear out the stack so it doesn't keep it cooking
	}
	
	//When the cooked food is destroyed, grab all the slider joints on the blade and destroy them
	public void DestroySliderJoints()
	{
		//Grab an array of all the sliders on the blade
		SliderJoint2D[] oldSliders = swordBlade.GetComponents<SliderJoint2D>();
		//iterate through and delete the sliders
		for (int i = 0; i < oldSliders.Length; i++)
		{
			Destroy(oldSliders[i]);
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
