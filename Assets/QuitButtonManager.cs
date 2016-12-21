using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuitButtonManager : MonoBehaviour {
	//Variables
	public GameObject quitButton;
	private Image imageButton;
	private Color showMeColor;
	private Color clearColor;


	void Start () {
		imageButton = GetComponent<Image>();
		showMeColor = imageButton.color;
		clearColor = showMeColor;
		clearColor.a = 0;
		DisableQuitButton();
	}//End


	public void EnableQuitButton(){
		quitButton.gameObject.SetActive(true);
		imageButton.color = showMeColor;
	}//End


	public void DisableQuitButton(){
		Debug.Log("We disabled quit button");
		quitButton.gameObject.SetActive(false);
		imageButton.color = clearColor;
	}//End


	public void QuitGame(){
		Debug.Log("Quit button pressed");
		Application.Quit();
	}//End
}//End class
