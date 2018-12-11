using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{

	public PlayerHealth playerHealth;
	public AudioManager audioManager;

	// Use this for initialization
	void Start ()
	{
		playerHealth = GameObject.Find("FirstPlayerController").GetComponent<PlayerHealth>(); //grab the player health script
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}

	//If the object it collides with is the player, call the GetHit script in PlayerHealth
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			
			playerHealth.GetHit();
		}
	}
}
