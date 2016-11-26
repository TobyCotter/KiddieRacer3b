// This script changes the image on our canvas to match the pickup box

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickupBoxDisplayImage : MonoBehaviour {
	// Variables
	private Image imageComponent;
	private CollisionHandler collisionHandler;
	public Sprite cone;
	public Sprite lightning;
	public Sprite projectile;			//public domain pixbay
	public Sprite none;

	void Start () {
		imageComponent = GetComponent<Image>();
		collisionHandler = GameObject.FindObjectOfType<CollisionHandler>();
	}


	void Update () {
		
	}


	public void SetPickupBoxImage (){
		// Sets the canvas image to cone, projectile, speed, or empty
		if(CollisionHandler.PICKUPBOX_TYPE == PickupBoxManager.pickupBoxKind.CONE){
			imageComponent.sprite = cone;
		}else if(CollisionHandler.PICKUPBOX_TYPE == PickupBoxManager.pickupBoxKind.PROJECTILE){
			imageComponent.sprite = projectile;
		}else if(CollisionHandler.PICKUPBOX_TYPE == PickupBoxManager.pickupBoxKind.SPEED){
			imageComponent.sprite = lightning;
		}else if(CollisionHandler.PICKUPBOX_TYPE == PickupBoxManager.pickupBoxKind.EMPTY){
			imageComponent.sprite = none;
		}
	}// End

}
