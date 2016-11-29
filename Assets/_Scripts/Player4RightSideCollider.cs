using UnityEngine;
using System.Collections;

public class Player4RightSideCollider : MonoBehaviour {
	//Variables
	public bool RightSideClear = true;


	void OnTriggerEnter (Collider collider){
		if(collider.CompareTag("SideCol")){
			RightSideClear = false;
		}
	}//End


	void OnTriggerExit	(Collider collider){
		if(collider.CompareTag("SideCol")){
			RightSideClear = true;
		}
	}//End
}
