using UnityEngine;
using System.Collections;

public class Dummy : MonoBehaviour {
	private float zSpeed;
	private float totalTimeSinceCollision;
	private RaceManager raceManager;
	private Animator anim;
	private CollisionHandler collisionHandler;
	private float finishLineLerpSpeed = 1.0f;
	private float sourceSpeedFactor;
	public float playerSpeedOffset;
	public float zGear;


	void Start () {
		raceManager = GameObject.FindObjectOfType<RaceManager>();
		collisionHandler = GetComponent<CollisionHandler>();
		anim = GetComponent<Animator>();
	}


	void Update () {
		if(raceManager.raceHasBegun){
			//totalTimeSinceCollision = totalTimeSinceCollision + Time.deltaTime;
			totalTimeSinceCollision = collisionHandler.GetTotalTimeSinceCollision();	//Returns time since collision
			ChooseGear();
			MovePlayerZAxis ();
			//ChangeLanes();	
			anim.SetBool("isRacingBool", true);		
		}
	}


	private void ChooseGear(){
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
		//Purpose of the following is once we cross the finish line we should lerp to stop
		if(collisionHandler.playerCrossedFinishLine){
			sourceSpeedFactor = finishLineLerpSpeed;
			finishLineLerpSpeed = Mathf.Lerp(sourceSpeedFactor, 0.0f, 0.05f);
		}

		zSpeed = (playerSpeedOffset * Time.deltaTime) * zGear * finishLineLerpSpeed;
		transform.Translate(0,0,zSpeed);
	}// End MovePlayerForward


	public void ResetCollisionTimer (){						// Called from other script to reset this timer to slow down car
		totalTimeSinceCollision = 0;
	}// End ResetCollisionTimer



}//End class
