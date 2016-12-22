using UnityEngine;
using System.Collections;

public class CongratsManager : MonoBehaviour {
	//Variables
	public GameObject congratsImage;


	void Start () {
		DisableCongratsImage();
	}//End
	

	void Update () {
	
	}//End


	public void EnableCongratsImage(){
		Debug.Log("Enable the congrats image");
		congratsImage.SetActive(true);
	}


	public void DisableCongratsImage(){
		Debug.Log("Disable the congrats image");
		congratsImage.SetActive(false);
	}//End

}//End Class
