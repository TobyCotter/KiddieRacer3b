using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	
	void Start () {
	
	}
	

	void Update () {
	
	}

	void OnTriggerEnter (Collider collider){
		Destroy(collider.gameObject);
	}// End OnTriggerEnter ()
}
