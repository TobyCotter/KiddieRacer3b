using UnityEngine;
using System.Collections;

public class DummyBrain : MonoBehaviour {
	// Variables
	private float totalTimeSinceCollision;
	private int iAmThisPlayer;
	private float desiredXPos;
	private float zSpeed;
	private float[] lane_XValue = {96.65f, 98.9f, 101.15f, 103.4f};
	private bool leftArrow = false;
	private bool rightArrow = false;
	private int myLanePos;
	private float finishLineLerpSpeed = 1.0f;
	private float sourceSpeedFactor = 1.0f;					//Used for finish line speed
	[Tooltip("Speed player moves in Z")]
	[Range(0f,100f)]
	public float playerSpeedOffset;
	[Tooltip("Speed the player changes lanes")]
	[Range(0f,30.0f)]
	public float LaneLerpSpeed;
	[Tooltip("Lower the number the less likely AI will change lanes")]
	[Range(0,999)]
	public int AI_ChangeLaneThreshhold;
	public float zGear = 1.1f;								// Which gear the player car is in, 1.1 = 1st, 1.2 = 2nd
	private int[] allPlayersLanePos = new int[4];
	private RaceManager raceManager;
	private DummyShooter dummyShooter;
	private Animator anim;
	private CollisionHandler collisionHandler;
	private CarPositionManager carPositionManager;


	void Start () {
		DetermineLanePosition();
		raceManager = GameObject.FindObjectOfType<RaceManager>();
		carPositionManager = GameObject.FindObjectOfType<CarPositionManager>();
		anim = GetComponent<Animator>();
		dummyShooter = GetComponent<DummyShooter>();
		collisionHandler = GetComponent<CollisionHandler>();
		desiredXPos = transform.position.x;					//desiredXPos isn't assigned a value until we push left or right, need default value
	}//End Start
	

	void Update () {
		if(raceManager.raceHasBegun){						//Only true when race has begun
			totalTimeSinceCollision = collisionHandler.GetTotalTimeSinceCollision();	//Returns time since collision
			ChooseGear();
			MovePlayerZAxis();
			MovePlayerXAxis();
			ReportOurPosition();
			anim.SetBool("isRacingBool", true);				// Starts moving tires and vertical bounce is less
		}
	}// End Update


	void LateUpdate(){
		allPlayersLanePos = carPositionManager.ReturnEveryonesLanePos();	//[0] = player1, value in [0] = lane player1 is in
		//If we have speed then fire and skip the rest
		if(collisionHandler.pickupBoxType == PickupBoxManager.pickupBoxKind.SPEED){
			dummyShooter.FireWeapon();							//Activate speed burst
		}else{
			//is player4 in our lane?  Y=fire if we have a weapon, N=change lanes
			if(allPlayersLanePos[3] == myLanePos){
				//Fire weapon because player4 is in the same lane
				if(collisionHandler.pickupBoxType == PickupBoxManager.pickupBoxKind.CONE || collisionHandler.pickupBoxType == PickupBoxManager.pickupBoxKind.PROJECTILE){
					Invoke("DummyFireWeapon", 0.4f);
				}
			}else{
				if(collisionHandler.pickupBoxType != PickupBoxManager.pickupBoxKind.EMPTY){			//Only want to change AI lanes if we have something to fire
					ChangeLanes();
				}
			}
		}
	}//End


	private void DummyFireWeapon(){
		//Needed a separate method because we are using invoke 
		//we are using an invoke because we are lerping when we change lanes so the lane change is not immediate
		dummyShooter.FireWeapon();
	}//End


