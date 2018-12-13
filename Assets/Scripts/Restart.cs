
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class Restart : MonoBehaviour
{
	
	void Update ()
	{
		//When the player hits the R key on the keyboard, load the current scene through the scenemanager
		//This should work
		if( Input.GetKeyDown(KeyCode.R) )
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			//PlayerPrefs.SetFloat();
		}
		
		//Quit if the player hits the esc key
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
