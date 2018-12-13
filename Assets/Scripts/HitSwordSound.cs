using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSwordSound : MonoBehaviour
{
	public AudioManager audioManager;

	// Use this for initialization
	void Start ()
	{
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Dagger"))
		{
			audioManager.PlaySoundEffect(audioManager.Clips.daggerHitSword);
		}
		else if (other.gameObject.CompareTag("Rock"))
		{
			audioManager.PlaySoundEffect(audioManager.Clips.rockHitSword);
		}
	}
}