	private void ChangeLanes(){
		int random1000;

		//If we don't have speed then change lanes, we will shoot in the next frame
		random1000 = Random.Range(1,1000);
		//Change lane because player4 is not in our lane, but at a threshold, otherwise all the AI cars would be obviously chasing the user
		//if we are in Lane 1
		if(myLanePos == 1){
			if(random1000 < AI_ChangeLaneThreshhold){
				rightArrow = true;
				leftArrow = false;
			}
		}

		//If we are in Lane 2
		if(myLanePos == 2){
			if(random1000 < AI_ChangeLaneThreshhold / 2){
				rightArrow = true;
				leftArrow = false;
			}
			if(random1000 >= AI_ChangeLaneThreshhold / 2 && random1000 < AI_ChangeLaneThreshhold){
				rightArrow = false;
				leftArrow = true;
			}
		}

		//If we are in Lane 3
		if(myLanePos == 3){
			if(random1000 < AI_ChangeLaneThreshhold / 2){
				rightArrow = true;
				leftArrow = false;
			}
			if(random1000 >= AI_ChangeLaneThreshhold / 2 && random1000 < AI_ChangeLaneThreshhold){
				rightArrow = false;
				leftArrow = true;
			}
		}

		//If we are in Lane 4
		if(myLanePos == 4){
			if(random1000 >= AI_ChangeLaneThreshhold){
				rightArrow = false;
				leftArrow = true;
			}
		}
	}//End


	public void DetermineLanePosition (){
		//*** Finds x position of player and assigns lane # ***
		float xPos;
		xPos = transform.position.x;

		if(xPos == 103.4f){						// Lane 4
			myLanePos = 4;
		}else if(xPos == 101.15f){				// Lane 3
			myLanePos = 3;
		}else if(xPos == 98.9f){				// Lane 2
			myLanePos = 2;
		}else if(xPos == 96.65f){				// Lane 1
			myLanePos = 1;
		}

		//return myLanePos;
	}// End DetermineLanePosition


	private void ChooseGear (){
		if(totalTimeSinceCollision < 2){
				zGear = 1.0f;
			}else if(totalTimeSinceCollision > 2 && totalTimeSinceCollision < 4){				
				zGear = 1.1f;
			}else if(totalTimeSinceCollision > 4 && totalTimeSinceCollision < 6){
				zGear = 1.2f;
			}else if(totalTimeSinceCollision > 6){	
				zGear = 1.3f;
			}
	}// End ChooseGear


	private void MovePlayerZAxis (){
		float speedBurstMultiplier = 1.0f;
		float newSpeedMultiplier;
		float oldSpeedMultiplier;

		//Purpose of the following is to lerp the rocket speed burst
		newSpeedMultiplier = dummyShooter.speedBurst;
		oldSpeedMultiplier = speedBurstMultiplier;
		speedBurstMultiplier = Mathf.Lerp(oldSpeedMultiplier, newSpeedMultiplier, 0.8f);

		//Purpose of the following is once we cross the finish line we should lerp to stop
		if(collisionHandler.playerCrossedFinishLine){
			sourceSpeedFactor = finishLineLerpSpeed;
			finishLineLerpSpeed = Mathf.Lerp(sourceSpeedFactor, 0.0f, 0.05f);
		}

		//*** Calculate total Z speed by the following ***
		//zGear is determined by the amount of time since collision, the longer the time the higher the gear
		//speedBurstMultiplier is a small burst of speed given when we execute a speed pickupBox
		//Finish line speed is once we cross the finish line we will lerp our speed to zero which will null all of the other variables
		zSpeed = (playerSpeedOffset * Time.deltaTime) * zGear * speedBurstMultiplier * finishLineLerpSpeed;

		//Move Player in Z axis
		transform.Translate(0,0,zSpeed);
	}// End


	private void MovePlayerXAxis(){
		if(leftArrow && myLanePos >= 2){							// To move left we need to be in atleast the 2nd lane
			myLanePos--;
			leftArrow = false;
		}else if(rightArrow && myLanePos <= 3){						// To move right we need to be in the 3rd lane or less
			myLanePos++;
			rightArrow = false;
		}

		desiredXPos = Mathf.Lerp(transform.position.x, lane_XValue[myLanePos-1], (LaneLerpSpeed * Time.deltaTime));			
		transform.position = new Vector3(desiredXPos, transform.position.y, transform.position.z);						//Move our car in the x axis only
	}//End


	private void ReportOurPosition(){
		iAmThisPlayer = collisionHandler.iAmThisPlayer;
		if(myLanePos != 0){											//We don't want to report our position before we have gotten our position
			carPositionManager.ReportMyLanePos(iAmThisPlayer, myLanePos);
		}
	}
}//End Class

