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
	private bool rayCastLeftResult;
	private bool OkToMoveLeft;
	private bool OkToMoveRight;

	[Tooltip("Speed player moves in Z")]
	[Range(0f,100f)]
	public float playerSpeedOffset;
	[Tooltip("Speed the player changes lanes")]
	[Range(0f,30.0f)]
	public float LaneLerpSpeed;
	public float zGear = 1.1f;							// Which gear the player car is in, 1.1 = 1st, 1.2 = 2nd


	void Start () {
		DetermineLanePosition();
		raceManager = GameObject.FindObjectOfType<RaceManager>();
		shooter = GameObject.FindObjectOfType<Shooter>();
		engineAudioSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		desiredXPos = transform.position.x;				//desiredXPos isn't assigned a value until we push left or right, need default value
	}
	

	void Update () {
		if(raceManager.raceHasBegun){					//Only true when race has begun
			totalTimeSinceCollision = totalTimeSinceCollision + Time.deltaTime;
			ChooseGear();
			DetectArrowKey();			
			MovePlayerZAxis();
			MovePlayerXAxis();
			anim.SetBool("isRacingBool", true);			// Starts moving tires and vertical bounce is less
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
				engineSoundPitch = 0.4f;
			}else if(totalTimeSinceCollision > 2 && totalTimeSinceCollision < 4){				
				zGear = 1.1f;
				engineSoundPitch = 0.5f;
			}else if(totalTimeSinceCollision > 4 && totalTimeSinceCollision < 6){
				zGear = 1.2f;
				engineSoundPitch = 0.6f;
			}else if(totalTimeSinceCollision > 6){	
				zGear = 1.3f;
				engineSoundPitch = 0.7f;
			}

			engineAudioSource.pitch = engineSoundPitch;					//Changes the pitch of the engine sound to match the gear
	}// End ChooseGear


	private void DetectArrowKey (){
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			leftArrow = true;
			rightArrow = false;
		}

		if(Input.GetKeyDown(KeyCode.RightArrow)){
			rightArrow = true;
			leftArrow = false;
		}
	}//End


	private void MovePlayerZAxis (){
		float speedBurstMultiplier = 1.0f;
		float newSpeedMultiplier;
		float oldSpeedMultiplier;

		//Purpose of the following is to lerp the rocket speed burst
		newSpeedMultiplier = shooter.speedBurst;
		oldSpeedMultiplier = speedBurstMultiplier;
		speedBurstMultiplier = Mathf.Lerp(oldSpeedMultiplier, newSpeedMultiplier, .8f);

		//Calculate total Z speed
		zSpeed = (playerSpeedOffset * Time.deltaTime) * zGear * speedBurstMultiplier;

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

