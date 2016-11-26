using UnityEngine;
using System.Collections;

public class RocketSound : MonoBehaviour {
	//Variables
	public AudioClip rocketSound;
	private AudioSource rocketAudioSource;


	void Start () {
		rocketAudioSource = GetComponent<AudioSource>();
	}
	

	void Update () {
		
	}


	public void PlayRocketSound(){
		rocketAudioSource.PlayOneShot(rocketSound);
	}//End
}
