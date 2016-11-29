// This script only determines which pickup box we receive (it is somewhat random)

using UnityEngine;
using System.Collections;

public class PickupBoxManager : MonoBehaviour {
	// Variables
	public enum pickupBoxKind {CONE, PROJECTILE, SPEED, GRINDER, EMPTY};


	void Start () {
	
	}
	

	void Update () {
	
	}


	public pickupBoxKind DecideWhichPickupBox (int racePos){
		//*** Takes in a player's race position, based on this it returns a pickup box
		int boxNum;

		boxNum = Random.Range(0,4);					// TODO later we will make this better so last place gets good stuff
		boxNum = 3;									// TODO remove - for testing only
		if(boxNum == 0){
			return pickupBoxKind.CONE;
		}else if(boxNum == 1){
			return pickupBoxKind.PROJECTILE;
		}else if(boxNum == 2){
			return pickupBoxKind.SPEED;
		}else if(boxNum == 3){
			return pickupBoxKind.GRINDER;
		}

		return pickupBoxKind.EMPTY;				// We should only make it here if the other returns didn't work
	}// End
}
