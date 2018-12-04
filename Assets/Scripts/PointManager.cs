using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
	
	public Text pointsText;
	public int currentPoints = 0;
	

// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		pointsText.text ="Point:"+currentPoints;
	}
	
	public void IncreasePoints(int pointsToAdd)
	{

		currentPoints = currentPoints + pointsToAdd;
	}
	
}
