using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public Text HighScoreText;
	
	private void Start()
	{
		pointsText.text = "Points: ";
		HighScoreText.text = "High Score" + PlayerPrefs.GetFloat("HighScore");	

	}

	private void Update()
	{
		//if (impaledFoodStack != null)
		//	{
		//	DetachFood();
		//}
		
		
		
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
		impaledFoodStack.Push(obj);
		impaledFoodString += letter;
	}
	
	//Call this from the grill
	//When the timer runs down, give out the combo points and reset the necessary variables
	public void ComboPayout()
	{
		if (impaledFoodStack.Count != 0)
		{
			float comboTotal = 0;
			float comboMultiplier = 1;
			float lastPoints = 0;
			float points = 0;
			bool wasPepper = false;

			for (int i = 0; i < impaledFoodStack.Count; i++)
			{
				GameObject poppedFood = impaledFoodStack.Pop();
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

			comboTotal *= comboMultiplier;
			IncreasePoints(comboTotal);
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
