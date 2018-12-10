using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookScript : MonoBehaviour
{

	public PointManager pointManager;

	//Timer stuff
	private float timer;
	public float cookTime;

	private bool isCooking;
	private bool cooling;
	private GameObject cookingSound;
	private AudioManager audioManager;
	
	// Use this for initialization
	void Start ()
	{
		timer = 0;
		pointManager = GameObject.Find("PointManager").GetComponent<PointManager>();
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isCooking)
		{
			CookImpaledFood();
		}
		else if (cooling)
		{
			CookTimeDown();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Hilt") && pointManager.impaledFoodStack != null)
		{
			isCooking = true;
			cooling = false;
			cookingSound = audioManager.PlaySoundEffect(audioManager.Clips.grillSizzle, 1.0f, true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Hilt") && isCooking)
		{
			isCooking = false;

			if (cookingSound != null)
			{
				Destroy(cookingSound);
			}
			
			cooling = true;
		}
	}

	public void CookImpaledFood()
	{
		timer += Time.deltaTime;

		if (timer >= cookTime)
		{
			pointManager.ComboPayout();
			timer = 0;
		}
	}

	public void CookTimeDown()
	{
		timer -= Time.deltaTime / 2f;
		if (timer <= 0)
		{
			timer = 0;
			cooling = false;
		}
	}
}
