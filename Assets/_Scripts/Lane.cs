using UnityEngine;
using System.Collections;

public class Lane : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
    	{
			GetComponent<Renderer>().material.color = new Color(0, 255, 0);
     	}
//     	if(Input.GetKeyDown(KeyCode.B))
//     	{
//        	gameObject.renderer.material.color = Color.blue;
//     	}
//     	if (Input.GetKeyDown (KeyCode.T))
//     	{
//        	gameObject.renderer.material.color = Color.white;
//     	}
	
	}
}
