using UnityEngine;
using System.Collections;

public class PickupBoxSound : MonoBehaviour {
	//Variables
	private AudioSource audioSource;
	public AudioClip pickupBoxSound;

	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {

	}


	void PlayPickupBoxSound(){
		float randomVolume = Random.Range(0.7f, 1.0f);	
		audioSource.PlayOneShot(pickupBoxSound, randomVolume);
	}// PlayPickupBoxSound()
}
