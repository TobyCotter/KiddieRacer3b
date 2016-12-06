using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CenterFireTouchInput : MonoBehaviour {
	//Variables
	private Image fireButtonPanel;
	private Color currentColor;
	private Color clearColor;
	
	void Start () {
		fireButtonPanel = GetComponent<Image>();
		currentColor = fireButtonPanel.color;
		clearColor = currentColor;							
		clearColor.a = 0;									//clear color is the same as default color but with alpha set to 0
		fireButtonPanel.color = clearColor;					//clears the color at the start of the game
	}//End

	
	public void ShowFireButtonBriefly(){
		//Displays the fire button for a specified time
		currentColor.a = .196f;
		fireButtonPanel.color = currentColor;
		Invoke("HideFireButtons", 0.3f);						//Shows the fire button for this amount of time
	}


	public void HideFireButtons(){
		fireButtonPanel.color = clearColor;
	}
}
