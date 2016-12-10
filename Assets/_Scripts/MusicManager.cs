using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	// Variables
	public AudioClip[] backgroundMusic;
	[Range(0f,1f)]
	public float backgroundMusicVolume;
	private AudioSource audioSource;
	private float myRandom;
	private int clipNumber;



	void Start () {
		audioSource = GetComponent<AudioSource>();
		myRandom = Random.Range(0.6f, 2.70f);						// We want the middle track to be chosen more often than the other 2 which is why the numbers are not 0 and 2.99
		clipNumber = Mathf.FloorToInt(myRandom);					// We want the middle track to be chosen more often than the other 2 which is why the numbers are not 0 and 2.99
		audioSource.clip = backgroundMusic[clipNumber];
		audioSource.loop = true;
		audioSource.volume = backgroundMusicVolume;
	}


	void Update () {
		
	}


	public void PlayBackgroundMusic(){
		audioSource.Play();
	}// End PlayBackgroundMusic


	public void StopPlayingBackgroundMusic (){
		audioSource.Stop ();
	}//End

}//End Class
