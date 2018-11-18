//This script generates new balls and throws them at the player at random intervals.

//I want to have 2 types of objects that come at the player
// a rock or a piece of food
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

public class FoodGenerator : MonoBehaviour {
	private float timeUntilNextItem;
	private bool shouldThrow;
	public GameObject Rock; //I have a game object that is a rock
	public GameObject Radish; //this is a game object that is a food

	
	//these tuning values are set in the inspector
	[Header("Delay before firing first ball")]
	[Range(0.0f, 10.0f)]
	
	public float START_DELAY; //wait before firing first ball
	[Header("Delay between balls getting fired")]
	[Range(1.0f, 10.0f)]
	public float MIN_DELAY; //minimum time between firing balls
	[Range(1.0f, 10.0f)]
	public float MAX_DELAY;	//maximum time between firing balls
	[Header("Random variation in ball starting height")]
	[Range(1.0f, 5.0f)]
	public float HEIGHT_MIN; //maximum spatial offset for random ball position
	[Range(1.0f, 5.0f)]
	public float HEIGHT_MAX; //maximum spatial offset for random ball position
	[Header("Minimum and maximum landing position of balls")]
	[Range(6f, 13.0f)]
	public float MIN_RANGE; //minimum starting force for balls
	[Range(6f, 13.0f)]
	public float MAX_RANGE; //maximum starting force for balls
	[Header("Minimum and maximum starting force for balls")]
	[Range(5.0f, 20.0f)]
	public float MIN_STARTSPEED; //minimum starting force for balls
	[Range(5.0f, 20.0f)]
	public float MAX_STARTSPEED; //maximum starting force for balls
	[Header("Maximum spin speed for balls")]
	[Range(0.0f, 200.0f)]
	public float MAX_SPIN; //maximum spin [torque] force for balls

	public float MAX_ANGLE;
	public float MIN_ANGLE;
	


	// Use this for initialization
	void Start () {
		shouldThrow = true;
		timeUntilNextItem = START_DELAY; //give the player time to get ready
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldThrow==true){ //don't do anything if we've been told not to throw
			timeUntilNextItem -= Time.deltaTime;

			if (timeUntilNextItem <= 0){ //time is up
				//throw a new ball
				//first create a random position offset so the ball doesn't always appear at same height
				float rand_y = Random.Range(HEIGHT_MIN, HEIGHT_MAX);
				Vector3 position = new Vector3(transform.position.x, rand_y, transform.position.z);
				//GameObject newRock = Instantiate(Rock, position , Quaternion.identity) as GameObject;
				GameObject newObject;
				if (Random.value < 0.5f)
					newObject= Instantiate(Rock, position, Quaternion.identity);
				else
					newObject= Instantiate(Radish, position, Quaternion.identity);
				float angle = Random.Range(MIN_ANGLE, MAX_ANGLE);
				float speed = Random.Range(MIN_STARTSPEED, MAX_STARTSPEED);

				newObject.GetComponent<Rigidbody2D>().velocity= new Vector2(-Mathf.Cos(angle*Mathf.Deg2Rad)*speed, Mathf.Sin(angle*Mathf.Deg2Rad)*speed);
				

				//set the timer to a random amount
				timeUntilNextItem = Random.Range(MIN_DELAY,MAX_DELAY);
			}
		}
	}


	public void StopThrowing() {
		shouldThrow = false;
	}
}