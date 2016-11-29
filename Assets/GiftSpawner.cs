using UnityEngine;
using System.Collections;

public class GiftSpawner : MonoBehaviour {
	//Variables
	public GameObject[] giftBoxPrefabArray;
	public int giftBoxIndex;
	private GameObject showThisGiftBox;
	
	void Start () {
		giftBoxIndex = Random.Range(0,3);					//Chooses which giftBox to instantiate at startup
		InstantiateBox();
	}
	

	void Update () {
	
	}


	void OnTriggerEnter(){									//Anytime there is a trigger we will change the giftbox
		GameObject.Destroy(showThisGiftBox);
		Invoke("InstantiateBox", 1.0f);
	}//End Trigger


	private void InstantiateBox (){
		giftBoxIndex++;										//We incremented the index inside the invoke so the other trigger would detect this GiftBox and not the next one
		if(giftBoxIndex == 3){								//After the index 2 we want to go to 0
			giftBoxIndex = 0;
		}

		showThisGiftBox = Instantiate(giftBoxPrefabArray[giftBoxIndex]) as GameObject;
		showThisGiftBox.transform.parent = transform;
		showThisGiftBox.transform.position = transform.position;
	}//End


}




//		GameObject myNewGiftBox = Instantiate(giftBoxPrefabArray[2]) as GameObject;
//		myNewGiftBox.transform.parent = transform;
//		myNewGiftBox.transform.position = transform.position;
//		Debug.Log("New giftbox: " + myNewGiftBox.name);
//		if(myNewGiftBox.name == "Gift_3(Clone)"){
//			Debug.Log("it equals gift 3");
//		}
