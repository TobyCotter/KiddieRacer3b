using UnityEngine;
using System.Collections;

public class SparksParticle : MonoBehaviour {
	//Variables
	private ParticleSystem particleSys;


	void Start () {
		particleSys = GetComponentInChildren<ParticleSystem>();
		if(!particleSys){											//I we didn't find the particle system
			Debug.LogError("Could not find the childed sparks particle system");
		}
	}//End
	
	// Update is called once per frame
	void Update () {
	
	}//End


	public void PlaySparksParticleSystem(){
		particleSys.Play(true);
	}//End


	public void StopSparksParicleSystem(){

	}
}
