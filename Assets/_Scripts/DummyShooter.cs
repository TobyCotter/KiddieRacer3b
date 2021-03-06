using UnityEngine;
using System.Collections;

public class DummyShooter : MonoBehaviour {
	// Variables
	private CollisionHandler collisionHandler;
	private int numConeDropped = 0;
	private float speedBurstTime = 2.0f;
	private GameObject bulletParent;
	private GameObject coneParent;
	//private PickupBoxManager pickupBoxManager;
	public Rigidbody bulletPrefab;
	public Rigidbody conePrefab;
	public Transform gun;
	public Transform rearGun;
	public int bulletSpeed;
	public float coneDropSpeed;
	public float speedBurst = 1.0f;
	public int numConesToDrop;

	
	void Start () {
		collisionHandler = GetComponent<CollisionHandler>();
		//pickupBoxManager = GameObject.FindObjectOfType<PickupBoxManager>();
		BroadcastMessage("DeactivateAfterburner");
		speedBurst = 1.0f;

		// Creates Bullets parent object if it doesn't exist
		bulletParent = GameObject.Find("Bullets");
		if(!bulletParent){
			bulletParent = new GameObject("Bullets");
		}

		coneParent = GameObject.Find("Cones");
		if(!coneParent){
			coneParent = new GameObject("Cones");
		}
	}//End Start
	

	void Update (){
		
	}// End


	private void CheckIfUserWantsToFireWeapon(){
		//if(Input.GetKeyDown(KeyCode.Space)){
			// Let's fire our pickupBox weapon, used only in windows based app
		//	FireWeapon();
		//}
	}//End


	public void FireWeapon(){
		//Take action
		if(collisionHandler.pickupBoxType == PickupBoxManager.pickupBoxKind.PROJECTILE){
				// Launch Projectile
				InstantiateBullet();											//And sends it flying												
				ResetPickupBox();												//Removes projectile from display
			}else if(collisionHandler.pickupBoxType == PickupBoxManager.pickupBoxKind.CONE){
				// Drop Cones
				numConeDropped = 0;												//Reset the number of cones that have been dropped
				InvokeRepeating("DropCone", .000001f, coneDropSpeed);			//Drops a cone immediately and then waits one second
				ResetPickupBox();												//Removes cone from display
			}else if(collisionHandler.pickupBoxType == PickupBoxManager.pickupBoxKind.SPEED){
				// Speed burst
				ActivateSpeedBurst();
				ResetPickupBox();												//Removes speed icon from display
			}
	}//End


	private void ActivateSpeedBurst(){
		speedBurst = 1.6f;		
		BroadcastMessage("ActivateAfterburner");						//Located in a script attached to the rearGun.  We do this because there are multiple afterburners in this project.  Otherwise we would need to loop thru each one until we find our afterburner.							
		Invoke("DisableSpeedBurst", speedBurstTime);					//Will reset speed burst back to normal speed
	}


	private void DisableSpeedBurst(){
		// Reset speed burst multiplier to 1.0f after 2 seconds
		speedBurst = 1.0f;
		BroadcastMessage("DeactivateAfterburner");
	}// End


	private void DropCone(){
		Rigidbody coneInstance;
		coneInstance = Instantiate(conePrefab, rearGun.position, rearGun.rotation) as Rigidbody;	//no velocity needed because cone is stationary
		coneInstance.transform.parent = coneParent.transform;
		numConeDropped++;
		if(numConeDropped >= numConesToDrop){																		//We drop this many cones
			CancelInvoke();
		}
	}// End


	private void InstantiateBullet(){
		Rigidbody bulletInstance;
		bulletInstance = Instantiate(bulletPrefab, gun.position, gun.rotation) as Rigidbody;		// gun is a transform
		bulletInstance.velocity = new Vector3(0,0,bulletSpeed);
		bulletInstance.transform.parent = bulletParent.transform;
	}// End


//	private void PlayShootSound(){
//		BroadcastMessage("PlayShootingSound");
//	}// End


	private void ResetPickupBox(){
		//Clear the variable and set the pickupBox image to transparent
		collisionHandler.pickupBoxType = PickupBoxManager.pickupBoxKind.EMPTY;			//This sets the variable to empty
	}// End
}
