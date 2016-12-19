using UnityEngine;
using System.Collections;

public class DirectionalLight : MonoBehaviour {
	//Variables
	private Light lightComponent;


	void Start () {
		lightComponent = GetComponent<Light>();
	}//End
	

	void Update () {
	
	}//End


	public void DimTheLights(){				//Originally set to intensity = 1.5
		lightComponent.intensity = .1f;
	}//End
}//End Class
