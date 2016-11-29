// This script changes the image on our canvas to match the pickup box

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickupBoxDisplayImage : MonoBehaviour {
	// Variables
	private Image imageComponent;
	//private CollisionHandler collisionHandler;
	public Sprite cone;
	public Sprite lightning;
	public Sprite projectile;			//public domain pixbay
	public Sprite none;

	void Start () {
		imageComponent = GetComponent<Image>();
		//collisionHandler = GameObject.FindObjectOfType<CollisionHandler>();
	}


	void Update () {
		
	}


	public void SetPickupBoxImage (int pickupBoxNum){
		// Sets the canvas image to cone, projectile, speed, or empty
		if(pickupBoxNum == 0){
			imageComponent.sprite = cone;
		}else if(pickupBoxNum == 1){
			imageComponent.sprite = projectile;
		}else if(pickupBoxNum == 2){
			imageComponent.sprite = lightning;
		}else{
			imageComponent.sprite = none;
		}
	}// End
}
