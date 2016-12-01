using UnityEngine;
using System.Collections;

public class BulletHitSound : MonoBehaviour {
	//Variables
	public AudioClip bulletHitSound;
	private AudioSource audioSource;
	
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void PlayBulletHitSound(){
		audioSource.PlayOneShot(bulletHitSound);
	}//End
}
