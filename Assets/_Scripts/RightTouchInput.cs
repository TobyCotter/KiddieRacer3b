using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RightTouchInput : MonoBehaviour {
	//Variables
	private Image rightTouchPanelInputImage;
	private Color currentColor;
	private Color clearColor;
	
	void Start () {
		rightTouchPanelInputImage = GetComponent<Image>();
		currentColor = rightTouchPanelInputImage.color;
		clearColor = currentColor;							
		clearColor.a = 0;										//clear color is the same as default color but with alpha set to 0
		rightTouchPanelInputImage.color = clearColor;			//clears the color at the start of the game
	}//End

	
	public void ShowRightButtonBriefly(){
		//Displays the fire button for a specified time
		currentColor.a = .196f;
		rightTouchPanelInputImage.color = currentColor;
		Invoke("HideRightButton", 0.3f);						//Shows the fire button for this amount of time
	}//End


	private void HideRightButton(){
		rightTouchPanelInputImage.color = clearColor;
	}//End
}//End Class
