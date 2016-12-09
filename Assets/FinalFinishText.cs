using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalFinishText : MonoBehaviour {
	//Variables
	private Text finishText;


	void Start () {
		finishText = GetComponent<Text>();
	}
	

	void Update () {
	
	}


	public void DisplayFinishText(int finishPos){
		switch(finishPos){
			case 1:
				finishText.text = "First Place!!";
				break;
			case 2:
				finishText.text = "Second Place!!";
				break;
			case 3:
				finishText.text = "Third Place!!";
				break;
			case 4:
				finishText.text = "Fourth Place!!";
				break;
			default:
				Debug.LogError("Error in determining your finish position");
				break;
		}//End switch
	}//End ()
}//End class
