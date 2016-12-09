using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {
	// Variables
	private int iAmThisPlayer = 0;
	private int player4RacePos;
	private float totalTimeSinceCollision;
	private PickupBoxManager pickupBoxManager;
	private RaceManager raceManager;
	private PickupBoxDisplayImage pickupBoxDisplayImage;
	public int ourFinishPos;
	public PickupBoxManager.pickupBoxKind pickupBoxType;
	public bool user;
	public bool playerCrossedFinishLine = false;

	
	void Start () {
		pickupBoxManager = GameObject.FindObjectOfType<PickupBoxManager>();
		raceManager = GameObject.FindObjectOfType<RaceManager>();
		pickupBoxDisplayImage = GameObject.FindObjectOfType<PickupBoxDisplayImage>();

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
