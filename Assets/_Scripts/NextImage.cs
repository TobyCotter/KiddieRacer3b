using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextImage : MonoBehaviour {
	//Variables
	public int levelToLoadNext;


	void Start () {
	
	}//End
	
	// Update is called once per frame
	void Update () {
	
	}//End


	public void LoadThisLevelMy(){
		Debug.Log("We clicked load this level");
		SceneManager.LoadScene(levelToLoadNext);
	}//End
}//End class
