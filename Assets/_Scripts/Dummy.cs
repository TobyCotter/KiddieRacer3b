using UnityEngine;
using System.Collections;

public class Dummy : MonoBehaviour {
	private float zSpeed;
	private float totalTimeSinceCollision;
	private RaceManager raceManager;
	private Animator anim;
	private CollisionHandler collisionHandler;
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
			MovePlayerForward ();
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

	private void MovePlayerForward (){
		zSpeed = (playerSpeedOffset * Time.deltaTime) * zGear;
		transform.Translate(0,0,zSpeed);
	}// End MovePlayerForward


	void OnTriggerEnter(Collider collider) {   
		
	  
//		if(collider.GetComponent<Dummy>()){    								//If we collided with Dummy player, reset the dummy collision timer       
//        	collider.GetComponent<Dummy>().ResetCollisionTimer();
//        }
    }// End OnTriggerEnter


	public void ResetCollisionTimer (){						// Called from other script to reset this timer to slow down car
		totalTimeSinceCollision = 0;
	}// End ResetCollisionTimer
}
