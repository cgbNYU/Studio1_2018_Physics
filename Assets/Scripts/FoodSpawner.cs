using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//a spawner with a timer for food that shoots towards
//the player who is positioned on the left
public class FoodSpawner : MonoBehaviour
{

	public GameObject food;
	public bool stopSpawning = false;
	public float spawnTime;
	public float spawnDelay;

	// Use this for initialization
	void Start () {
		InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	
	public void SpawnObject()
	{
		Instantiate(food, transform.position, transform.rotation);
	}
}
