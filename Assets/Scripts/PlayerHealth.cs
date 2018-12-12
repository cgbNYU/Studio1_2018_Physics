using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

	public int hitPoints;

	public GameObject frontMouse;
	public GameObject backMouse;

	public HingeJoint2D backCalfJoint;
	public SliderJoint2D frontCalfJoint;
	public AudioManager audioManager;
	public FaceAnimationManager faceManager;

	private bool isGameOver = false;

	// Use this for initialization
	void Start ()
	{
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		faceManager = GameObject.Find("FirstPlayerController").GetComponent<FaceAnimationManager>();
	}

	public void GetHit()
	{
		//reduce health
		hitPoints--;
		faceManager.ChangeFace(faceManager.sadFaceSprites[UnityEngine.Random.Range(0, faceManager.sadFaceSprites.Length)]);

		if (hitPoints <= 0 && !isGameOver)
		{
			isGameOver = true;

			GameOver();
		}
	}

	public void GameOver()
	{
		//player loses control
		audioManager.PlaySoundEffect(audioManager.Clips.deathsounds[UnityEngine.Random.Range(0, audioManager.Clips.impaledSounds.Length)]);
		//Destroy the mouse control objects so the player loses control
		Destroy(frontMouse);
		Destroy(backMouse);
		//Destroy the joints that fix the player to the floor so it can fall down
		Destroy(backCalfJoint);
		Destroy(frontCalfJoint);
	}
}
