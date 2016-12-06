using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeftTouchInput : MonoBehaviour {
	//Variables
	private Image leftTouchInputImage;
	private Color currentColor;
	private Color clearColor;
	
	void Start () {
		leftTouchInputImage = GetComponent<Image>();
		currentColor = leftTouchInputImage.color;
		clearColor = currentColor;							
		clearColor.a = 0;									//clear color is the same as default color but with alpha set to 0
		leftTouchInputImage.color = clearColor;					//clears the color at the start of the game
	}//End

	
	public void ShowLeftButtonBriefly(){
		//Displays the fire button for a specified time
		currentColor.a = .196f;
		leftTouchInputImage.color = currentColor;
		Invoke("HideLeftButton", 0.3f);						//Shows the fire button for this amount of time
	}


	private void HideLeftButton(){
		leftTouchInputImage.color = clearColor;
	}
}
