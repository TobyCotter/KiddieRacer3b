using UnityEngine;
using System.Collections;

public class PlayerEngineSound : MonoBehaviour {
	// Variables
	private AudioSource audioSource;
	private RaceManager raceManager;
	private bool doneThisAlready = false;
	public AudioClip engineIdleSound;
	public AudioClip engineGoSound;
	[Range(0,2)]
	public float engineIdlePitch;
	[Range(0,2)]
	public float engineGoPitch;
	[Range(0,1)]
	public float engineVolume;


	void Start () {
		raceManager = GameObject.FindObjectOfType<RaceManager>();
		audioSource = GetComponent<AudioSource>();
		PlayEngineIdleSound(true);
	}
	

	void Update () {
		if(raceManager.raceHasBegun && !doneThisAlready){
			PlayEngineIdleSound(false);
			PlayEngineGoSound(true);
			doneThisAlready = true;
		}
	}


	private void PlayEngineIdleSound(bool playMe){	
		// PLay engine idle sound only before the race has started
		if(playMe){	
			audioSource.clip = engineIdleSound;
			audioSource.loop = true;
			audioSource.pitch = engineIdlePitch;
			audioSource.volume = engineVolume;
			audioSource.Play();
		}else{
			audioSource.Stop();
		}
	}// End PlayIdle


	public void PlayEngineGoSound(bool playMe){
		if(playMe){
			audioSource.clip = engineGoSound;
			audioSource.loop = true;
			audioSource.pitch = engineGoPitch;
			audioSource.volume = engineVolume;
			audioSource.Play();
		}else{
			audioSource.Stop();
		}
	}// End PlayEngineGoSound
}
