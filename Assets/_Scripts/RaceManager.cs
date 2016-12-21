using UnityEngine;
using System.Collections;
using System.Linq;						// This was needed for the Orderby method

public class RaceManager : MonoBehaviour {
	// Variables
	private int youFinishedThisPlace;
	private float timeSinceStart;
	private bool firstTimeThru = true;
	private DirectionalLight directionalLight;
	private MusicManager musicManager;
	private AudioSource audioSource;
	private WinSounds winSounds;
	private FinalFinishText finalFinishText;
	private Racer[] sortedRacerArray;
	private PlayAgainManager playAgainManager;
	private QuitButtonManager quitButtonManager;
	private bool firstTimeThruThis = true;
	private FinalFinishImage finalFinishImage;
	public int[] finalRaceOrder = new int[4];
	public bool raceHasBegun = false;
	public AudioClip ready;
	public AudioClip go;


	void Start () {
		musicManager = GameObject.FindObjectOfType<MusicManager>();
		playAgainManager = GameObject.FindObjectOfType<PlayAgainManager>();
		quitButtonManager = GameObject.FindObjectOfType<QuitButtonManager>();
		audioSource = GetComponent<AudioSource>();
		finalFinishImage = GameObject.FindObjectOfType<FinalFinishImage>();
		finalFinishText = GameObject.FindObjectOfType<FinalFinishText>();
		winSounds = GameObject.FindObjectOfType<WinSounds>();
		directionalLight = GameObject.FindObjectOfType<DirectionalLight>();
	}//End


	void Update (){
		HandlePostRaceEvents();	
	}//End
	

	void LateUpdate () {
		if(!raceHasBegun){										// Once the race has begun we no longer need to check it (just saves computer power)
			WaitToStartRace ();									// Starts race after a predetermined amount of time
		}
	}//End


	void HandlePostRaceEvents ()
	{
		if(FinishLine.FINISH_POSITION == 5 && firstTimeThruThis == true){
			//**** All 4 players have crossed the finish line @ this point ****

			//Get player4's finish position (and the total finish order)
			GetFinalFinishOrder ();

			//Display finish position image/text for player 4
			finalFinishText.DisplayFinishText (youFinishedThisPlace);
			finalFinishImage.DisplayFinishImage (youFinishedThisPlace);
			if(youFinishedThisPlace == 1){
				winSounds.PlayWinSounds();
			}else if(youFinishedThisPlace ==2){
				winSounds.PlaySecondPlaceSound();
			}else if(youFinishedThisPlace >= 3){
				winSounds.PlayLoseSound();
			}

			//Stop background music
			musicManager.StopPlayingBackgroundMusic ();

			//Enable Play Again button
			playAgainManager.EnablePlayAgainButton();
			quitButtonManager.EnableQuitButton();

			//Dim background so UI canvas shows up better
			directionalLight.DimTheLights();

			//Only pass thru this once
			firstTimeThruThis = false;		//Only report the finish order once
		}//End if
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

	void GetFinalFinishOrder ()
	{
		for (int i = 0; i < finalRaceOrder.Length; i++) {
			//finalRaceOrder variable is populated by other scripts
			if (finalRaceOrder [i] == 4) {
				youFinishedThisPlace = i + 1;
			}//End if
		}//End for
	}//End ()
}//End class







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
