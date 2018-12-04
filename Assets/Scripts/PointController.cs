using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PointController : MonoBehaviour
{
	public int myPointValue;
	public PointManager pointManager;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		pointManager.IncreasePoints(myPointValue);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
