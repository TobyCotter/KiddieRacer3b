using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalFinishText : MonoBehaviour {
	//Variables
	private Text finishText;


	// Use this for initialization
	void Start () {
		finishText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void DisplayFinishText(int finishPos){
		switch(finishPos){
				case 1:
					Debug.Log("-You finished in 1st!");
					//imageComponent.sprite = firstPlace;
					finishText.text = "First Place!!";
					break;
				case 2:
					Debug.Log("-You finished in 2nd!");
					//imageComponent.sprite = secondPlace;
					finishText.text = "Second Place!!";
					break;
				case 3:
					Debug.Log("-You finished in 3rd!");
					//imageComponent.sprite = thirdPlace;
					finishText.text = "Third Place!!";
					break;
				case 4:
					Debug.Log("-You finished in 4th!");
					//imageComponent.sprite = fourthPlace;
					finishText.text = "Fourth Place!!";
					break;
				default:
					Debug.LogError("Error in determining your finish position");
					break;
			}//End switch
	}//End ()
}//End class
