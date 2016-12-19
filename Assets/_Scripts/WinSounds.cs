using UnityEngine;
using System.Collections;

public class WinSounds : MonoBehaviour {
	//Variables
	public AudioClip marvelous;
	public AudioClip winJingle;
	public AudioClip secondPlace;
	public AudioClip loseSound;
	private AudioSource audioSource;


	void Start () {
		audioSource = GetComponent<AudioSource>();
	}//End


	public void PlayLoseSound(){
		audioSource.PlayOneShot(loseSound);
	}//End


	public void PlaySecondPlaceSound(){
		audioSource.PlayOneShot(secondPlace);
	}//End


	public void PlayWinSounds(){
		PlayMarvelousSound();
		Invoke("PlayWinJingle", 0.6f);
	}//End


	private void PlayMarvelousSound(){
		audioSource.PlayOneShot(marvelous);
	}//End


	private void PlayWinJingle(){
		audioSource.PlayOneShot(winJingle);
	}//End
}//End class
