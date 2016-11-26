using UnityEngine;
using System.Collections;

public class ConeSound : MonoBehaviour {
	// Variables
	public AudioClip coneSound;
	private AudioSource audioSource;


	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	

	void Update () {
		
	}


	public void PlayConeSound(){
		audioSource.PlayOneShot(coneSound);
	}// End
}
