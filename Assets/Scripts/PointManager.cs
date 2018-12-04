using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
	
	public Text pointsText;
	public int currentPoints = 0;

	private void Start()
	{
		pointsText.text = "Points: ";
	}

	//We are going to call this from the ImpaleScript
	//When this is called, it will increase the variable that holds the points and then update the text to match.
	public void IncreasePoints(int pointsToAdd)
	{
		currentPoints = currentPoints + pointsToAdd;
		pointsText.text = "Points: " + currentPoints;
	}
	
}
