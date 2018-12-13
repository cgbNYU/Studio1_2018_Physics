using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookScript : MonoBehaviour
{

	public PointManager pointManager;

	//Timer stuff
	public float timer;
	public float cookTime;

	private bool isCooking;
	private bool cooling;
	private GameObject cookingSound;
	private AudioManager audioManager;
	
	//Face manager
	private FaceAnimationManager faceManager;
	
	// Use this for initialization
	void Start ()
	{
		timer = 0;
		pointManager = GameObject.Find("PointManager").GetComponent<PointManager>();
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		faceManager = GameObject.Find("FirstPlayerController").GetComponent<FaceAnimationManager>();
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
		
		//Debug.Log("Cook time = " + timer);
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Hilt") && pointManager.impaledFoodStack.Count != 0)
		{
			BeginCookTween();
			isCooking = true;
			cooling = false;
			cookingSound = audioManager.PlaySoundEffect(audioManager.Clips.grillSizzle, 1.0f);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Hilt") && isCooking)
		{
			BeginCoolTween();
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
			isCooking = false;
			pointManager.ComboPayout();
			faceManager.ChangeFace(faceManager.happyFaceSprites[UnityEngine.Random.Range(0, faceManager.happyFaceSprites.Length)], true);
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

	//When the cooking starts, iterate through the impaledArray in pointmanager to activate the cooktween script in the food
	public void BeginCookTween()
	{
		for (int i = 0; i < pointManager.impaledFoodArray.Length; i++)
		{
			pointManager.impaledFoodArray[i].GetComponent<ImpaleScript>().CookTween();
		}
	}
	
	//When cooling starts, do what they did with the cook tween
	public void BeginCoolTween()
	{
		for (int i = 0; i < pointManager.impaledFoodArray.Length; i++)
		{
			pointManager.impaledFoodArray[i].GetComponent<ImpaleScript>().CoolTween();
		}
	}
}
