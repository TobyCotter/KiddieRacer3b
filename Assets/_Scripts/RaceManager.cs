using UnityEngine;
using System.Collections;
using System.Linq;						// This was needed for the Orderby method

public class RaceManager : MonoBehaviour {
	// Variables
	private float timeSinceStart;
	private bool firstTimeThru = true;
	private MusicManager musicManager;
	private AudioSource audioSource;
	private Racer[] sortedRacerArray;
	public bool raceHasBegun = false;
	public AudioClip ready;
	public AudioClip go;


	void Start () {
		musicManager = GameObject.FindObjectOfType<MusicManager>();
		audioSource = GetComponent<AudioSource>();
	}//End


	void Update (){

	}//End
	

	void LateUpdate () {
		if(!raceHasBegun){										// Once the race has begun we no longer need to check it (just saves computer power)
			WaitToStartRace ();									// Starts race after a predetermined amount of time
		}
	}//End


	public int FindPlayerFourRacePosition (){
		//*** Returns the position of player4 as an int ***
		sortedRacerArray = GameObject.FindObjectsOfType<Racer>().OrderByDescending(racerArray=>racerArray.transform.position.z).ToArray();			

		for (int i = 0; i < 4; i++) {
			if(sortedRacerArray[i].CompareTag("Player4")){
				return i+1;									//Returns the position of player4.  First = 1, second = 2, etc...
			}
		}

		return 0;
	}// End


	private void WaitToStartRace(){
		timeSinceStart = timeSinceStart + Time.deltaTime;

		//Play ready first time thru
		if(firstTimeThru){
			Invoke("PlayReadySound", 1.0f);
			firstTimeThru = false;
		}

		//Play Go and start race
		if(timeSinceStart > 3){
			// Start Race after time listed above!!!  BOOM!
			audioSource.PlayOneShot(go);
			raceHasBegun = true;						//Will not enter this routine again once this variable is set to true
			Invoke("PlayBackgroundMusic", 0.3f);		//Added delay so "Go" is played completely
		}
	}// End


	private void PlayReadySound(){
		audioSource.PlayOneShot(ready);
	}//End


	private void PlayBackgroundMusic(){
		musicManager.PlayBackgroundMusic();
	}//End
}







//  public int firstPlace, secondPlace, thirdPlace, fourthPlace;
//  public enum racePosition {first, second, third, fourth, invalid};
//
//	public void FindRaceOrder(){
//		// Decides what place everyone is in so we can decide what pickup box to give them
//		sortedRacerArray = GameObject.FindObjectsOfType<Racer>().OrderByDescending(racerArray=>racerArray.transform.position.z).ToArray();
//
//		if(sortedRacerArray[0].CompareTag("Player1")){
//			firstPlace = 1;		
//		}else if(sortedRacerArray[0].CompareTag("Player2")){
//			firstPlace = 2;
//		}else if(sortedRacerArray[0].CompareTag("Player3")){
//			firstPlace = 3;
//		}else if(sortedRacerArray[0].CompareTag("Player4")){
//			firstPlace = 4;
//		}
//
//
//		Debug.Log("First place: " + firstPlace);
//
//	}
//
//	public racePosition FindPlayerFourRacePosition(){
//		//*** Returns the position of player4 as an enum ***
//		sortedRacerArray = GameObject.FindObjectsOfType<Racer>().OrderByDescending(racerArray=>racerArray.transform.position.z).ToArray();			
//
//		for (int i = 0; i < 4; i++) {
//			if(sortedRacerArray[i].CompareTag("Player4")){
//				if(i == 0){
//					return racePosition.first;
//				}else if(i == 1){
//					return racePosition.second;
//				}else if(i == 2){
//					return racePosition.third;
//				}else if(i == 3){
//					return racePosition.fourth;
//				}
//			}
//		}
//
//		return racePosition.invalid;
//		}// End
