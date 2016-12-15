using UnityEngine;
using System.Collections;

public class DummyBrain : MonoBehaviour {
	// Variables
	private float totalTimeSinceCollision;
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
	public float zGear = 1.1f;								// Which gear the player car is in, 1.1 = 1st, 1.2 = 2nd
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
			//TODO determine if we move left or right
			MovePlayerZAxis();
			MovePlayerXAxis();
			ReportOurPosition();
			anim.SetBool("isRacingBool", true);				// Starts moving tires and vertical bounce is less
		}
	}// End Update


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
		int iAmThisPlayer;

		iAmThisPlayer = collisionHandler.iAmThisPlayer;
		if(myLanePos != 0){											//We don't want to report our position before we have gotten our position
			carPositionManager.ReportMyLanePos(iAmThisPlayer, myLanePos);
		}
	}
}//End Class

