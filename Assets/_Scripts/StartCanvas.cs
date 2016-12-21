using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartCanvas : MonoBehaviour {
	//Variables



	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	//Load main scene
	public void PlayButtonPressed(){
		Debug.Log("Play button pressed");
		SceneManager.LoadScene(1);
	}//End


	//Quit application
	public void QuitButtonPressed(){
		Application.Quit();
		Debug.Log("Quit button pressed");
	}//End


	//Load 1st Tutorial Scene
	public void LearnButtonPressed(){
		Debug.Log("Learn button pressed");
		SceneManager.LoadScene(2);
	}//End 


	//Load Credits scene
	public void CreditsButtonPressed(){
		Debug.Log("Credits button pressed");
	}//End
}//End Class
