using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayAgainManager : MonoBehaviour {
	//Variables
	//private PlayAgainButton playAgainButton;
	public GameObject playAgainButton;
	private Image imageButton;
	private Color showMeColor;
	private Color clearColor;


	void Awake () {
		//playAgainButton = GameObject.FindObjectOfType<PlayAgainButton>();
		imageButton = GetComponent<Image>();
		showMeColor = imageButton.color;
		clearColor = showMeColor;
		clearColor.a = 0;							//Sets clearColor to transparent
		DisablePlayAgainButton();					//Disable the play again button at race start
	}


	public void EnablePlayAgainButton(){
		playAgainButton.gameObject.SetActive(true);
		imageButton.color = showMeColor;
		Debug.Log("We enabled the play again button");
	}//End


	public void DisablePlayAgainButton(){
		
		playAgainButton.gameObject.SetActive(false);
		imageButton.color = clearColor;
	}//End


	public void PlayGameAgain(){
		Debug.Log("Play game again");
		FinishLine.FINISH_POSITION = 1;					//This is a static so when reloading the scene it doesn't get reset
		SceneManager.LoadScene(1);						//Loads the original scene
	}//End
}//End class
