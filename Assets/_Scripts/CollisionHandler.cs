using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {
	// Variables
	private PickupBoxManager pickupBoxManager;
	private RaceManager raceManager;
	private PickupBoxDisplayImage thisPickupBox;
	private int ourRacePosition;
	public static PickupBoxManager.pickupBoxKind PICKUPBOX_TYPE;

	
	void Start () {
		pickupBoxManager = GameObject.FindObjectOfType<PickupBoxManager>();
		raceManager = GameObject.FindObjectOfType<RaceManager>();
		thisPickupBox = GameObject.FindObjectOfType<PickupBoxDisplayImage>();
	}
	

	void Update () {
	
	}


	void OnTriggerEnter(Collider collider) {   
		// Collides with other player's car
		if(collider.GetComponent<Dummy>()){    									//If we collided with Dummy player, reset the dummy collision timer       
        	collider.GetComponent<Dummy>().ResetCollisionTimer();
        }

        // Collides with pickupBox
        if(collider.CompareTag("PickupBox")){
        	HitPickupBox();
        }
    }// End OnTriggerEnter


    private void HitPickupBox(){
    	//Play pickupBox sound, determine what type of pickupbox we get, set image on canvas
		BroadcastMessage("PlayPickupBoxSound");
		ourRacePosition = raceManager.FindPlayerFourRacePosition();						// Our raceposition is used to determine what box we get
		PICKUPBOX_TYPE = pickupBoxManager.DecideWhichPickupBox(ourRacePosition);		// pickupBoxType is a static that holds the value of our current pickupbox

		if(PICKUPBOX_TYPE == PickupBoxManager.pickupBoxKind.CONE){
			thisPickupBox.SetPickupBoxImage();
		}else if(PICKUPBOX_TYPE == PickupBoxManager.pickupBoxKind.PROJECTILE){
			thisPickupBox.SetPickupBoxImage();
		}else if(PICKUPBOX_TYPE == PickupBoxManager.pickupBoxKind.SPEED){
			thisPickupBox.SetPickupBoxImage();
		}else if(PICKUPBOX_TYPE == PickupBoxManager.pickupBoxKind.EMPTY){
			thisPickupBox.SetPickupBoxImage();
		}
    }// End
}
