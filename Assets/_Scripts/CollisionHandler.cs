using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {
	// Variables
	private int player4RacePos;
	public bool user;
	private PickupBoxManager pickupBoxManager;
	private RaceManager raceManager;
	private PickupBoxDisplayImage pickupBoxDisplayImage;
	public PickupBoxManager.pickupBoxKind pickupBoxType;

	
	void Start () {
		pickupBoxManager = GameObject.FindObjectOfType<PickupBoxManager>();
		raceManager = GameObject.FindObjectOfType<RaceManager>();
		pickupBoxDisplayImage = GameObject.FindObjectOfType<PickupBoxDisplayImage>();
	}
	

	void Update () {
	
	}


	void OnTriggerEnter(Collider collider) {  
		Debug.Log("We entered the collisionHandler's trigger");

        // Collides with pickupBox
        if(collider.CompareTag("PickupBox")){
        	HitPickupBox();
        	Debug.Log("We hit a pickup box");
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
