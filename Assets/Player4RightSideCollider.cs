using UnityEngine;
using System.Collections;

public class Player4RightSideCollider : MonoBehaviour {
	//Variables
	public bool RightSideClear = true;


	void OnTriggerEnter (Collider collider){
		if(collider.CompareTag("SideCol")){
			Debug.Log("Right side has collided");
			RightSideClear = false;
		}
	}//End


	void OnTriggerExit	(Collider collider){
		if(collider.CompareTag("SideCol")){
			Debug.Log("We are no longer hitting neighbor on right");
			RightSideClear = true;
		}
	}//End
}
