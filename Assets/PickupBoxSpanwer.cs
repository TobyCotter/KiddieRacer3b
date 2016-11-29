using UnityEngine;
using System.Collections;

public class PickupBoxSpanwer : MonoBehaviour {
	//Variables
	private GameObject showThisGiftBox;
	public GameObject giftBox;


	void Start () {
		InstantiateBox();
	}//End
	

	void Update () {
	
	}//End


	private void InstantiateBox(){
		//Instatiate gift box and make this it's parent
		showThisGiftBox = Instantiate(giftBox) as GameObject;
		showThisGiftBox.transform.parent = transform;
		showThisGiftBox.transform.position = transform.position;
	}//End


	void OnTriggerEnter(){
		GameObject.Destroy(showThisGiftBox);
		Invoke("InstantiateBox", 0.7f);
	}//End
}
