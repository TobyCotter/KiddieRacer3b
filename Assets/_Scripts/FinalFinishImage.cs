using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This script displays the user's final position when race is over

public class FinalFinishImage : MonoBehaviour {
	//Variables
	private Image imageComponent;
	private Color showColor;
	private Color clearColor;
	public Sprite firstPlace;
	public Sprite secondPlace;
	public Sprite thirdPlace;
	public Sprite fourthPlace;


	void Start () {
		imageComponent = GetComponent<Image>();
		showColor = imageComponent.color;
		clearColor = showColor;
		clearColor.a = 0;
		ShowImageAlpha(false);
	}//End


	private void ShowImageAlpha(bool showImage){
		if(!showImage){
			imageComponent.color = clearColor;
			Debug.Log("Show clear color: " + clearColor);
		}else{
			imageComponent.color = showColor;
			Debug.Log("Show show color: " + showColor);
		}
	}//End
	

	void Update () {
		
	}//End


	public void DisplayFinishImage(int finishPos){
		ShowImageAlpha(true);

		switch(finishPos){
				case 1:
					imageComponent.sprite = firstPlace;
					break;
				case 2:
					imageComponent.sprite = secondPlace;
					break;
				case 3:
					imageComponent.sprite = thirdPlace;
					break;
				case 4:
					imageComponent.sprite = fourthPlace;
					break;
				default:
					Debug.LogError("Error in determining your finish position");
					break;
			}//End switch
	}
}//End class
