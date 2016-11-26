using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour {
	// Variables
	public Transform lookAt;
	public float lerpSpeed;
	public float xOffset;
	public float yOffset;
	public float zOffset;
	public bool lerpCamera = true;


	void Start () {
		
	}
	

	void LateUpdate () {
		// Moves the camera to follow the player
		Vector3 offset = new Vector3(xOffset, yOffset, zOffset);
		Vector3 desiredPos = lookAt.transform.position + offset;

		if(lerpCamera){	// true
			transform.position = Vector3.Lerp(transform.position, desiredPos, (lerpSpeed * Time.deltaTime));
		}else{			// false
			transform.position = desiredPos;
		}
	}// End LateUpdate*/
}
