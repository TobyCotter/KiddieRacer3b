using UnityEngine;
using System.Collections;

public class Denied : MonoBehaviour {
	//Variables
	public AudioClip deniedSound;
	private AudioSource audioSource;
	
	void Start () {
		audioSource = GameObject.FindObjectOfType<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void PlayDeniedSound(){
		audioSource.PlayOneShot(deniedSound);
	}//End
}
