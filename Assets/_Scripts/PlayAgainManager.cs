using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayAgainManager : MonoBehaviour {
	//Variables
	private PlayAgainButton playAgainButton;


	void Start () {
		playAgainButton = GameObject.FindObjectOfType<PlayAgainButton>();
		DisablePlayAgainButton();					//Disable the play again button at race start
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void EnablePlayAgainButton(){
		playAgainButton.gameObject.SetActive(true);
		Debug.Log("We enabled the play again button");
	}//End


	public void DisablePlayAgainButton(){
		Debug.Log("We disabled the play again button");
		playAgainButton.gameObject.SetActive(false);
	}//End


	public void PlayGameAgain(){
		Debug.Log("Play game again");
		FinishLine.FINISH_POSITION = 1;					//This is a static so when reloading the scene it doesn't get reset
		SceneManager.LoadScene(0);						//Loads the original scene
	}//End
}//End class
