﻿using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {
	// Variables
	private int player4RacePos;
	private float totalTimeSinceCollision;
	private PickupBoxManager pickupBoxManager;
	private RaceManager raceManager;
	private PlayerEngineSound playerEngineSound;
	public int ourFinishPos;
	public PickupBoxManager.pickupBoxKind pickupBoxType;
	public bool user;
	public bool playerCrossedFinishLine = false;
	public int iAmThisPlayer = 0;  

	
	void Start () {
		pickupBoxManager = GameObject.FindObjectOfType<PickupBoxManager>();
		raceManager = GameObject.FindObjectOfType<RaceManager>();
		playerEngineSound = GameObject.FindObjectOfType<PlayerEngineSound>();	//Used to stop engine sound at race finish

		//The following is used when we cross the finish line, we can report what player we are
		if(this.CompareTag("Player1")){
			iAmThisPlayer = 1;
		}else if(this.CompareTag("Player2")){
			iAmThisPlayer = 2;
		}else if(this.CompareTag("Player3")){
			iAmThisPlayer = 3;
		}else if (this.CompareTag("Player4")) {
			iAmThisPlayer = 4;
		}
	}//End
	

	void Update () {
		if(!raceManager.raceHasBegun){
			totalTimeSinceCollision = 0;
		}else{
			totalTimeSinceCollision = totalTimeSinceCollision + Time.deltaTime;
		}
	}//End


	public float GetTotalTimeSinceCollision(){
		return totalTimeSinceCollision;
	}//End


	void OnTriggerEnter(Collider collider) {  
		//TODO delete the following--we are no longer using lane colliders
		/*
		//Collides with LaneCollider
		if(collider.CompareTag("Lane1")){
			thisCarslanePos = 1;
		}else if(collider.CompareTag("Lane2")){
			thisCarslanePos = 2;
		}else if(collider.CompareTag("Lane3")){
			thisCarslanePos = 3;
		}else if(collider.CompareTag("Lane4")){
			thisCarslanePos = 4;
		}*/

        // Collides with pickupBox
        if(collider.CompareTag("PickupBox")){
        	HitPickupBox();
        }

       	// Collides with cone
       	if(collider.CompareTag("Debris")){
       		totalTimeSinceCollision = 0;
       		if(user){
       			BroadcastMessage("PlayHitConeSound");
       		}
       	}

       	//Bullet hits us
       	if(collider.CompareTag("Bullet")){
       		totalTimeSinceCollision = 0;
       		if(user){
       			BroadcastMessage("PlayBulletHitSound");
       		}
       		Destroy(collider.gameObject);									//Destroy bullet
       	}

       	//Hit FinishLine
       	if(collider.CompareTag("FinishLine")){
       		ourFinishPos = FinishLine.FINISH_POSITION;
       		FinishLine.FINISH_POSITION++;
       		raceManager.finalRaceOrder[ourFinishPos-1] = iAmThisPlayer;		//Reports this player's position to the raceManager
       		playerCrossedFinishLine = true;									//HACK when we play again we will need to reset this OR reload scen
       		if(user){														//The engine sound is only playing on the Player (user) so only need to stop the engine sound on the user
				playerEngineSound.PlayEngineGoSound (false);
       		}
       	}
    }// End OnTriggerEnter


    private void HitPickupBox(){
    	int fakeRacePosVal;

    	//Play pickupBox sound, determine what type of pickupbox we get, set image on canvas
    	if(user){																			//CollisionHandler is used on both the user and dummy cars, we don't want to change the image and other things on the dummy cars
			BroadcastMessage("PlayPickupBoxSound");
			player4RacePos = raceManager.FindPlayerFourRacePosition();						// Our raceposition is used to determine what box we get
			pickupBoxType = pickupBoxManager.DecideWhichPickupBox(player4RacePos);			// pickupBoxType is a static that holds the value of our current pickupbox
			pickupBoxManager.SetPickupBoxImage((int)pickupBoxType);	
		}else{
			//Used for dummy racers
			player4RacePos = raceManager.FindPlayerFourRacePosition();						// Our raceposition is used to determine what box we get
			if(player4RacePos == 1){														// If player4 is in first, we don't want to give the dummies cones constantly.  We will use a strategy for #3 which is slightly more speed and projectiles
				fakeRacePosVal = 3;
			}else{
				fakeRacePosVal = 2;
			}
			pickupBoxType = pickupBoxManager.DecideWhichPickupBox(fakeRacePosVal);			// pickupBoxType is a static that holds the value of our current pickupbox
		}									
    }// End
}//End class
