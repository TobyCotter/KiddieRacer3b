using UnityEngine;
using System.Collections;

public class RearAfterBurner : MonoBehaviour {
	//Variables
	private bool junk;
	private ParticleSystem particleSys;

	
	void Awake () {
		particleSys = GetComponent<ParticleSystem>();
	}//End


	public void ActivateAfterburner(){
		particleSys.Play(true);					//"True" is to stop particle in all children as well
	}//End


	public void DeactivateAfterburner(){
		particleSys.Stop(true);					//"True" is to stop particle in all children as well
	}//End
}//End Class
