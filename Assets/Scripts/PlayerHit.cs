using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{

	public PlayerHealth playerHealth;
	public AudioManager audioManager;

	private bool hasHit = false;

	// Use this for initialization
	void Start ()
	{
		playerHealth = GameObject.Find("FirstPlayerController").GetComponent<PlayerHealth>(); //grab the player health script
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}

	//If the object it collides with is the player, call the GetHit script in PlayerHealth
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player") && !hasHit)
		{
			hasHit = true;
			if (gameObject.name == "Rock(Clone)")
			{
				audioManager.PlaySoundEffect(audioManager.Clips.rockThrow);
			}
			else
			{
				audioManager.PlaySoundEffect(audioManager.Clips.knifeThrow);
			}
			playerHealth.GetHit();
		}
	}
}
