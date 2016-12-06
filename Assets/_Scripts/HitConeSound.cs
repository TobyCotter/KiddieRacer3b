using UnityEngine;
using System.Collections;

public class HitConeSound : MonoBehaviour {
	//Variables
	public AudioClip hitConeSound;
	private AudioSource audioSource;


	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	

	void Update () {
		
	}


	public void PlayHitConeSound(){
		audioSource.PlayOneShot(hitConeSound);
	}//End
}
