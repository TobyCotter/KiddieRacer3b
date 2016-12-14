using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// Variables
	private float totalTimeSinceCollision;
	private float desiredXPos;
	private float zSpeed;
	private float[] lane_XValue = {96.65f, 98.9f, 101.15f, 103.4f};
	private bool leftArrow = false;
	private bool rightArrow = false;
	private int myLanePos;
	private float engineSoundPitch;
	private RaceManager raceManager;
	private AudioSource engineAudioSource;
	private Shooter shooter;
	private Animator anim;
	private Player4RightSideCollider player4RightSideCollider;
	private Player4LeftSideCollider player4LeftSideCollider;
	private CollisionHandler collisionHandler;
	private LeftTouchInput leftTouchInput;
	private RightTouchInput rightTouchInput;
	private SpeedProgessBar speedProgressBar;
	private bool rayCastLeftResult;
	private bool OkToMoveLeft;
	private bool OkToMoveRight;
	private float finishLineLerpSpeed = 1.0f;
	private float sourceSpeedFactor = 1.0f;

	[Tooltip("Speed player moves in Z")]
	[Range(0f,100f)]
	public float playerSpeedOffset;
	[Tooltip("Speed the player changes lanes")]
	[Range(0f,30.0f)]
	public float LaneLerpSpeed;
	public float zGear = 1.1f;								// Which gear the player car is in, 1.1 = 1st, 1.2 = 2nd


	void Start () {
		DetermineLanePosition();
		raceManager = GameObject.FindObjectOfType<RaceManager>();
		shooter = GameObject.FindObjectOfType<Shooter>();
		player4RightSideCollider = GameObject.FindObjectOfType<Player4RightSideCollider>();
		player4LeftSideCollider = GameObject.FindObjectOfType<Player4LeftSideCollider>();
		leftTouchInput = GameObject.FindObjectOfType<LeftTouchInput>();
		rightTouchInput = GameObject.FindObjectOfType<RightTouchInput>();
		engineAudioSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		collisionHandler = GetComponent<CollisionHandler>();
		speedProgressBar = GameObject.FindObjectOfType<SpeedProgessBar>();
		desiredXPos = transform.position.x;					//desiredXPos isn't assigned a value until we push left or right, need default value
	}//End Start
	

	void Update () {
		if(raceManager.raceHasBegun){						//Only true when race has begun
			totalTimeSinceCollision = collisionHandler.GetTotalTimeSinceCollision();	//Returns time since collision
			ChooseGear();
			DetectArrowKey();	//TODO delete later as this is only used for windows
			DisableTurnIfNeighborExists();
			MovePlayerZAxis();
			MovePlayerXAxis();
			anim.SetBool("isRacingBool", true);				// Starts moving tires and vertical bounce is less
		}
	}// End Update


	public int DetermineLanePosition (){
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

		return myLanePos;
	}// End DetermineLanePosition


	private void ChooseGear (){
		if(totalTimeSinceCollision < 2){
				zGear = 1.0f;
				speedProgressBar.AdjustSpeedBar(1);					//Sets the speed bar to 1
				engineSoundPitch = 0.4f;
			}else if(totalTimeSinceCollision > 2 && totalTimeSinceCollision < 4){				
				zGear = 1.1f;
				speedProgressBar.AdjustSpeedBar(2);	
				engineSoundPitch = 0.5f;
			}else if(totalTimeSinceCollision > 4 && totalTimeSinceCollision < 6){
				zGear = 1.2f;
				speedProgressBar.AdjustSpeedBar(3);	
				engineSoundPitch = 0.6f;
			}else if(totalTimeSinceCollision > 6){	
				zGear = 1.3f;
				speedProgressBar.AdjustSpeedBar(4);	
				engineSoundPitch = 0.7f;
			}

			engineAudioSource.pitch = engineSoundPitch;					//Changes the pitch of the engine sound to match the gear
	}// End ChooseGear


	private void DetectArrowKey (){
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			LeftTouchInputPressed();
		}

		if(Input.GetKeyDown(KeyCode.RightArrow)){
			RightTouchInputPressed();
		}
	}//End


	public void RightTouchInputPressed(){
		//The right panel was pressed
		rightArrow = true;
		leftArrow = false;
		rightTouchInput.ShowRightButtonBriefly();
	}//End


	public void LeftTouchInputPressed(){
		//The left panel was pressed
		rightArrow = false;
		leftArrow = true;
		leftTouchInput.ShowLeftButtonBriefly();
	}//End


	private void DisableTurnIfNeighborExists(){
		//Detect right side
		if(rightArrow && !player4RightSideCollider.RightSideClear){				//Basically if statement = turn right but there's someone there then deny right turn
			rightArrow = false;													//Disable right turn
			BroadcastMessage("PlayDeniedSound");								//Play denied sound
		}
		//Detect left side
		if(leftArrow && !player4LeftSideCollider.LeftSideClear){
			leftArrow = false;
			BroadcastMessage("PlayDeniedSound");
		}

	}//End


	private void MovePlayerZAxis (){
		float speedBurstMultiplier = 1.0f;
		float newSpeedMultiplier;
		float oldSpeedMultiplier;

		//Purpose of the following is to lerp the rocket speed burst
		newSpeedMultiplier = shooter.speedBurst;
		oldSpeedMultiplier = speedBurstMultiplier;
		speedBurstMultiplier = Mathf.Lerp(oldSpeedMultiplier, newSpeedMultiplier, 0.8f);

		//Purpose of the following is once we cross the finish line we should lerp to stop
		if(collisionHandler.playerCrossedFinishLine){
			sourceSpeedFactor = finishLineLerpSpeed;
			finishLineLerpSpeed = Mathf.Lerp(sourceSpeedFactor, 0.0f, 0.05f);
		}

		//Calculate total Z speed
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
}//End Class

