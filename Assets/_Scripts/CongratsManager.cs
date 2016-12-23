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
		congratsImage.SetActive(true);
	}


	public void DisableCongratsImage(){
		congratsImage.SetActive(false);
	}//End

}//End Class
