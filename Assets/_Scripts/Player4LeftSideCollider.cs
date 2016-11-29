using UnityEngine;
using System.Collections;

public class Player4LeftSideCollider : MonoBehaviour {
	//Variables
	public bool LeftSideClear = true;


	void OnTriggerEnter (Collider collider){
		if(collider.CompareTag("SideCol")){
			LeftSideClear = false;
		}
	}//End


	void OnTriggerExit	(Collider collider){
		if(collider.CompareTag("SideCol")){
			LeftSideClear = true;
		}
	}//End
}
