using UnityEngine;
using System.Collections;

public class ShootingSound : MonoBehaviour {
	// Variables
	private AudioSource audioSource;
	public AudioClip shootingSound;


	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	

	void Update () {
	
	}


	void PlayShootingSound (){
		float randomVolume = Random.Range(0.7f, 1.0f);						// Use random volume to make the sounds more natural
		audioSource.PlayOneShot(shootingSound, randomVolume);				// Used playOneShot so we can set the volume to random
	}// End PlayShootingSound
}
