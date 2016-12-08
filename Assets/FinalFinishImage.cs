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
		}else{
			imageComponent.color = showColor;
		}
	}//End
	

	void Update () {
		
	}//End


	public void DisplayFinishImage(int finishPos){
		ShowImageAlpha(true);

		switch(finishPos){
				case 1:
					Debug.Log("You finished in 1st!");
					imageComponent.sprite = firstPlace;
					break;
				case 2:
					Debug.Log("You finished in 2nd!");
					imageComponent.sprite = secondPlace;
					break;
				case 3:
					Debug.Log("You finished in 3rd!");
					imageComponent.sprite = thirdPlace;
					break;
				case 4:
					Debug.Log("You finished in 4th!");
					imageComponent.sprite = fourthPlace;
					break;
				default:
					Debug.LogError("Error in determining your finish position");
					break;
			}//End switch
	}
}//End class
