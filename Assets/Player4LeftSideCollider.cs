using UnityEngine;
using System.Collections;

public class Player4LeftSideCollider : MonoBehaviour {
	//Variables
	public bool LeftSideClear = true;


	void OnTriggerEnter (Collider collider){
		if(collider.CompareTag("SideCol")){
			Debug.Log("Left side has collided");
			LeftSideClear = false;
		}
	}//End


	void OnTriggerExit	(Collider collider){
		if(collider.CompareTag("SideCol")){
			Debug.Log("We are no longer hitting neighbor on left");
			LeftSideClear = true;
		}
	}//End
}
