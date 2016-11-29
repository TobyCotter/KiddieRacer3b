// This script only determines which pickup box we receive (it is somewhat random)

using UnityEngine;
using System.Collections;

public class PickupBoxManager : MonoBehaviour {
	// Variables
	public enum pickupBoxKind {CONE, PROJECTILE, SPEED, EMPTY};


	void Start () {
	
	}
	

	void Update () {
	
	}


	public pickupBoxKind DecideWhichPickupBox (int racePos){
		//*** Takes in a player's race position, based on this it returns a pickup box
		int chanceOfProj = 0;
		int chanceOfCone = 0;
		int chance100;

		chance100 = Random.Range(0,100);

		if(racePos == 1){							//This means the player is in first place and doesn't want projectiles
		 	chanceOfProj = 90;						//50% chance of cone, 10% chance of projectile, and 40% chance of speed
		 	chanceOfCone = 50; 
		}else if(racePos == 2){						//Player is in second place and wants a few more projectiles
			chanceOfProj = 60;
			chanceOfCone = 20;
		}else if(racePos == 3){
			chanceOfProj = 60;
			chanceOfCone = 12;
		}else if(racePos == 4){
			chanceOfProj = 60;
			chanceOfCone = 5;
		}else{
			Debug.LogError("We should not have made it here");
		}

		if(chance100 >= chanceOfProj){
			return pickupBoxKind.PROJECTILE;
		}else if(chance100 <= chanceOfCone){
			return pickupBoxKind.CONE;
		}else{
			return pickupBoxKind.SPEED;
		}
	}// End
}
