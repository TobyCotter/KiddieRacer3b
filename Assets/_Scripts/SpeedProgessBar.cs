using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedProgessBar : MonoBehaviour {
	//Variables
	private Slider slider;


	void Start () {
		slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void AdjustSpeedBar (int progBarVal){
		slider.value = progBarVal;
	}//End
}//End Class
