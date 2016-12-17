using UnityEngine;
using System.Collections;

public class PickupBoxSpanwer : MonoBehaviour {
	//Variables
	private GameObject showThisGiftBox;
	private Collider thisCollider;
	public GameObject giftBox;


	void Start () {
		thisCollider = GetComponent<Collider>();
		InstantiateBox();
	}//End
	

	void Update () {
	
	}//End


	private void InstantiateBox(){
		//Instatiate gift box and make this it's parent
		thisCollider.enabled = true;
		showThisGiftBox = Instantiate(giftBox) as GameObject;
		showThisGiftBox.transform.parent = transform;
		showThisGiftBox.transform.position = transform.position;
	}//End


	private void DisableBox(){
		thisCollider.enabled = false;		//Disable the pickupBox so the player right behind cannot pick it up
	}


	void OnTriggerEnter(){
		GameObject.Destroy(showThisGiftBox);
		Invoke("DisableBox", 0.1f);
		Invoke("InstantiateBox", 0.9f);
	}//End
}
