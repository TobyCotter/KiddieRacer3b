using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	// Variables
	private int numConeDropped = 0;
	private float speedBurstTime = 2.0f;
	private GameObject bulletParent;
	private GameObject coneParent;
	private GameObject afterBurnerParticle;
	private PickupBoxDisplayImage thisPickUpBox;
	public Rigidbody bulletPrefab;
	public Rigidbody conePrefab;
	public Transform gun;
	public Transform rearGun;
	public int bulletSpeed;
	public float coneDropSpeed;
	public float speedBurst;
	public int numConesToDrop;

	
	void Start () {
		thisPickUpBox = GameObject.FindObjectOfType<PickupBoxDisplayImage> ();
		afterBurnerParticle = GameObject.FindGameObjectWithTag("AfterBurner");
		afterBurnerParticle.SetActive(false);
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
		if(Input.GetKeyDown(KeyCode.Space)){
			// Let's fire our pickupBox weapon
			if(CollisionHandler.PICKUPBOX_TYPE == PickupBoxManager.pickupBoxKind.PROJECTILE){
				// Launch Projectile
				InstantiateBullet();											//And sends it flying
				PlayShootSound();												
				ResetPickupBox();												//Removes projectile from display
			}else if(CollisionHandler.PICKUPBOX_TYPE == PickupBoxManager.pickupBoxKind.CONE){
				// Drop Cones
				numConeDropped = 0;												//Reset the number of cones that have been dropped
				InvokeRepeating("DropCone", .000001f, coneDropSpeed);			//Drops a cone immediately and then waits one second
				ResetPickupBox();												//Removes cone from display
			}else if(CollisionHandler.PICKUPBOX_TYPE == PickupBoxManager.pickupBoxKind.SPEED){
				// Speed burst
				ActivateSpeedBurst();
				ResetPickupBox();												//Removes speed icon from display
			}
		}
	}// End


	private void ActivateSpeedBurst(){
		speedBurst = 1.6f;		
		afterBurnerParticle.SetActive(true);							//Enables afterburner particle glow									
		Invoke("DisableSpeedBurst", speedBurstTime);					//Will reset speed burst back to normal speed
		//Play rocket speed sound
		BroadcastMessage("PlayRocketSound");							//Located on RocketSound.cs
	}


	private void DisableSpeedBurst(){
		// Reset speed burst multiplier to 1.0f after 2 seconds
		speedBurst = 1.0f;
		afterBurnerParticle.SetActive(false);
	}// End


	private void DropCone(){
		Rigidbody coneInstance;
		coneInstance = Instantiate(conePrefab, rearGun.position, rearGun.rotation) as Rigidbody;	//no velocity needed because cone is stationary
		coneInstance.transform.parent = coneParent.transform;
		numConeDropped++;
		if(numConeDropped >= numConesToDrop){																		//We drop this many cones
			CancelInvoke();
		}
		//Play cone drop sound
		BroadcastMessage("PlayConeSound");
	}// End


	private void InstantiateBullet(){
		Rigidbody bulletInstance;
		bulletInstance = Instantiate(bulletPrefab, gun.position, gun.rotation) as Rigidbody;		// gun is a transform
		bulletInstance.velocity = new Vector3(0,0,bulletSpeed);
		bulletInstance.transform.parent = bulletParent.transform;

	}// End


	private void PlayShootSound(){
		BroadcastMessage("PlayShootingSound");
	}// End


	private void ResetPickupBox(){
		CollisionHandler.PICKUPBOX_TYPE = PickupBoxManager.pickupBoxKind.EMPTY;	// This empties the variable
		thisPickUpBox.SetPickupBoxImage();										// This clears the image from our canvas
	}// End
}
