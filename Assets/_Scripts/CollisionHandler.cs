﻿using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {
	// Variables
	private int player4RacePos;
	private float totalTimeSinceCollision;
	private PickupBoxManager pickupBoxManager;
	private RaceManager raceManager;
	private PickupBoxDisplayImage pickupBoxDisplayImage;
	public PickupBoxManager.pickupBoxKind pickupBoxType;
	public bool user;

	
	void Start () {
		pickupBoxManager = GameObject.FindObjectOfType<PickupBoxManager>();
		raceManager = GameObject.FindObjectOfType<RaceManager>();
		pickupBoxDisplayImage = GameObject.FindObjectOfType<PickupBoxDisplayImage>();
	}//End
	

	void Update () {
		totalTimeSinceCollision = totalTimeSinceCollision + Time.deltaTime;
	}//End


	public float GetTotalTimeSinceCollision(){
		return totalTimeSinceCollision;
	}//End


	void OnTriggerEnter(Collider collider) {  
        // Collides with pickupBox
        if(collider.CompareTag("PickupBox")){
        	HitPickupBox();
        }

       	// Collides with cone
       	if(collider.CompareTag("Debris")){
       		totalTimeSinceCollision = 0;
       		BroadcastMessage("PlayHitConeSound");
       	}

       	//Bullet hits us
       	if(collider.CompareTag("Bullet")){
       		totalTimeSinceCollision = 0;
       		BroadcastMessage("PlayBulletHitSound");
       		Destroy(collider.gameObject);							//Destroy bullet
       	}

    }// End OnTriggerEnter


    private void HitPickupBox(){
    	int fakeRacePosVal;
    	//Play pickupBox sound, determine what type of pickupbox we get, set image on canvas
    	if(user){																			//CollisionHandler is used on both the user and dummy cars, we don't want to change the image and other things on the dummy cars
			BroadcastMessage("PlayPickupBoxSound");
			player4RacePos = raceManager.FindPlayerFourRacePosition();						// Our raceposition is used to determine what box we get
			pickupBoxType = pickupBoxManager.DecideWhichPickupBox(player4RacePos);			// pickupBoxType is a static that holds the value of our current pickupbox
			pickupBoxDisplayImage.SetPickupBoxImage((int)pickupBoxType);	
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
}
